using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class NPCIdleWander : MonoBehaviour
{
    [Header("Decision")]
    [Tooltip("Time between decisions (after previous finishes)")]
    public float decisionCooldown = 3f;

    [Range(0f, 1f)]
    [Tooltip("Chance that NPC decides to move")]
    public float moveChance = 0.5f;

    [Header("Movement")]
    public float walkSpeed = 1.2f;
    public float walkDuration = 1.5f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 facingDir = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.linearVelocity = Vector2.zero;
    }

    private void Start()
    {
        // Force initial facing direction so animator updates
        Vector2 startDir = RandomDirection();
        UpdateFacing(startDir);
        SetMovement(Vector2.zero);
        StartCoroutine(DecisionLoop());
    }

    IEnumerator DecisionLoop()
    {
        while (true)
        {
            // ---- IDLE ----
            rb.linearVelocity = Vector2.zero;
            SetMovement(Vector2.zero);
            UpdateFacing(RandomDirection());

            // Wait BEFORE making a new decision
            yield return new WaitForSeconds(decisionCooldown);

            // ---- MAKE DECISION ----
            if (Random.value > moveChance)
            {
                // Stay idle, but maybe turn
                UpdateFacing(RandomDirection());
                continue;
            }

            // ---- MOVE ----
            Vector2 moveDir = RandomDirection();
            UpdateFacing(moveDir);

            float timer = 0f;

            while (timer < walkDuration)
            {
                rb.linearVelocity = moveDir * walkSpeed;

                SetMovement(moveDir);
                UpdateFacing(moveDir);

                timer += Time.deltaTime;
                yield return null;
            }

            // Stop after movement finishes
            rb.linearVelocity = Vector2.zero;
            SetMovement(Vector2.zero);
        }
    }

    // ---------------- Animator ----------------

    void SetMovement(Vector2 dir)
    {
        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);
        anim.SetFloat("Speed", dir.sqrMagnitude);

        if (dir != Vector2.zero)
        {
            anim.SetFloat("LastX", dir.x);
            anim.SetFloat("LastY", dir.y);
        }
    }

    void UpdateFacing(Vector2 dir)
    {
        if (dir == Vector2.zero)
            return;

        facingDir = dir.normalized;
        anim.SetFloat("LastX", facingDir.x);
        anim.SetFloat("LastY", facingDir.y);
    }

    Vector2 RandomDirection()
    {
        return Random.Range(0, 4) switch
        {
            0 => Vector2.up,
            1 => Vector2.down,
            2 => Vector2.left,
            _ => Vector2.right,
        };
    }
}

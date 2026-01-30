using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 input;
    private Vector2 lastMoveDir = Vector2.down; // hướng idle mặc định

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ReadInput();
        UpdateAnimator();
        HandleAttack();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input.normalized * moveSpeed;
    }

    void ReadInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero)
        {
            lastMoveDir = input;
        }
    }

    void UpdateAnimator()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("Speed", input.sqrMagnitude);

        // hướng idle
        anim.SetFloat("LastX", lastMoveDir.x);
        anim.SetFloat("LastY", lastMoveDir.y);
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }
}

using System.Collections;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(BoxCollider2D))]
public class TeleportPoint : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("Where the player will be teleported to")]
    public Transform targetPoint;

    [Tooltip("How far the player is pushed out of the target trigger")]
    [SerializeField] private float exitOffset = 1.5f;

    [Tooltip("Delay while screen is fully black")]
    [SerializeField] private float teleportDelay = 0.5f;

    [Header("Fade Settings")]
    [Tooltip("Black screen image (UI Image)")]
    public Image fadeImage;

    [Tooltip("Fade speed")]
    public float fadeSpeed = 5f;

    private static bool teleportLocked = false;

    private void Awake()
    {
        // Fade should be invisible & inactive by default
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportLocked)
            return;

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleportRoutine(collision.transform));
        }
    }

    IEnumerator TeleportRoutine(Transform player)
    {
        teleportLocked = true;

        fadeImage.gameObject.SetActive(true);

        // Fade to black
        yield return Fade(1f);

        yield return new WaitForSeconds(teleportDelay);

        // Get facing direction (SAFE)
        Vector2 facingDir = GetFacingDirection(player);

        // Teleport
        player.position = targetPoint.position + (Vector3)(facingDir * exitOffset);

        // Stop movement if using Rigidbody2D
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Fade back in
        yield return Fade(0f);

        fadeImage.gameObject.SetActive(false);

        // Small delay prevents instant retrigger
        yield return new WaitForSeconds(0.1f);

        teleportLocked = false;
    }

    IEnumerator Fade(float targetAlpha)
    {
        Color color = fadeImage.color;

        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = color;
            yield return null;
        }
    }

    Vector2 GetFacingDirection(Transform player)
    {
        var movement = player.GetComponent<PlayerMovement>();
        if (movement != null && movement.lastMoveDir != Vector2.zero)
            return movement.lastMoveDir.normalized;

        return Vector2.down;
    }

}

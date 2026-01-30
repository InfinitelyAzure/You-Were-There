using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float detectRadius = 1.2f;
    public LayerMask interactLayer;

    private Interactable currentTarget;

    void Update()
    {
        Detect();
        HandleInput();
    }

    void Detect()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            detectRadius,
            interactLayer
        );

        if (hit == null)
        {
            Clear();
            return;
        }

        Interactable interactable = hit.GetComponent<Interactable>();

        if (interactable != null && interactable != currentTarget)
        {
            currentTarget = interactable;
            InteractUIManager.Instance.Show(currentTarget.GetText());
        }
    }

    void HandleInput()
    {
        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            currentTarget.Interact();
        }
    }

    void Clear()
    {
        if (currentTarget != null)
        {
            currentTarget = null;
            InteractUIManager.Instance.Hide();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}

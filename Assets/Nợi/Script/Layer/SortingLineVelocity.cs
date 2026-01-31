using UnityEngine;

/// <summary>
/// Sorting line 2 hướng (theo 1 trục).
/// - Hoạt động với TriggerEnter + TriggerStay
/// - Player collider nằm ở object con
/// - Player root xác định bằng Rigidbody2D
/// - Mỗi frame chỉ đổi sorting nếu cần
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class SortingLineVelocity : MonoBehaviour
{
    public enum Axis
    {
        Vertical,   // Trái ↔ Phải
        Horizontal  // Trên ↔ Dưới
    }

    [Header("Detect Axis")]
    public Axis detectAxis = Axis.Horizontal;

    [Header("Sorting Order Mapping")]
    public int fromTop = 0;
    public int fromBottom = 1;
    public int fromLeft = 2;
    public int fromRight = 3;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleTrigger(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandleTrigger(other);
    }

    /// <summary>
    /// Logic xử lý chung cho Enter / Stay
    /// </summary>
    private void HandleTrigger(Collider2D other)
    {
        // ✅ Lấy đúng Player root bằng Rigidbody2D
        if (!other.CompareTag("PlayerTrackerLayer")) return;
        Rigidbody2D rb = other.GetComponentInParent<Rigidbody2D>();
        if (!rb) return;

        GameObject playerRoot = rb.gameObject;

        // Optional: đảm bảo đúng Player
        if (!playerRoot.CompareTag("Player")) return;

        SpriteRenderer sr = playerRoot.GetComponent<SpriteRenderer>();
        if (!sr) return;

        // Player đứng yên → không đổi sorting
        if (rb.linearVelocity.sqrMagnitude < 0.0001f) return;

        Vector2 dir = rb.linearVelocity.normalized;
        int targetSorting = sr.sortingOrder;

        // ===== KIỂM TRA THEO TRỤC =====
        if (detectAxis == Axis.Horizontal)
        {
            // Trên ↔ Dưới
            if (Mathf.Abs(dir.y) < Mathf.Abs(dir.x)) return;

            targetSorting = dir.y > 0 ? fromBottom : fromTop;
        }
        else
        {
            // Trái ↔ Phải
            if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y)) return;

            targetSorting = dir.x > 0 ? fromLeft : fromRight;
        }

        // ===== CHỈ ĐỔI KHI KHÁC =====
        if (sr.sortingOrder != targetSorting)
        {
            sr.sortingOrder = targetSorting;
        }
    }
}

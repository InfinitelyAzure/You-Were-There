using UnityEngine;

public class BossSkillDamage : MonoBehaviour
{
    public Vector2 hitBoxSize = new Vector2(1.2f, 1.2f);
    public LayerMask playerLayer;
    public float lifeTime = 0.5f;

    void Start()
    {
        Invoke(nameof(DoDamage), 0.05f);
        Destroy(gameObject, lifeTime);
    }

    void DoDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position,
            hitBoxSize,
            0f,
            playerLayer
        );

        foreach (var hit in hits)
        {
            hit.GetComponentInParent<PlayerMovement>()?.TakeHit();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, hitBoxSize);
    }
    public void Skillend()=>gameObject.SetActive(false);
}

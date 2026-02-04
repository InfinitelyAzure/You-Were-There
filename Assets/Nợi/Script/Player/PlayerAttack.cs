using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayer;

    public void DoAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        foreach (var hit in hits)
        {
            var boss = hit.GetComponent<BossController>();
            if(boss!=null) hit.GetComponent<BossController>().TakeHit();
            else hit.GetComponent<Boss2Controller>().TakeHit();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

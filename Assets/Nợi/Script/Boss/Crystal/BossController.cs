using UnityEngine;

public class BossController : MonoBehaviour
{
    public float attackCooldown = 2f;
    private Animator anim;
    private float timer;
    public int maxHitCount = 20;
    private int currentHit;

    [Header("Hitbox")]
    public Transform leftHandPoint;
    public Transform rightHandPoint;
    public Transform skillPoint;
    public Vector2 boxSkillSize = new Vector2(1.5f, 1f);
    public Vector2 boxThunderSize = new Vector2(1.5f, 1f);
    public LayerMask playerLayer;
    public GameObject[] skillthunder;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ChooseAttack();
            timer = attackCooldown;
        }
    }

    void ChooseAttack()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                anim.SetTrigger("AttackLeft");
                break;
            case 1:
                anim.SetTrigger("AttackRight");
                break;
            case 2:
                anim.SetTrigger("Skill");
                break;
        }
    }
    public void LeftHandAttack()
    {
        CreateHitbox(leftHandPoint);
    } 
    public void RightHandAttack()
    {
        CreateHitbox(rightHandPoint);
    } 
    public void SkillAttack(){
        CreateHitbox(skillPoint);
    }
    public void ThunderSpawm()
    {
        foreach(var obj in skillthunder)
        {
            if(obj==null) return;
            obj.SetActive(true);
        }
    }

    public void CreateHitbox(Transform point)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            point.position,
            boxSkillSize,
            0f,
            playerLayer
        );

        foreach (var hit in hits)
        {
            hit.GetComponentInParent<PlayerMovement>().TakeHit();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (leftHandPoint)
            Gizmos.DrawWireCube(leftHandPoint.position, boxSkillSize);

        if (rightHandPoint)
            Gizmos.DrawWireCube(rightHandPoint.position, boxSkillSize);

        if (skillPoint)
            Gizmos.DrawWireCube(skillPoint.position, boxThunderSize);
    }
    public void TakeHit()
    {
        currentHit++;

        if (currentHit >= maxHitCount)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("Dead");
        attackCooldown=30;
    }
    public void BossDie()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;

public class Boss2Controller : MonoBehaviour
{
    [Header("Attack")]
    public float attackCooldown = 3f;
    private float timer;
    private Animator anim;

    [Header("Skill Spawn")]
    public GameObject skillPrefab;
    public int spawnCount = 3;
    public float spawnDelay = 0.4f;

    public Vector2 spawnAreaSize = new Vector2(6f, 3f);
    public Transform forwardPoint;

    [Header("Hit")]
    public int maxHitCount = 25;
    private int currentHit;
    private bool Isdie=false;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            anim.SetTrigger("Attack");
            timer = attackCooldown;
        }
        if(Isdie) anim.ResetTrigger("Attack");
    }

    // ===== GỌI BẰNG ANIMATION EVENT =====
    public void SpawnSkill()
    {
        StartCoroutine(SpawnSkillRoutine());
    }

    IEnumerator SpawnSkillRoutine()
    {
        if(Isdie) yield break;
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos = GetRandomPositionInFront();
            Instantiate(skillPrefab, spawnPos, Quaternion.identity);
            if(Isdie) yield break;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    Vector2 GetRandomPositionInFront()
    {
        Vector2 center = forwardPoint.position;

        float randomX = Random.Range(
            -spawnAreaSize.x / 2,
            spawnAreaSize.x / 2
        );

        float randomY = Random.Range(
            -spawnAreaSize.y / 2,
            spawnAreaSize.y / 2
        );

        return center + new Vector2(randomX, randomY);
    }

    // ===== DAMAGE =====
    public void TakeHit()
    {
        currentHit++;

        if (currentHit >= maxHitCount)
        {
            Die();
            StopCoroutine(SpawnSkillRoutine());
        }
    }

    void Die()
    {
        Isdie=true;
        anim.SetTrigger("Dead");
        attackCooldown = 999f;
    }

    public void BossDie()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (forwardPoint == null) return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(
            forwardPoint.position,
            spawnAreaSize
        );
    }
}

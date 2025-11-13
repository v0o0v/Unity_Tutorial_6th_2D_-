using UnityEngine;

public class BearController : MonoBehaviour
{
    private Animator bearAnim;
    public Transform target;
    private SpriteRenderer renderer;

    public float moveSpeed = 5f;

    private float attackTimer = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;

    private float timer;

    private int randomDir;
    private bool isPatrol = false;

    void Start()
    {
        bearAnim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform; // 플레이어를 타겟으로 설정
            bearAnim.SetBool("IsRun", true);
            bearAnim.SetBool("IsWalk", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null; // 타겟 없음
            bearAnim.SetBool("IsRun", false);
            bearAnim.SetBool("IsWalk", false);
        }
    }

    void Update()
    {
        if (target != null)
            Trace();
        else
            Patrol();
    }

    private void Patrol()
    {
        timer += Time.deltaTime;
        
        if (isPatrol)
        {
            if (timer >= 2f)
            {
                timer = 0f;
                bearAnim.SetBool("IsWalk", false);
                isPatrol = false;
            }
            
            transform.position += Vector3.right * randomDir * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (timer >= 3f)
            {
                timer = 0f;
                randomDir = Random.Range(-1, 2); // -1, 0, 1의 랜덤값
                isPatrol = true;

                if (randomDir != 0)
                    bearAnim.SetBool("IsWalk", true);

                if (randomDir > 0)
                    renderer.flipX = false;
                else if  (randomDir < 0)
                    renderer.flipX = true;
            }
        }
    }

    private void Trace()
    {
        Vector3 dir = (target.position - transform.position).normalized; // 타겟을 향하는 방향

        transform.position += dir * moveSpeed * Time.deltaTime;

        if (dir.x > 0)
            renderer.flipX = false;
        else if (dir.x < 0)
            renderer.flipX = true;

        Attack();
    }

    private void Attack()
    {
        float distance = Vector3.Distance(transform.position, target.position); // 곰과 플레이어 사이의 거리

        if (distance < attackRange) // 바로 앞에 있을 때
        {
            bearAnim.SetBool("IsRun", false);

            attackTimer += Time.deltaTime; // 타이머 기능

            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0f;
                bearAnim.SetTrigger("Attack");
            }
        }
        else // 조금 멀리 있을 때
        {
            bearAnim.SetBool("IsRun", true);
        }
    }
}
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] public float hp = 2f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private float stunTime = 0.2f;         // 피격 멈춤 시간
    [SerializeField] private float gravityDuration = 0.3f;   // 중력 유지 시간
    [SerializeField] private float floatAmplitude = 0.05f;  // 떠다니는 높이 변동 폭
    [SerializeField] private float floatFrequency = 1f;      // 떠다니는 속도
    [SerializeField] public float enemyBodyDamage = 1f;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private Coroutine hitCoroutine;
    private Animator animator;
    private Rigidbody2D rb;
    private EnemySpawner spawner;

    private bool isKnock = false;
    public bool movingLeft = true;

    private System.Random rand;
    private float moveTime;
    private float startY;
    public AudioClip hurt;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rand = new System.Random();
        moveTime = GetRandomMoveTime();
        startY = transform.position.y;

        StartCoroutine(MoveRoutine());
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (!isKnock)
            {
                // 이동 방향에 따라 스프라이트 반전
                spriteRenderer.flipX = !movingLeft;

                // 좌우 이동
                Vector2 direction = new Vector2(movingLeft ? -1 : 1, 0);
                rb.linearVelocity = direction * moveSpeed;

                // 떠다니는 효과 (sin 파형 활용)
                float sinValue = Mathf.Sin(6f * Time.time * floatFrequency);
                float offsetY = (sinValue + 1f) * 0.5f * floatAmplitude;
                transform.position = new Vector2(transform.position.x, startY + offsetY);

                // 일정 시간 후 방향 전환
                moveTime -= Time.deltaTime;
                if (moveTime <= 0)
                {
                    movingLeft = !movingLeft;
                    moveTime = GetRandomMoveTime();
                }
            }
            else
            {
                // 넉백 중에는 이동 정지
                rb.linearVelocity = Vector2.zero;
            }
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftWall"))
        {
            movingLeft = false;
            moveTime = GetRandomMoveTime();
        }
        else if (other.CompareTag("RightWall"))
        {
            movingLeft = true;
            moveTime = GetRandomMoveTime();
        }
        else if (other.CompareTag("Ground"))
        {
            rb.gravityScale = 0f;
            isKnock = false;
        }

        if (other.gameObject.CompareTag("Weapon"))
        {
            audioSource.PlayOneShot(hurt);
            hp -= Player.Instance.attack;

            // 피격 시 넉백 (한 번만 실행)
            if (!isKnock)
            {
                beShot();
            }

            // 피격 모션 강제 재생 (매번 실행)
            if (hitCoroutine != null)
                StopCoroutine(hitCoroutine);
                hitCoroutine = StartCoroutine(HandleHitEffect());

            // 사망 처리 (피격 모션 후 파괴)
            if (hp <= 0)
            {
                StartCoroutine(DieAfterHit());
            }
        }

        if (other.gameObject.CompareTag("Bullet")){
            hp -= Player.Instance.gunAttack;

            // 피격 시 넉백 (한 번만 실행)
            if (!isKnock)
            {
                beShot();
            }

            // 피격 모션 강제 재생 (매번 실행)
            if (hitCoroutine != null)
                StopCoroutine(hitCoroutine);
                hitCoroutine = StartCoroutine(HandleHitEffect());

            // 사망 처리 (피격 모션 후 파괴)
            if (hp <= 0)
            {
                StartCoroutine(DieAfterHit());
            }
        }
        
        if(Player.Instance.isSwing && other.gameObject.CompareTag("Player")){
            audioSource.PlayOneShot(hurt);  
            hp -= Player.Instance.swordAttack;

            // 피격 시 넉백 (한 번만 실행)
            if (!isKnock)
            {
                beShot();
            }

            // 피격 모션 강제 재생 (매번 실행)
            if (hitCoroutine != null)
                StopCoroutine(hitCoroutine);
                hitCoroutine = StartCoroutine(HandleHitEffect());

            // 사망 처리 (피격 모션 후 파괴)
            if (hp <= 0)
            {
                StartCoroutine(DieAfterHit());
            }
        }
    }

    private IEnumerator DieAfterHit()
    {
        // 피격 모션 완료까지 대기
        if (hitCoroutine != null)
            yield return hitCoroutine;

        // 코인 생성 및 오브젝트 파괴
        spawner.OnEnemyDied();
        Instantiate(Coin, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private float GetRandomMoveTime()
    {
        return rand.Next(1, 4);
    }

    private void beShot()
    {
        isKnock = true;
        rb.gravityScale = 1f;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private IEnumerator HandleHitEffect()
    {
        // 애니메이터가 있으면 강제 재생 (트리거 대신 직접 재생)
        if (animator != null)
        {
            animator.Play("Hit", -1, 0f); // 0f = 애니메이션 처음부터 재생
        }
        else // 애니메이터 없으면 스프라이트 변경
        {
            spriteRenderer.sprite = hitSprite;
        }

        yield return new WaitForSeconds(stunTime);

        // 애니메이터가 없으면 원래 스프라이트 복구
        if (animator == null)
        {
            spriteRenderer.sprite = originalSprite;
        }

        isKnock = false;

        // 중력 지속 시간 추가 대기
        yield return new WaitForSeconds(gravityDuration - stunTime);
        rb.gravityScale = 0f;
    }
}
using UnityEngine;
using System.Collections;

public class TigerBossSkill : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    public static float TigerBossAttack = 1f;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.left;
    private TigerBossskillspawner spawner;
    private Animator animator;

     private GameObject player;
    private Player playerScript;

    bool IsShooting;

    public float skillIncrease = 0.5f;
    public float growthDuration = 0.5f;
    private bool isInitialized = false;

    void Start()
    {
        // 플레이어 위치 가져오기
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        //플레이어 위치 - 스킬 생성 위치를 계산하면, 스킬이 플레이어를 향하는 방향 벡터가 나온다.
        moveDirection = (playerScript.transform.position - transform.position).normalized;

        // 스킬 스포너 참조 
        spawner = FindAnyObjectByType<TigerBossskillspawner>();

        //리지드 바디 사용
        rb = GetComponent<Rigidbody2D>();

        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>(); 
        IsShooting = false;

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다. Rigidbody2D를 추가하세요.");
            return;
        }
        StartCoroutine(GrowEffect());
    }

    public void BossSkill()
    {
        animator.SetBool("IsShooting", true);
        rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
    }

    IEnumerator GrowEffect()
    {
        Vector2 originalScale = transform.localScale;
        Vector2 targetScale = originalScale + new Vector2(skillIncrease, skillIncrease);
        float elapsedTime = 0;

        while (elapsedTime < growthDuration)
        {
            transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / growthDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = targetScale;
        ActivateSkill();
    }

    void ActivateSkill()
    {
        if (animator != null)
        {
            animator.SetBool("IsShooting", true);
        }
        rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
        isInitialized = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInitialized) return;
        
        if (other.CompareTag("LeftWall") || other.CompareTag("RightWall") || other.CompareTag("Player"))
        {
            if (spawner != null)
            {
                spawner.ResetSkillState();
            }
            Destroy(gameObject);
        }
    }
}
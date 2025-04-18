using UnityEngine;

public class GhostFireball : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    public float GhostAttack = 1f;

    private SpriteRenderer spriteRenderer;  // 스프라이트렌더러 사용
    
    private GameObject enemy; // enemy 정보 가져오기
    
    private Enemy enemyscript;

    private Player player;

    private Vector3 moveDirection; // 이동 방향을 저장할 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyscript = enemy.GetComponent<Enemy>();

        // 생성 시점에서 이동 방향을 결정
        if (enemyscript.movingLeft)
        {
            moveDirection = Vector3.left; // 왼쪽으로 이동
        }
        else
        {
            moveDirection = Vector3.right; // 오른쪽으로 이동
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 결정된 방향으로 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // LeftWall 또는 RightWall에 닿았을 때
        if (other.CompareTag("LeftWall") || other.CompareTag("RightWall"))
        {
            Destroy(gameObject);
        }
        // Player에 닿았을 때
        else if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;
using System.Collections;

public class ArrowWeapon : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 10;

    private SpriteRenderer spriteRenderer;  // 스프라이트렌더러 사용
    
    private GameObject player;

    private Player playerScript; // player 스크립트 사용

    private bool isLeftDirection; // 발사 방향 저장
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 좌우반전 스프라이트 
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        isLeftDirection = playerScript.isLeft; // 생성 시 플레이어 방향 저장

        spriteRenderer.flipX = isLeftDirection; // 방향저장장
        Destroy(gameObject, 1f); // 1초후 삭제
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"[Enemy 충돌 확인] → {other.name}, 태그: {other.tag}");
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (playerScript.isLeft == true){
            spriteRenderer.flipX = true;  // 스프라이트 (에셋) 좌
            transform.position += Vector3.left * moveSpeed * Time.deltaTime; // 왼쪽쪽 방향으로 발사
        } else {
            spriteRenderer.flipX = false;  // 스프라이트 (에셋) 좌
            transform.position += Vector3.right * moveSpeed * Time.deltaTime; // 오른쪽 방향으로 발사
        }
         */

        if (isLeftDirection) // 좌
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else // 우
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}

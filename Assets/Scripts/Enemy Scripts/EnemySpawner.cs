using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;  // 프리팹 불러오기
    
    [SerializeField]
    private float posY;  // 유니티 에디터에서 물체 Y 좌표 정하기

    [SerializeField]
    private int enemyAmount;  // 유니티 에디터에서 생성할 물체의 개수 정하기

    [SerializeField]
    private float minX;  // 최소 X 좌표값

    [SerializeField]
    private float maxX;  // 최대 X 좌표값

    private int remainingEnemies;  // 남은 몬스터 수

    private EnemyManager enemyManager; // EnemyManager 참조

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemy();  // 적 스폰
        
        enemyManager = FindFirstObjectByType<EnemyManager>();  // EnemyManager 찾기
        if (enemyManager != null)
        {
            enemyManager.RegisterSpawner(this);  // EnemyManager에 등록
        }
    }

    void SpawnEnemy(){

        remainingEnemies = enemyAmount;  // 초기 몬스터 수 설정
        
        for(int i = 0; i < enemyAmount; i++){
            float randomX = Random.Range(minX, maxX);  // 랜덤 X 좌표 지정

            Vector2 SpawnPos = new Vector2(randomX, posY);  // 스폰 포지션 지정
            GameObject enemy = Instantiate(enemyPrefab, SpawnPos, Quaternion.identity);  // 적 스폰 (적 프리팹, 위치, 회전)
        
            // Enemy 스크립트에 EnemySpawner 참조 전달
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetSpawner(this);
            }

        }
    }
    // 몬스터가 죽을 때 호출될 메서드
    
    public void OnEnemyDied()
    {
        remainingEnemies--;  // 남은 몬스터 수 감소

        if (enemyManager != null)
        {
            enemyManager.OnEnemyDied();  // EnemyManager에 몬스터가 죽었음을 알림
        }
    }

    // 남은 몬스터 수 반환
    public int RemainingEnemies => remainingEnemies;
}

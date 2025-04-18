using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemySpawner> spawners = new List<EnemySpawner>();  // 모든 EnemySpawner를 저장
    private int totalRemainingEnemies;  // 전체 남은 몬스터 수

    public void RegisterSpawner(EnemySpawner spawner)
    {
        spawners.Add(spawner);
        RecalculateRemainingEnemies();
    }
    private void RecalculateRemainingEnemies()
    {
        totalRemainingEnemies = 0;
        foreach (var spawner in spawners)
        {
            totalRemainingEnemies += spawner.RemainingEnemies;
        }
    }

    // 몬스터가 죽을 때 호출
    public void OnEnemyDied()
    {
        RecalculateRemainingEnemies();

        if (totalRemainingEnemies <= 0)
        {
            Debug.Log("All enemies are dead!");
            // 모든 몬스터가 죽었을 때 추가 로직 실행 (예: 포탈 활성화)
        }
    }

    // 전체 남은 몬스터 수 반환
    public int TotalRemainingEnemies => totalRemainingEnemies;
}
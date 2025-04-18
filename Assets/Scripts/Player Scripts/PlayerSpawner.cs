using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerSpawn();
    }

    void PlayerSpawn(){
        Vector2 Pos = new Vector2(-7.5f, 0f);  // 플레이어 스폰 위치 지정
        Instantiate(playerPrefab, Pos, Quaternion.identity);  // 플레이어 스폰 (플레이어 프리팹, 위치, 회전)
    }
}

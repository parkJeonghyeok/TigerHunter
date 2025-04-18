using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bossPrefab;

    [SerializeField] private float posX;

    [SerializeField] private float posY;

    private GameObject spawnedBoss;  // 생성된 보스 저장

    private Vector2 pos;

    public bool isJST2Spawn = false;

    public AudioClip roar;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pos = new Vector2(posX, posY);
        BossSpawn();
    }

    void BossSpawn()
    {
        audioSource.PlayOneShot(roar); 
        spawnedBoss = Instantiate(bossPrefab[0], pos, Quaternion.identity);
        Debug.Log("Boss is Spawned!");
    }

    public void JST2Spawn(){
        if(bossPrefab[1] != null && !isJST2Spawn){
            isJST2Spawn = true;
            Instantiate(bossPrefab[1], pos, Quaternion.identity);
        }
    }
}

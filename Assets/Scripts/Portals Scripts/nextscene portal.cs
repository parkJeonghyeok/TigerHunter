using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    public string transferMapName; // 이동할 맵 이름

    private Player thePlayer;
    private EnemyManager enemyManager;
    public AudioClip usingportal;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (thePlayer == null)
        {
            thePlayer = FindFirstObjectByType<Player>();
        }

        enemyManager = FindFirstObjectByType<EnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 모든 몬스터가 죽었는지 확인
            if (enemyManager != null && enemyManager.TotalRemainingEnemies <= 0)
            {
                Debug.Log("🚪 Portal Triggered!");
                Debug.Log("📦 transferMapName = [" + transferMapName + "]");

                thePlayer.currentMapName = transferMapName;
                Debug.Log("✅ currentMapName 설정 완료: " + thePlayer.currentMapName);

                StartCoroutine(LoadNextScene());
            }
            else
            {
                Debug.Log("❗ 아직 모든 적을 다 죽이지 못했어..!");
            }
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return null; // 한 프레임 대기해서 currentMapName 설정이 반영되게 함
        audioSource.PlayOneShot(usingportal);
        SceneManager.LoadScene(transferMapName);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    private static DeadMenu instance;
    public AudioClip clickingSound;
    private AudioSource audioSource;

    public GameObject CanvasUI;
    public GameObject DeadMenuUI;

    private void Start()
    {
        // AudioSource 안전하게 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioSource 컴포넌트가 자동 추가되었습니다");
        }
    }

    public static DeadMenu Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnClickToMainMenu()
    {
        StartCoroutine(GoToMainMenuDelayed());
    }

    private IEnumerator GoToMainMenuDelayed()
    {
        audioSource.PlayOneShot(clickingSound);
        yield return new WaitForSeconds(0.3f); // 클릭 사운드 재생 대기

        SceneManager.LoadScene("MainMenu");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject eventSystem = GameObject.FindGameObjectWithTag("EventSystem");

        if (player != null) Destroy(player);
        if (eventSystem != null) Destroy(eventSystem);

        if (HUD.Instance != null && HUD.Instance.CanvasUI != null)
            Destroy(HUD.Instance.CanvasUI);
        if (PauseMenu.Instance != null && PauseMenu.Instance.CanvasUI != null)
            Destroy(PauseMenu.Instance.CanvasUI);
        if (InventoryUI.Instance != null && InventoryUI.Instance.CanvasUI != null)
            Destroy(InventoryUI.Instance.CanvasUI);

        Destroy(CanvasUI);
    }

    public void OnClickQuit()
    {
        audioSource.PlayOneShot(clickingSound);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

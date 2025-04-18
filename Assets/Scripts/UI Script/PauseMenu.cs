using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu instance;

    public AudioClip clickingSound;

    private AudioSource audioSource;

    public static PauseMenu Instance{
        get{
            if(null == instance){
                return null;
            }
            return instance;
        }
    }

    public GameObject CanvasUI;

    public GameObject PauseMenuUI;

    public GameObject ControlUI;

    private ShopUI shop;

    private bool isPaused = false;

    private bool isShop = false;  // 플레이어가 store 씬에 진입했는 지 여부

    private void Awake() {  // 싱글톤 구현
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }else{
            Destroy(this.gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        shop = FindAnyObjectByType<ShopUI>();

        if(shop != null){
            isShop = true;
        }else{
            isShop = false;
        }
    }

    private void Start() // 대문자 S로 수정
    {
        // AudioSource 안전하게 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioSource 컴포넌트가 자동 추가되었습니다");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isShop){
            if(Input.GetKeyDown(KeyCode.Escape) && !shop.shopUI.activeSelf){
                if(!isPaused){
                    escape();
                }else{
                    isPaused = false;
                    Time.timeScale = 1f;
                    PauseMenuUI.SetActive(false);
                }
            }            
        }else{
            if(Input.GetKeyDown(KeyCode.Escape)){
                if(!isPaused){
                    escape();
                }else{
                    isPaused = false;
                    Time.timeScale = 1f;
                    PauseMenuUI.SetActive(false);
                }
            }
        }


    }

    private void escape(){
        audioSource.PlayOneShot(clickingSound);
        isPaused = true;
        Time.timeScale = 0f;
        
        if(isPaused){
            PauseMenuUI.SetActive(true);
        }
    }

    public void OnClickBackToGame(){
        audioSource.PlayOneShot(clickingSound);
        PauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void OnClickControl(){
        audioSource.PlayOneShot(clickingSound);
        ControlUI.SetActive(true);
    }

    public void OnClickControlClose(){
        audioSource.PlayOneShot(clickingSound);
        ControlUI.SetActive(false);
    }

    public void OnClickToMainMenu(){
        audioSource.PlayOneShot(clickingSound);
        SceneManager.LoadScene("MainMenu");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
        
        if(player != null){
            Destroy(player);
        }

        if(eventSystem != null){
            Destroy(eventSystem);
        }
        
        Destroy(HUD.Instance.CanvasUI);  // HUD 캔버스 삭제 (다시시작 시 플레이어 HP 연동)
        Destroy(DeadMenu.Instance.CanvasUI);
        Destroy(InventoryUI.Instance.CanvasUI);
        Destroy(CanvasUI);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void OnClickQuit(){
        audioSource.PlayOneShot(clickingSound);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

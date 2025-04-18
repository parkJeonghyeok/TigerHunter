using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject shopUI; // 상점 UI 패널
    public Button closeButton; // 닫힘 버튼 
    private bool isPlayerNear = false;
    public GameObject[] shopPages; // 여러 개의 상점 페이지 저장
    private int currentPage = 0; // 현재 페이지 인덱스
    public AudioClip clickingsound;
    private AudioSource audioSource;
    private bool isShopOpen = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void nextPageBtn(){
        if (currentPage < shopPages.Length - 1) // 마지막 페이지가 아닐 때만 실행
        {
            audioSource.PlayOneShot(clickingsound);
            currentPage++;
            UpdateShopPage();
        }
    }
    public void beforePageBtn(){
        if (currentPage > 0) // 첫 번째 페이지가 아닐 때만 실행
        {
            audioSource.PlayOneShot(clickingsound);
            currentPage--;
            UpdateShopPage();
        }
    }

    void UpdateShopPage()
    {
        // 모든 페이지 비활성화 후 현재 페이지만 활성화
        for (int i = 0; i < shopPages.Length; i++)
        {
            shopPages[i].SetActive(i == currentPage);
        }

        Debug.Log("현재 페이지: " + (currentPage + 1));
    }
    // Update is called once per frame
    void Update()
    {
        // 플레이어가 가까울 때만 스페이스바로 상점 열기
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space)){
            OpenShop();
        }

        // 마우스 클릭 시 거리 상관없이 상점 NPC 클릭하면 상점열기
        if (Input.GetMouseButtonDown(0)){
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject){
                OpenShop();
            }
        }

        // 끄기
        if (closeButton != null){
            closeButton.onClick.AddListener(CloseShop);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isShopOpen){
            CloseShop();
        }
    }

    // 근처 감지지
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            isPlayerNear = true;
        }
    }

    // 상점 오픈S2~
    void OpenShop(){
        audioSource.PlayOneShot(clickingsound);
        isShopOpen = true;
        shopUI.SetActive(true);
        Time.timeScale = 0f; // 게임 일시정지 (선택 사항)
    }

    void CloseShop()
    {
        isShopOpen = false;
        shopUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

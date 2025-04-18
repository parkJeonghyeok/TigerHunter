using UnityEngine;
using UnityEngine.UI;
public class JSB2HealthBar : MonoBehaviour
{
    private Slider mySlider;
    private GameObject jst2;
    private JST2Boss jst2Script;

    public float bossMaxHp = 10f;  // 보스 최대 체력
    private float displayedHp; // 체력바에 표시될 값 (부드럽게 변화)

    void Start()
    {
        jst2 = GameObject.FindGameObjectWithTag("JST2");
        jst2Script = jst2.GetComponent<JST2Boss>();
        displayedHp = jst2Script.hp; // 초기 체력 설정
    }

    private void Awake()
    {   
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        float targetHp = jst2Script.hp / bossMaxHp;
        displayedHp = Mathf.Lerp(displayedHp, targetHp, Time.deltaTime * 5); // 부드럽게 체력 감소
        mySlider.value = displayedHp;
    }
}

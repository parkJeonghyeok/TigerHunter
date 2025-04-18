using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Slider mySlider;
    private GameObject boss;
    private Boss bossscript;

    public float bossMaxHp = 10f;  // 보스 최대 체력
    private float displayedHp; // 체력바에 표시될 값 (부드럽게 변화)

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossscript = boss.GetComponent<Boss>();
        displayedHp = bossscript.hp; // 초기 체력 설정
    }

    private void Awake()
    {   
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        float targetHp = bossscript.hp / bossMaxHp;
        displayedHp = Mathf.Lerp(displayedHp, targetHp, Time.deltaTime * 5); // 부드럽게 체력 감소
        mySlider.value = displayedHp;
    }
}

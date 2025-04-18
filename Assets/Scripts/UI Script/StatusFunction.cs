using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StatusFunction : MonoBehaviour
{
    public enum InfoType { Hp, Attack, Defense, Speed, Coin, Map, Weapon };
    public InfoType type;

    Text myText;
    Slider mySlider;
    private float displayedHp; // 체력바에 표시될 값 (부드럽게 변화)

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void Start()
    {
        displayedHp = Player.Instance.hp; // 초기 체력 설정
    }

    private string currentSceneName = "";
    
    void LateUpdate()
    {
        switch (type){
            case InfoType.Hp:
                float targetHp = Player.Instance.hp / 20f;
                displayedHp = Mathf.Lerp(displayedHp, targetHp, Time.deltaTime * 5); // 부드럽게 체력 감소
                mySlider.value = displayedHp;
                break;
            case InfoType.Attack:
                if (Player.Instance.isSword){
                    myText.text = string.Format("{0:F0}",Player.Instance.swordAttack);
                }
                if (Player.Instance.isArcher){
                    myText.text = string.Format("{0:F0}",Player.Instance.attack);
                }
                if (Player.Instance.isGun){
                    myText.text = string.Format("{0:F0}",Player.Instance.gunAttack);
                }
                break;
            case InfoType.Defense:
                myText.text = string.Format("{0:F0}",Player.Instance.defend);
                break;
            case InfoType.Speed:
                myText.text = string.Format("{0:F0}",Player.Instance.moveSpeed);
                break;
            case InfoType.Coin:
                myText.text = string.Format("{0:F0}",Player.Instance.coin);
                break;
            case InfoType.Map:
                Scene scene = SceneManager.GetActiveScene();
                if (currentSceneName != scene.name) // 씬 이름이 변경될 때만 업데이트
                {
                    currentSceneName = scene.name;
                    if (currentSceneName[1] == '1'){
                        if (currentSceneName[3] == 's'){
                            currentSceneName = "상점";
                        } else {
                        currentSceneName = "폐허가 된 마을";
                        }
                    } else if (currentSceneName[1] == '2'){
                        if (currentSceneName[3] == 's'){
                            currentSceneName = "상점";
                        } else {
                            currentSceneName = "산초입";
                        }
                    } else if (currentSceneName[1] == '3'){
                        if (currentSceneName[3] == 's'){
                            currentSceneName = "상점";
                        } else {
                            currentSceneName = "산 깊숙한 곳";
                        }
                    } else if (currentSceneName[1] == '4'){
                        if (currentSceneName[3] == 's'){
                            currentSceneName = "상점";
                        } else {
                            currentSceneName = "호랑이의 굴";
                        }
                    } else if (currentSceneName[1] == '5'){
                        if (currentSceneName[3] == 's'){
                            currentSceneName = "상점";
                        } else {
                            currentSceneName = "???";
                        }
                    }
                    myText.text = currentSceneName;
                }
                break;
            case InfoType.Weapon:
                if (Player.Instance.isSword){
                    myText.text = "칼";
                } else if (Player.Instance.isArcher){
                    myText.text = "활";
                } else if (Player.Instance.isGun){
                    myText.text = "총";
                }

                break;
        }
    }

}

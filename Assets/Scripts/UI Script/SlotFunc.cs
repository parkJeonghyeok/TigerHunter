using UnityEngine;

public class SlotFunc : MonoBehaviour
{
    private Player playerScript;

    public Itemdata item;
    // 플레이어의 최대 체력
    private float playerMaxHealth = 20f;
    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerScript = player.GetComponent<Player>();

        playerScript = FindAnyObjectByType<Player>();
        //playerScript = Player.instance;
    }
    public void SetItem(Itemdata newItem){
        item = newItem;
    }
    public void Onclick(){
        if (playerScript == null)
        {
            Debug.LogError("❌ playerScript가 설정되지 않았습니다!");
            return;
        }

        if (item == null)
        {
            Debug.LogError("❌ item이 설정되지 않았습니다!");
            return;
        }
        // 포션 온클릭 
        if(item.Type == Itemdata.itemType.Potion){
            playerScript.hp += item.HealingAdd;
            // 최대체력을 넘지 않도록.
            if (playerScript.hp >= playerMaxHealth){
                playerScript.hp = playerMaxHealth;
            }
            Debug.Log("체력 회복" + " + " + item.HealingAdd);
        }
        // 그 외 
    }
}

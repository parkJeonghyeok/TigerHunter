using UnityEngine;
using UnityEngine.UI;

public class ShopFunction : MonoBehaviour
{
    [SerializeField]
    private Button buy;

    private InventoryUI inventory; // 인벤토리 참조

    public Itemdata item_1;
    public Itemdata item_2;
    public Itemdata item_3;
    private float MaxHp = 20f; 
    public AudioClip clickingsound;
    public AudioClip purchase;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Awake()
    {
        inventory = InventoryUI.Instance; // 싱글톤 사용!

        if (inventory == null){
            Debug.LogError("❌ InventoryUI 싱글톤이 없습니다! 씬에서 InventoryUI 오브젝트가 활성화되어 있는지 확인하세요.");
        }
        else{   
            Debug.Log("✅ InventoryUI 찾음!");
        }   
    }

    public void OnClick1_item(){ // straw shoes
        audioSource.PlayOneShot(clickingsound);
        OnClickButtonWithParameters(1);
    }
 
    public void OnClick2_item(){ // unknown
        audioSource.PlayOneShot(clickingsound);
        OnClickButtonWithParameters(2);
    }

    public void OnClick3_item(){ // sword
        audioSource.PlayOneShot(clickingsound);
        OnClickButtonWithParameters(3);
    }

    

    void OnClickButtonWithParameters(int num){
        buy.onClick.RemoveAllListeners();
        switch (num){
            case 1:
                audioSource.PlayOneShot(clickingsound);
                buy.onClick.AddListener(()=>BuyEvent(item_1));
                break;
            case 2:
                audioSource.PlayOneShot(clickingsound);
                buy.onClick.AddListener(()=>BuyEvent(item_2));
                break;
            case 3:   
                audioSource.PlayOneShot(clickingsound);
                buy.onClick.AddListener(()=>BuyEvent(item_3));
                break;
        }
    }

    void BuyEvent(Itemdata item)
    {
        // 널인경우 처리
        if (item == null){
            Debug.Log("Unknown Item!!");
            return;
        }

        if (Player.Instance.coin >= item.price) {
            Debug.Log(item.itemName + " is buy!");
            if (item.Type != Itemdata.itemType.Potion){
                inventory.AddItem(item); // 인벤토라에 아이템 추가 
            } else {
                if (Player.Instance.hp + item.HealingAdd > 20){
                    Player.Instance.hp = MaxHp;  
                } else {
                    Player.Instance.hp += item.HealingAdd;
                    Debug.Log(Player.Instance.hp);
                }
            }

            // 코인 깎기기
            audioSource.PlayOneShot(purchase);
            Player.Instance.coin -= item.price;
            
            // 아이템 효과
            if (item.Type == Itemdata.itemType.SpeedItem){
                Player.Instance.moveSpeed += item.moveSpeedAdd;
                Player.Instance.jumpPower += item.jumpSpeedAdd;
            }
            if (item.Type == Itemdata.itemType.DefendItem){
                Player.Instance.defend += item.defendAdd;    
            }
            if (item.Type == Itemdata.itemType.SwordWeapon){
                Player.Instance.swordAttack += item.attackAdd;
                Player.Instance.isSword = true;
                Player.Instance.isArcher = false;
                Player.Instance.isGun = false;
            }
            if (item.Type == Itemdata.itemType.ArcherWeapon){
                Player.Instance.attack += item.attackAdd;
                Player.Instance.isSword = false;
                Player.Instance.isArcher = true;
                Player.Instance.isGun = false;
            }
            if (item.Type == Itemdata.itemType.GunWeapon){
                Player.Instance.gunAttack += item.attackAdd;
                Player.Instance.isSword = false;
                Player.Instance.isArcher = false;
                Player.Instance.isGun = true;
            }
            if (item.Type == Itemdata.itemType.Allitem){
                Player.Instance.defend += item.defendAdd;
                Player.Instance.moveSpeed += item.moveSpeedAdd;
                Player.Instance.jumpPower += item.jumpSpeedAdd;
            }


            // 포션 적용, 사자마자 인벤토리에 추가 되지 않고 바로 체력회복, 최대체력을 넘기 않게 설정 
            
                
            
            

            Debug.Log("남은 코인: " + Player.Instance.coin);
        } else{
            Debug.Log("돈이 부족합니다: " + item.itemName);
        }
    }


}

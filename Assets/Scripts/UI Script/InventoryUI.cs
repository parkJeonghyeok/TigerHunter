using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public static InventoryUI Instance { get; private set; } // 싱글톤
    public GameObject CanvasUI;
    public GameObject inventoryUI;
    bool inventoryactive = false;
    public List<Itemdata> items  = new List<Itemdata>();
    public List<Button> slot = new List<Button>();
    //public List<SlotFunc> slotItem;
    void Start()
    {
        inventoryUI.SetActive(inventoryactive);
        //slotItem = slot.ConvertAll(button => button.GetComponent<SlotFunc>());
    }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 싱글톤이 있으면 새로 생성된 건 파괴
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)){
            inventoryactive = !inventoryactive;
            inventoryUI.SetActive(inventoryactive);
        }
    }

    public void AddItem(Itemdata newItem){
        Debug.Log($"🔍 AddItem 실행됨: {newItem.itemName}");

        for (int i = 0; i < slot.Count; i++){
            Image slotImage = slot[i].GetComponentInChildren<Image>();

            if (slotImage == null){
                Debug.LogError($"❌ 슬롯 {i}의 Image 컴포넌트를 찾을 수 없습니다!");
                continue;
            }

            if (items.Count > i) // 이미 아이템이 있는 슬롯이면 건너뜀
            {
                continue;
            }

            items.Add(newItem);
            //slotItem[i].SetItem(newItem);
            slotImage.sprite = newItem.itemSprite;
            Debug.Log($"✅ 인벤토리에 추가됨: {newItem.itemName}");
            return;
        }

        

        Debug.Log("❌ 빈 슬롯이 없습니다!");
    }
    
    public void RemoveItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slot.Count){
            return;
        } 

        Image slotImage = slot[slotIndex].GetComponentInChildren<Image>();
        //Text slotText = slot[slotIndex].GetComponentInChildren<Text>();

        slotImage.sprite = null; // 이미지 제거
        //slotText.text = ""; // 텍스트 제거
        if (slotIndex < items.Count){
            items.RemoveAt(slotIndex);
        }
        Debug.Log("아이템 제거됨, 슬롯: " + slotIndex);
    }


}

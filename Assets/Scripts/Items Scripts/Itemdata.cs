using UnityEngine;
[System.Serializable]
public class Itemdata
{   
    public enum itemType {SpeedItem, DefendItem, Potion, SwordWeapon, ArcherWeapon, Allitem, GunWeapon};
    public itemType Type; // 아이템 타입
    public string itemName; // 아이템 이름
    public Sprite itemSprite; // 아이템 이미지
    public int price; // 아이템 코인인
    public float attackAdd;
    public float defendAdd;
    public float moveSpeedAdd;
    public float jumpSpeedAdd;

    // 포션일 경우에만 설정
    public float HealingAdd;
}

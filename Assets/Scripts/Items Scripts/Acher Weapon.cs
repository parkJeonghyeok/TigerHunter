using UnityEngine;

public class ArcherWeapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f; // 화살 이동 속도

    [SerializeField]
    private GameObject Weapon; // 화살 프리팹

    [SerializeField]
    private Transform shootTransform; // 화살이 생성될 위치

    [SerializeField]
    private float shootInterval = 0.5f; // 연속 발사 간격 (초)

    private float lastShotTime = -999f; // 초기화 (항상 첫 발은 바로 나가도록 설정)
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && Time.time >= lastShotTime + shootInterval) // a키를 눌렀을때 발사 & 한번에 한발씩만 
        {
            Shoot(); // Shoot 메소드 실행
        }
    }

    void Shoot()
    {
        lastShotTime = Time.time; // 현재 시간을 저장하여 딜레이 체크
        Instantiate(Weapon, shootTransform.position, Quaternion.identity); // 활의 무기인 화살을 생성 (활, 스폰 위치, 회전전)
    }
} 

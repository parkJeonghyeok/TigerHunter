using UnityEngine;

public class mobSkill : MonoBehaviour
{
    [SerializeField]
    private float shootterm;

    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private Transform shootTransform; // 스킬이 생성될 위치

    [SerializeField]
    private GameObject GhostSkill; // 스킬 프리팹

    [SerializeField]
    private float shootInterval = 1.5f; // 연속 발사 간격 (초)

    private float lastShotTime = 0f; // 초기화 (항상 첫 발은 바로 나가도록 설정)

    public AudioClip shootskill;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 초기화 코드가 필요하다면 여기에 추가
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        // shootInterval 시간이 지났는지 확인
        if (Time.time - lastShotTime > shootInterval)
        {
            // shootTransform.position의 y값을 조절
            Vector3 spawnPosition = shootTransform.position;
            spawnPosition.y -= 1.2f; // 예시: y값을 1만큼 증가시킴 (원하는 값으로 조절 가능)

            // 조절된 위치에 GhostSkill 생성
            audioSource.PlayOneShot(shootskill);  
            Instantiate(GhostSkill, spawnPosition, Quaternion.identity);

            // 현재 시간을 저장하여 딜레이 체크
            lastShotTime = Time.time;
        }
    }
}
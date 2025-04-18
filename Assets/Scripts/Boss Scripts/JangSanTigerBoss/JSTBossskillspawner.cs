using UnityEngine;

public class JSTBossskillspawner : MonoBehaviour
{
    [SerializeField]
    private float shootTerm;
    
    [SerializeField]
    public Transform shootTransform; // 스킬이 생성될 위치

    [SerializeField]
    private GameObject JSTBossSkill; // 스킬 프리팹

    [SerializeField]
    private float skillTime;
    private float startTime;
    public bool skill = false;
    public AudioClip skillsound;
    private AudioSource audioSource;
    void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        startTime = Time.time;
    }
    void Update()
    {
        float elabsedTime = Time.time - startTime;
        if(elabsedTime >= skillTime && !skill){
            audioSource.PlayOneShot(skillsound);
            Shoot();
            skill = true;
            startTime = Time.time;
        }
        if (Boss.IsBossDead){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(JSTBossSkill, shootTransform.position, Quaternion.identity);
    }
}

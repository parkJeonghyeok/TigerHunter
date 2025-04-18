using UnityEngine;

public class TigerBossskillspawner : MonoBehaviour
{
    [SerializeField] private float shootTerm = 5f;
    [SerializeField] public Transform shootTransform;
    [SerializeField] private GameObject TigerBossSkill;
    [SerializeField] private float skillTime = 10f;
    
    private float startTime;
    public bool skill = false;
    private Boss Boss;
    public AudioClip skillsound;
    private AudioSource audioSource;

    void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        startTime = Time.time;
        Boss = GetComponent<Boss>();
        if (Boss == null)
        {
            Debug.LogError("MotionBoss component not found!");
        }
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;
        if(elapsedTime >= skillTime && !skill)
        {
            if (Boss != null) Boss.UseSkill();
            audioSource.PlayOneShot(skillsound);
            Shoot();
            skill = true;
            startTime = Time.time;
        }
        if(Boss.IsBossDead){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(TigerBossSkill, shootTransform.position, Quaternion.identity);
    }

    public void ResetSkillState()
    {
        skill = false;
    }
}
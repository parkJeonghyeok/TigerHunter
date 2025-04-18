using UnityEngine;

public class FoxBossSkillSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject skillPrefab;

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

        if(elabsedTime >= skillTime && !skill && !Boss.IsBossDead){
            audioSource.PlayOneShot(skillsound);
            SkillSpawner();
            skill = true;
            startTime = Time.time;
        }
    }

    private void SkillSpawner(){
        Vector2 skillPos = new Vector2(4.5f, 0.5f);
        Instantiate(skillPrefab, skillPos, Quaternion.identity);
    }
}

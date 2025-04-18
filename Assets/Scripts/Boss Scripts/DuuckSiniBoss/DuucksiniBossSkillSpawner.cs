using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DuucksiniBossSkillSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject skillPrefab;  // Skill 프리팹 가져오기

    [SerializeField]
    private GameObject skillWarner;  // 빨간색 사각형으로 스킬이 올라올 곳을 경고

    [SerializeField]
    private float skillWarnerTime = 1;  // 스킬 경고 후에 스킬이 올라올 때까지의 시간 (skillTime 보다 작아야 함)

    [SerializeField]
    private float skillPosY;

    [SerializeField]
    private float skillTime;  // 시작 후 스킬이 나올 때까지의 시간 (다음 스킬과의 텀이 되기도 함)

    private GameObject player;

    private Player playerScript;
    
    private Vector2 skillPos;

    private float startTime;  // 게임 시작 시간

    private bool skill = false;  // 스킬이 나오고 있는지 판별
    public AudioClip skillsound;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        startTime = Time.time;
    }

    
    void Update()
    {   
        float elabsedTime = Time.time - startTime;

        if(!Boss.IsBossDead && !skill && elabsedTime >= skillTime){  // 게임 시작 시간으로부터 skillTime 만큼 지나면 스킬 발동
            skill = true;
            startTime = Time.time;
            SkillWarnSpawner();
        }
    }

    void SkillWarnSpawner(){
        GameObject destroySkillWarner;
        skillPos = new Vector2(playerScript.transform.position.x, skillPosY);
        destroySkillWarner = Instantiate(skillWarner, skillPos, Quaternion.identity);
        Destroy(destroySkillWarner, skillWarnerTime);
        StartCoroutine(SkillSpawner(skillWarnerTime));
    }

    IEnumerator SkillSpawner(float Delay){
        yield return new WaitForSeconds(Delay);
        audioSource.PlayOneShot(skillsound);
        Instantiate(skillPrefab, skillPos, Quaternion.identity);
        skill = false;
    }

}

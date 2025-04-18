using System.Collections;
using UnityEngine;

public class FoxBossSkill : MonoBehaviour
{
    [SerializeField]
    private float skillForce;

    [SerializeField]
    private float skillMoveTime;

    private Rigidbody2D rigid;

    private FoxBossSkillSpawner skillSpawnerScript;

    private Player playerScript;

    private Vector2 moveDirection;

    void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        skillSpawnerScript = FindAnyObjectByType<FoxBossSkillSpawner>();
        rigid = GetComponent<Rigidbody2D>(); 

        moveDirection = (playerScript.transform.position - transform.position).normalized;

        SkillMover();
    }

    void SkillMover(){
        rigid.AddForce(moveDirection * skillForce, ForceMode2D.Impulse);
        StartCoroutine(comeback());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss") || other.CompareTag("Player") || other.CompareTag("LeftWall") || other.CompareTag("RightWall") || other.CompareTag("Ground")){
            Destroy(gameObject);
            skillSpawnerScript.skill = false;
        }
    }

    IEnumerator comeback(){
        yield return new WaitForSeconds(skillMoveTime);
        
        rigid.linearDamping = 0f;
        rigid.linearVelocity = Vector2.zero;
        Vector2 ForceRight = Vector2.right * skillForce;
        rigid.AddForce(ForceRight, ForceMode2D.Impulse);
    }

}

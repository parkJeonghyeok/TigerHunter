using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JST2Boss : MonoBehaviour
{
    [SerializeField]
    public float hp = 10f;

    [SerializeField]
    private float skillTime;

    [SerializeField]
    private float skillForce;

    [SerializeField]
    private float moveTime;

    [SerializeField]
    public float attack;

    private Player player;

    private BossSpawner spawner;

    private Animator animator;

    private Rigidbody2D rigid;

    private SpriteRenderer spriteRenderer;

    private bool skill;

    public static bool isJST2Dead; // 정적 변수로 보스의 죽음 상태를 관리

    private float startTime;
    public AudioClip hurt;
    public AudioClip bossskill;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        isJST2Dead = false;
        player = FindAnyObjectByType<Player>();
        spawner = FindAnyObjectByType<BossSpawner>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }

    void Update()
    {
        float elabsedTime = Time.time - startTime;

        if(elabsedTime >= skillTime && !skill){
            Skill();
            startTime = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon")){
            audioSource.PlayOneShot(hurt);
            hp -= Player.Instance.attack;

            if (hp <= 0){
                bossDead();
            }
        }
        if (other.gameObject.CompareTag("Player")){
            audioSource.PlayOneShot(hurt);
            hp -= Player.Instance.swordAttack;

            if (hp <= 0){
                bossDead();
            }
        }
        if (other.gameObject.CompareTag("Bullet")){
            audioSource.PlayOneShot(hurt);
            hp -= Player.Instance.gunAttack;

            if (hp <= 0){
                bossDead();
            }
        }
        
        if(other.gameObject.CompareTag("LeftWall")){
            Vector2 moveDirRight = Vector2.right * skillForce;
            rigid.linearVelocity = Vector2.zero;
            spriteRenderer.flipX = true;
            rigid.AddForce(moveDirRight, ForceMode2D.Impulse);

        }

        if(other.gameObject.CompareTag("RightWall")){
            rigid.linearVelocity = Vector2.zero;
            spriteRenderer.flipX = false;
            skill = false;
            animator.SetBool("skill", skill);
        }
    }
    void bossDead(){
        isJST2Dead = true; // 보스가 죽으면 상태를 true로 설정
        spawner.isJST2Spawn = false;
        Debug.Log("boss is dead");
        Destroy(gameObject);
        Destroy(player.gameObject);
        SceneManager.LoadScene("Ending");
        Destroy(HUD.Instance.CanvasUI);
        Destroy(PauseMenu.Instance.CanvasUI);
        Destroy(DeadMenu.Instance.CanvasUI);
        Destroy(InventoryUI.Instance.CanvasUI);
    }

    void Skill(){
        skill = true;
        animator.SetBool("skill" , skill);
        audioSource.PlayOneShot(bossskill);
        Vector2 moveDirLeft = Vector2.left * skillForce;
        rigid.AddForce(moveDirLeft, ForceMode2D.Impulse);
    }
}

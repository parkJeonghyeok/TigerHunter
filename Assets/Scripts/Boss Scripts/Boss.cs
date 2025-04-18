using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] public float hp = 10f;
    [SerializeField] public float bossBodyDamage = 1f;
    [SerializeField] public float bossAttack = 1f;
    public static bool IsBossDead;

    [Header("Animation Settings (Only for Tigerboss)")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip normalAnimation;
    [SerializeField] private AnimationClip skillAnimation;
    [SerializeField] private string skillTriggerName = "IsUsingSkill";
    [SerializeField] private float skillTime;

    private GameObject player;
    private Player playerScript;
    private BossSpawner spawner;
    private Scene scene;
    public AudioClip hurt;
    private AudioSource audioSource;
    private bool isTigerBoss => gameObject.name == "boss 4";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
        spawner = FindAnyObjectByType<BossSpawner>();
        scene = SceneManager.GetActiveScene();

        IsBossDead = false;

        if (isTigerBoss)
        {
            InitializeComponents();
        }
    }

    void InitializeComponents()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!", this);
                return;
            }
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }

        ForceInitialAnimation();
    }

    void ForceInitialAnimation()
    {
        if (animator != null && normalAnimation != null)
        {
            animator.Play(normalAnimation.name, 0, 0f);
            Debug.Log($"초기 애니메이션 강제 재생: {normalAnimation.name}");
        }
    }

    public void PlayNormalAnimation()
    {
        if (animator != null)
        {
            animator.SetBool(skillTriggerName, false);
            Debug.Log($"기본 애니메이션 전환: {normalAnimation.name}");
        }
    }

    public void PlaySkillAnimation()
    {
        if (animator != null)
        {
            animator.SetBool(skillTriggerName, true);
            Debug.Log($"스킬 애니메이션 전환: {skillAnimation.name}");
        }
    }

    public IEnumerator SkillRoutine()
    {
        PlaySkillAnimation();
        yield return new WaitForSeconds(skillTime);
        PlayNormalAnimation();
    }

    public void UseSkill()
    {
        if (isTigerBoss && gameObject.activeInHierarchy && animator != null)
        {
            StartCoroutine(SkillRoutine());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 활
        if (other.gameObject.CompareTag("Weapon"))
        {
            audioSource.PlayOneShot(hurt);
            hp -= Player.Instance.attack;
            if (hp <= 0)
            {
                bossDead();
            }
        }

        // 칼   
        if (other.gameObject.CompareTag("Player") && Player.Instance.isSword)
        {
            audioSource.PlayOneShot(hurt);
            hp -= Player.Instance.swordAttack;
            
            if (hp <= 0)
            {
                bossDead();
            }
        }
        // 총 
        if (other.gameObject.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(hurt);
            hp -= Player.Instance.gunAttack;
            if (hp <= 0)
            {
                bossDead();
            }
        }

    }

    void bossDead(){
        IsBossDead = true;
        
        Debug.Log("boss is dead");

        if(scene.name == "F5-wave3"){
            spawner.JST2Spawn();
        }
        Destroy(gameObject);
        
        
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (normalAnimation != null && skillAnimation != null)
        {
            skillTriggerName = "IsUsingSkill";
        }
    }
#endif
}

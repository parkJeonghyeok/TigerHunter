/*
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MotionBoss : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator animator;
    
    [Header("Animation Settings")]
    [SerializeField] private AnimationClip normalAnimation;
    [SerializeField] private AnimationClip skillAnimation;
    [SerializeField] private string skillTriggerName = "IsUsingSkill";
    
    [Header("Gameplay Settings")]
    [SerializeField] private float hp = 10f;
    [SerializeField] private float skillTime;
    
    private GameObject player;
    private Player playerScript;

    void Start()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        // Animator 자동 할당
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!", this);
                return;
            }
        }

        // 플레이어 참조 설정
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }

        // 초기 애니메이션 강제 설정
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
        yield return new WaitForSeconds(skillTime); // 스킬 지속 시간
        PlayNormalAnimation();
    }

    public void UseSkill()
    {
        if (gameObject.activeInHierarchy && animator != null)
        {
            StartCoroutine(SkillRoutine());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            hp -= Player.Instance.attack;

            if (hp <= 0)
            {
                HandleBossDefeat();
            }
        }
    }

    #if UNITY_EDITOR
    void OnValidate()
    {
        // 에디터에서 자동 할당
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        // 애니메이션 이름 동기화
        if (normalAnimation != null && skillAnimation != null)
        {
            skillTriggerName = "IsUsingSkill";
        }
    }
    #endif
} */
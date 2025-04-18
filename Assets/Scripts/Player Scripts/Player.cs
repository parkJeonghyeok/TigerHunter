using System;
using System.Collections;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed; // 움직이는 속도

    [SerializeField]
    public float jumpPower; // 점프 힘  

    [SerializeField]
    public float hp = 20f;

    [SerializeField]
    private GameObject Weapon;

    [SerializeField]
    private GameObject bullet;
    //플레이어에서 발사 
    [SerializeField]
    private Transform shootTransform; // 화살이 생성될 위치

    [SerializeField]
    private float shootInterval = 0.5f; // 연속 발사 간격 (초)

    public AudioClip walkingSound;
    private AudioSource audioSource;

    public AudioClip shooting;
    public AudioClip swinging;
    public AudioClip playerhurt;
    public AudioClip pulling;
    public AudioClip gunshot;
    public AudioClip jumping;
    
    public float attack = 1f;  // 공격력
    public float swordAttack = 5f;  // 검 공격력
    public float gunAttack = 0f;
    public float defend = 1f;   // 수비력
    private float lastShotTime = -999f; // 초기화 (항상 첫 발은 바로 나가도록 설정)
    private SpriteRenderer playerSpriteRenderer; // 좌우반전을 위한 스프라이트 렌더러 사용  
    //private SpriteRenderer weaponSpriteRenderer; // WEAPON 좌우반전
    private Animator animator; // 애니메이션 적용
    private Rigidbody2D rigid; // Rigidbody2D 추가
    private bool isGrounded = true; // 땅에 닿았는지     
    private bool isJumping = false; // 점프를 하는지    
    public int coin = 0;
    public string currentMapName;
    private static Player instance;
    public static Player Instance {
        get{
            if(null == instance){
                return null;
            }

            return instance;
        }
    }
    private Enemy enemy;
    private Boss boss;
    private JST2Boss JST2;
    private GhostFireball ghostSkill;
    public bool isLeft = true; // 캐릭터 무기 좌우 감지
    public bool isShooting = false;
    public bool isArcher = true;
    public bool isSword = false;
    public bool isSwing = false;
    public bool isGun = false;
    public bool isGunShooting = false;
    private void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);  // prohibit while changing scene
        } else {
            Destroy(this.gameObject);
        }
    }
    
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>(); // 좌우반전
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        rigid = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
    }

    void Update(){
        bool isMoving = false; // 움직임 감지
        Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        //Jump
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded){ // 윗방향키 누르면 점프
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
            isJumping = true; // 점프 시작
            audioSource.PlayOneShot(jumping);
            animator.SetBool("isJumping", true);
        }

        //Moving
        if (Input.GetKey(KeyCode.LeftArrow)){ // 왼쪽 움직임
            //if (weaponSpriteRenderer != null) weaponSpriteRenderer.flipX = false; // WEAPON도 좌우반전
            playerSpriteRenderer.flipX = false; // 좌우반전 
            transform.position -= moveTo;
            isMoving = true; // 움직임 애니메이션
            if (isMoving) {
        
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(walkingSound);
                }
            } 
            isLeft = true;
        } else if (Input.GetKey(KeyCode.RightArrow)){ // 오른쪽 움직임 
            //if (weaponSpriteRenderer != null) weaponSpriteRenderer.flipX = true; // WEAPON도 좌우반전
            playerSpriteRenderer.flipX = true; // 좌우반전 
            transform.position += moveTo;
            isMoving = true; // 움직임 애니메이션 
            if (isMoving) {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(walkingSound);
                }
            }
            isLeft = false;
        }

        //isLeft = !isLeft;
        
        //플레이어에서 발사 
        if (Input.GetKeyDown(KeyCode.A) && Time.time >= lastShotTime + shootInterval && !isShooting && isArcher) // a키를 눌렀을때 발사 & 한번에 한발씩만 
        {
            isShooting = true;
            animator.SetBool("isShooting", isShooting);
        }
        if (Input.GetKeyDown(KeyCode.A) && Time.time >= lastShotTime + shootInterval && !isGunShooting && isGun){
            isGunShooting = true;
            animator.SetBool("isGunShooting", isGunShooting);
        }
        if(Input.GetKeyDown(KeyCode.A) && !isSwing && isSword){
            isSwing = true;
            animator.SetBool("isSwing", isSwing);
        }

        animator.SetBool("isMoving", isMoving); // 애니메이션 상태 변경
    }

    // 바닥에 닿았는지 확인
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            isJumping = false;
            animator.SetBool("isJumping", false); // 착지하면 점프 애니메이션 해제
        }
/*
        if(collision.gameObject.CompareTag("Coin")){
            coin++;
            Debug.Log(coin);
        } */
    }

    private void OnTriggerEnter2D(Collider2D other){  // 적과 닿았는지 확인 후 HP 감소
        if(other.gameObject.CompareTag("Enemy")){
            if(!isSwing){
                enemy = FindAnyObjectByType<Enemy>();        
                audioSource.PlayOneShot(playerhurt);
                //hp -= (enemy.enemyBodyDamage - (0.1f * defend));
                hp -= (enemy.enemyBodyDamage * (1f / defend));
            }

            if(hp <= 0){
                Dead();  // HP 가 0 이 되면 플레이어 삭제
            }
        }

        if(other.gameObject.CompareTag("GhostSkill")){
            ghostSkill = FindAnyObjectByType<GhostFireball>();
            audioSource.PlayOneShot(playerhurt);
            //hp -= (GhostFireball.GhostAttack - (0.1f * defend));
            //hp -= (GhostFireball.GhostAttack * (1f / defend));
            hp -= (ghostSkill.GhostAttack * (1f / defend));

            if(hp <= 0){
                Dead();
            }
        }

        if(other.gameObject.CompareTag("BossSkill")){
            boss = FindAnyObjectByType<Boss>();    
            audioSource.PlayOneShot(playerhurt);
            //hp -= (RatBossSkill.BossAttack - (0.1f * defend));
            hp -= (boss.bossAttack * (1f / defend));

            if(hp <= 0){
                Dead();
            }
        }

        if(other.gameObject.CompareTag("JST2")){
            JST2 = FindAnyObjectByType<JST2Boss>();
            audioSource.PlayOneShot(playerhurt);
            hp -= (JST2.attack * (1f / defend));

            if(hp <= 0){
                Dead();
            }
        }

        if(other.gameObject.CompareTag("Boss")){
            audioSource.PlayOneShot(playerhurt);
            //hp -= (boss.bossBodyDamage - (0.1f * defend));
            hp -= (boss.bossBodyDamage * (1f / defend));

            if(hp <= 0){
                Dead();
            }
        }

        if(other.gameObject.CompareTag("Coin")){
            coin++;
            Debug.Log(coin);
        }
    }

    void Dead(){
        Destroy(gameObject);
        DeadMenu.Instance.DeadMenuUI.SetActive(true);
    }

    void Shoot()
    {   
        lastShotTime = Time.time; // 현재 시간을 저장하여 딜레이 체크
        Instantiate(Weapon, shootTransform.position, Quaternion.identity); // 활의 무기인 화살을 생성 (활, 스폰 위치, 회전전)
        audioSource.PlayOneShot(shooting);
        isShooting = false;
        animator.SetBool("isShooting", isShooting);
    }

    void Pulling(){
        audioSource.PlayOneShot(pulling);
    }

    void Swing(){
        audioSource.PlayOneShot(swinging);
        isSwing = false;
        animator.SetBool("isSwing", isSwing);
    }

    void GunShoot()
    {   
        lastShotTime = Time.time; // 현재 시간을 저장하여 딜레이 체크
        Instantiate(bullet, shootTransform.position, Quaternion.identity); // 총의 무기인 화살을 생성 (총, 스폰 위치, 회전전)
        audioSource.PlayOneShot(gunshot);
        //audioSource.PlayOneShot(shooting);
        isGunShooting = false;
        animator.SetBool("isGunShooting", isGunShooting);
    }
}



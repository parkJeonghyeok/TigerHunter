using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextstage_Portal : MonoBehaviour
{
    [SerializeField]
    public string transferMapName; // 이동할 맵 이름
    private Player thePlayer;
    public AudioClip usingportal;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        thePlayer = FindFirstObjectByType<Player>();
        if (thePlayer == null)
        {
            Debug.LogError("Player not found!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 보스가 죽었는지 확인 (정적 변수를 사용)
            if (Boss.IsBossDead)
            {
                Debug.Log("portal open");
                thePlayer = FindFirstObjectByType<Player>();
                audioSource.PlayOneShot(usingportal);
                thePlayer.currentMapName = transferMapName; // 플레이어의 현재 맵 이름 업데이트
                SceneManager.LoadScene(transferMapName); // 씬 전환
            }
            else
            {
                Debug.Log("보스가 아직 죽지 않았어..! 이대로 포기하긴 일러!");
            }
        }
    }
}


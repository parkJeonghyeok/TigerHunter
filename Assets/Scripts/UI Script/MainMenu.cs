using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))] // AudioSource 자동 추가 보장
public class MainMenu : MonoBehaviour
{
    public GameObject ControlUI;
    public GameObject StoryScene;
    public AudioClip clickingSound;
    private AudioSource audioSource;

    private void Start() // 대문자 S로 수정
    {
        // AudioSource 안전하게 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioSource 컴포넌트가 자동 추가되었습니다");
        }
    }

    public void OnClickNewGame(){
        PlayClickSound();
        if (StoryScene != null)
        {
            StoryScene.SetActive(true);
            StartCoroutine(StoryDelay(10f));
        }
    }

    public void OnClickControl(){
        PlayClickSound();
        if (ControlUI != null) ControlUI.SetActive(true);
    }

    public void OnClickControlClose(){
        PlayClickSound();
        if (ControlUI != null) ControlUI.SetActive(false);
    }

    public void OnClickQuit(){
        PlayClickSound();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // 클릭 사운드 재생 메서드 (중복 코드 제거)
    private void PlayClickSound()
    {
        if (audioSource != null && clickingSound != null)
        {
            audioSource.PlayOneShot(clickingSound);
        }
        else
        {
            Debug.LogWarning("오디오 소스 또는 클릭 사운드가 설정되지 않았습니다");
        }
    }

    IEnumerator StoryDelay(float Delay){
        yield return new WaitForSeconds(Delay);
        SceneManager.LoadScene("F1-wave1");
    }
}
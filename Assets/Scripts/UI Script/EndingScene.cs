using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    [SerializeField]
    private GameObject Picture;

    private void Start() {
        StartCoroutine(OnPicture(5f));
    }

    IEnumerator OnPicture(float Delay){
        yield return new WaitForSeconds(Delay);
        Picture.SetActive(true);
    }

    public void OnClickToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickQuit(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

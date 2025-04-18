using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potal2 : MonoBehaviour
{
    public string startpoint; // 시작 지점 이름
    private Player thePlayer;
    public AudioClip usingportal;
    private AudioSource audioSource;

    private void Start()
{
    audioSource = GetComponent<AudioSource>();
    if (thePlayer == null)
        thePlayer = FindFirstObjectByType<Player>();

    string playerMap = thePlayer.currentMapName;
    string startPointTrimmed = startpoint.Trim();
    string playerMapTrimmed = playerMap.Trim();

    Debug.Log($"▶ Raw startpoint: [{startpoint}]");
    Debug.Log($"▶ Raw playerMap: [{playerMap}]");
    Debug.Log($"▶ Trimmed startpoint: [{startPointTrimmed}]");
    Debug.Log($"▶ Trimmed playerMap: [{playerMapTrimmed}]");
    Debug.Log($"▶ Char-by-char:");
    for (int i = 0; i < Mathf.Max(startPointTrimmed.Length, playerMapTrimmed.Length); i++)
    {
        char c1 = i < startPointTrimmed.Length ? startPointTrimmed[i] : ' ';
        char c2 = i < playerMapTrimmed.Length ? playerMapTrimmed[i] : ' ';
        Debug.Log($"[{i}] startpoint: '{c1}' ({(int)c1}) / currentMapName: '{c2}' ({(int)c2})");
    }

    if (startPointTrimmed == playerMapTrimmed)
    {
        Debug.Log("✅ 위치 일치! 이동 수행");
        thePlayer.transform.position = transform.position;
        audioSource.PlayOneShot(usingportal);
    }
    else
    {
        Debug.Log("❌ 여전히 위치 불일치!");
    }
}


}
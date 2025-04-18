using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    // 싱글톤 인스턴스
    static public CameraMgr instance;

    // Awake는 스크립트가 로드될 때 호출됩니다.
    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this; // 현재 인스턴스를 싱글톤 인스턴스로 설정
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트 파괴
        }
    }
}
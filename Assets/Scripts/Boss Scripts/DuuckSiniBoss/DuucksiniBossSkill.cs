using UnityEngine;
using System.Collections;  // IEnumerator를 사용하기 위해 추가

public class DuucksiniBossSkill : MonoBehaviour
{
    //[SerializeField]
    //public static float BossAttack = 6f;

    [SerializeField]
    private float skillDuration = 1f; // 스킬 지속 시간

    private void Start()
    {
        // 스킬 지속 시간 후 자동 파괴
        StartCoroutine(DestroyAfterDuration());
    }

    IEnumerator DestroyAfterDuration()
    {
        yield return new WaitForSeconds(skillDuration);
        Destroy(gameObject);
    }
}
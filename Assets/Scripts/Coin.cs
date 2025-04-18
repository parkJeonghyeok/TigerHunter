using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public AudioClip pickingcoin;
    private AudioSource audioSource;

    void Start()
    {
        Jump();
        audioSource = GetComponent<AudioSource>();
    }

    void Jump()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f);
        rigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ground와 충돌했을 때
        if (other.CompareTag("Ground"))
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            // 물리 운동 완전히 멈춤
            rigidbody.linearVelocity = Vector2.zero;
            rigidbody.angularVelocity = 0f;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;

            // Ground의 상단에 정확히 위치시킴
            float groundTop = other.bounds.max.y;
            float coinHalfHeight = GetComponent<Collider2D>().bounds.extents.y;

            transform.position = new Vector3(
                transform.position.x,
                groundTop + coinHalfHeight,
                transform.position.z
            );
        }

        // Player와 충돌했을 때
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin +1!");
            audioSource.PlayOneShot(pickingcoin);
            Destroy(gameObject);
        //    StartCoroutine(DestroyAfterSound());
        }
    }

 //   IEnumerator DestroyAfterSound()
 //   {
//        yield return new WaitForSeconds(pickingcoin.length);
 //       Destroy(gameObject);
//    }
}

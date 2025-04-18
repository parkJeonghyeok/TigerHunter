using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    [SerializeField]
    private float SpeedUp;

    private GameObject player;

    private Player playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();    
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            MoveSpeedUp();
            Destroy(gameObject);
        }
    }

    void MoveSpeedUp(){
        playerScript.moveSpeed += SpeedUp;
    }
}

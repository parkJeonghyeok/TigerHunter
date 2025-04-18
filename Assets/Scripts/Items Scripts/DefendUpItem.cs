using UnityEngine;

public class DefendUpItem : MonoBehaviour
{
    [SerializeField]
    private float DefendUp;

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
            DefendStatUp();
            Destroy(gameObject);
        }
    }

    void DefendStatUp(){
        playerScript.defend += DefendUp;
    }
}

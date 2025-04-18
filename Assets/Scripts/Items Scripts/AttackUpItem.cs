using UnityEngine;

public class AttackUpItem : MonoBehaviour
{
    [SerializeField]
    private float AttackUp;

    void OnTriggerEnter2D(Collider2D other){  
        if(other.gameObject.CompareTag("Player")){
            AttackPowerUp();
            Destroy(gameObject);
        }
    }

    void AttackPowerUp(){
        Player.Instance.attack += AttackUp;
    }
}

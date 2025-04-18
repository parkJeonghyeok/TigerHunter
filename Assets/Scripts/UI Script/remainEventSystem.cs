using UnityEngine;

public class remainEventSystem : MonoBehaviour
{
    private remainEventSystem instance;

    public remainEventSystem Instance{
        get{
            if(null == instance){
                return null;
            }

            return instance;
        }
    }

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }


}

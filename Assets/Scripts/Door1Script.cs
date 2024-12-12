using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
    [SerializeField]
    private string requiredKey = "1";
    private float openingTime = 3f;
    private float timeout =0f;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Character"){

            if(GameState.collectedItems.Keys.Any(k => k == "Key1")){
                GameState.TriggerGameEvent("Door1", 
                    new GameEvents.MessageEvent{
                        message = "Двері відчиняються",
                        data = requiredKey
                    });
                timeout = openingTime;
            }
            else{
                GameState.TriggerGameEvent("Door1", 
                 new GameEvents.MessageEvent{
                        message = "Для відкривання двері небхідно знайти ключ " +requiredKey,
                        data = requiredKey
                    });
            }            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeout > 0){
            this.transform.Translate(Time.deltaTime/openingTime,0,0);
            timeout -= Time.deltaTime;
        }
    }
}

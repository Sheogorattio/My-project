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

    private bool isOpen = false;

    private AudioSource closedSound;
    private AudioSource openedSound;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Character" && !this.isOpen){

            if(GameState.collectedItems.Keys.Any(k => k == "Key" + this.requiredKey)){
                GameState.TriggerGameEvent("Door1", 
                    new GameEvents.MessageEvent{
                        message = "Двері відчиняються",
                        data = requiredKey
                    });
                timeout = openingTime;
                this.openedSound.Play();
                this.isOpen = true;
            }
            else{
                GameState.TriggerGameEvent("Door1", 
                 new GameEvents.MessageEvent{
                        message = "Для відкривання двері небхідно знайти ключ " +requiredKey,
                        data = requiredKey
                    });
                    this.closedSound.Play();
            }            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        this.closedSound = audioSources[0];
        this.openedSound = audioSources[1];
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

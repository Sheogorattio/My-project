using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light _light;
    private float charge;
    private readonly float worktime = 45f;

    public float part;

    private AudioSource halfEmptySound;
    private bool isHalfEmpty = false;
    private AudioSource thirtyPercentsEmptySound;
    private bool isThirtyPercentsEmpty = false;


    private GameObject capacityImage;
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent;
        if(parentTransform == null){
            Debug.LogWarning("FlashLightScript: parentTransform is null");
        }
        charge = 1f;
        part = charge;
        _light = GetComponent<Light>();
        capacityImage = GameObject.Find("CapacityImage");
    
        GameState.Subsribe(OnRecharge, "Recharging");

        this.halfEmptySound = GetComponents<AudioSource>()[0];
        this.thirtyPercentsEmptySound = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if(parentTransform == null) return;

        if(charge>0 && !GameState.isDay){
            _light.intensity = charge;
            charge -= Time.deltaTime/worktime;
            part = charge;
        } 
        if(charge <= 0.3f && charge >= 0f && !this.isThirtyPercentsEmpty){
            this.thirtyPercentsEmptySound.Play();
            this.isThirtyPercentsEmpty = true;
        } 
        if(charge <= 0.5f && charge >= 0.2f && !this.isHalfEmpty) {
            this.halfEmptySound.Play();
            this.isHalfEmpty = true;
        }

        if(GameState.isFpv){
            transform.forward = Camera.main.transform.forward;
        }
        else{
            Vector3 forward= Camera.main.transform.forward;
            forward.y=0.0f;
            if(forward == Vector3.zero) forward = Camera.main.transform.up;
            transform.forward = forward.normalized;
        }
      
    }

    private void OnRecharge(string eventName, object addLevel){
        if( addLevel is float){
            charge += (float)addLevel;
            part = charge > 1f ? 1f : charge;
            if(charge >= 0.3f){
                this.isThirtyPercentsEmpty = false;               
            }
            if(charge >= 0.5f){
                this.isHalfEmpty = false;
            }
            GameState.TriggerGameEvent("Broadcast", new GameEvents.MessageEvent(){
                message = "Батарею дозаряджено на +" + ((float)addLevel *100)+ "%",
                data = addLevel
            });
            Debug.Log(eventName + " level" + addLevel);
        }
        else{
            Debug.LogError("OnRecharge: addLevel is not a float");
        }
        
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnRecharge, "Recharging");
    }
}

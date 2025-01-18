using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private float timeout  = 25;
    private float leftTime;

    private AudioSource collectedSound;

    private float destroyTimout =0;

    public float part;
    // Start is called before the first frame update
    void Start()
    {
        part = 1;
        leftTime = timeout;
        collectedSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leftTime >0){
            leftTime -= Time.deltaTime;
            part = leftTime/timeout;
            if(leftTime <= 0){                
                leftTime = 0;
                Destroy(gameObject);
            }
        }
        if(this.destroyTimout>0){
            this.destroyTimout -= Time.deltaTime;
            if(this.destroyTimout <= 0){
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Character"){
            GameState.collectedItems.Add("Key"+keyPointName, part);
            GameState.TriggerGameEvent("KeyPoint", 
            new GameEvents.MessageEvent(){
                message = "Ключ підібрано",
                data = part
            });
            this.collectedSound.Play();
           this.destroyTimout = .3f;
        }
    }
}

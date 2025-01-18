using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    [SerializeField]
    private float batteryLevel=1;
    private Collider _collider;
    private bool isColliding;

    private float destroyTimeout =0;

    private AudioSource collectSound;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        collectSound = GetComponent<AudioSource>();

    }

    void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.tag == "Character") {
            _collider.enabled = false;
            GameState.TriggerGameEvent("Recharging", batteryLevel);
            this.collectSound.Play();
            destroyTimeout = .3f;
        }
    }
    // Update is called once per frame
    void Update()
    {   if(destroyTimeout > 0){
            destroyTimeout -= Time.deltaTime;
            if(destroyTimeout <= 0){
                Destroy(this.gameObject);
            }
        }
    }
}

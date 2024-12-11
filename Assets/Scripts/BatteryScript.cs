using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    private Collider _collider;
    private bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();

    }

    void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.tag == "Character") {
             
             isColliding = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isColliding){
            Debug.Log("COLLIDING");
            GameState.isRecharge = true;
            this.gameObject.SetActive(false);
        }
    }
}
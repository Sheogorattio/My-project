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

    private GameObject capacityImage;
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent;
        if(parentTransform == null){
            Debug.LogWarning("FlashLightScript: parentTransform is null");
        }
        charge = 1f;
        _light = GetComponent<Light>();
        capacityImage = GameObject.Find("CapacityImage");
    }

    // Update is called once per frame
    void Update()
    {
        if(parentTransform == null) return;

        if(GameState.isRecharge){
            charge += 1f;
            GameState.isRecharge = false;
            part = charge >1f ? 1f : charge;
        }

        if(charge>0 && !GameState.isDay){
            _light.intensity = charge;
            charge -= Time.deltaTime/worktime;
            part = charge;
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
}

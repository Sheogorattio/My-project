using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightScript : MonoBehaviour
{
    private Light[] dayLights;
    private Light[] nightLights;

    private AudioSource switchSound;
    
    // Start is called before the first frame update
    void Start()
    {
        dayLights = GameObject.FindGameObjectsWithTag("Day light").Select(g => g.GetComponent<Light>()).ToArray();
        nightLights = GameObject.FindGameObjectsWithTag("Night light").Select(g => g.GetComponent<Light>()).ToArray();
        switchLight();
        this.switchSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.L)){
            switchLight();
            this.switchSound.Play();
        }
    }

    private void switchLight() {
        GameState.isDay = !GameState.isDay;
        foreach (Light light in dayLights) {
            light.enabled = GameState.isDay;
        }
        foreach (Light light in nightLights) {
            light.enabled = !GameState.isDay;
        }
    }
}

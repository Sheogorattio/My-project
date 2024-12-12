using UnityEngine;
using UnityEngine.UI;

public class FlashlightChargeImageScript : MonoBehaviour
{
    private FlashLightScript flashLightScript;
    private Image image;
    void Start(){
        image = GetComponent<Image>();
        flashLightScript = GameObject.FindAnyObjectByType<FlashLightScript>();

        if(flashLightScript == null){
            Debug.LogError("FlashlightChargeImageScript: FlashLightScript is not found");
        }
        
    }

    void Update(){
        if(flashLightScript != null){
            image.fillAmount = flashLightScript.part;
            image.color = new Color(
                1- image.fillAmount,
                image.fillAmount,
                0.2f
            );
        }
        else{
            Debug.Log("FlashlightChargeImageScript: FlashLightScript is null");
        }
    }
}
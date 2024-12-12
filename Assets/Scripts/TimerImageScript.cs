using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerImageScript : MonoBehaviour
{
    private Image image;
    private KeyPointScript keyPointScript;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        Transform t = this.transform;
        while (t  != null && 
                (keyPointScript = t.gameObject.GetComponent<KeyPointScript>()) == null)
        {
            t= t.parent;
        }

        if(keyPointScript == null){
            Debug.LogError("TimerImageScript: KeyPointScript not found in parent");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(keyPointScript != null){
            image.fillAmount = keyPointScript.part;
            image.color = new Color(
                1- image.fillAmount,
                image.fillAmount,
                0.2f
            );
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Script : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Character"){
            ToastScript.ShowToast(
                "Для відкривання двері вам необхідо знайти ключ №1 ",
                "Двері"
            );
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

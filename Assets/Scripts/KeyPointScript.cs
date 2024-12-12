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

    public float part;
    // Start is called before the first frame update
    void Start()
    {
        part = 1;
        leftTime = timeout;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftTime >0){
            leftTime -= Time.deltaTime;
            part = leftTime/timeout;
            
            if(leftTime < 0){                
                leftTime = 0;
            }
        }
    }
}

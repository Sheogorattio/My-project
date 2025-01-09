using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private static TMPro.TextMeshProUGUI textMeshPro;

    private static float time;

    private static string formatedTime = "DD:DD";
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float seconds;
        float minutes;
       
        seconds = MathF.Round(time);
        minutes = seconds / 60;
        seconds%=60;
        
        textMeshPro.text =  String.Format("{0:D2}:{1:D2}", (int)minutes, (int)seconds);
    }
}

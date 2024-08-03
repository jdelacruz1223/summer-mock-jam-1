using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public TMP_Text timeText;
    private float currentTime;
    
    void Start()
    {
        InvokeRepeating("Timer", 1f, 1f);
    }

    void Update()
    {
        //timeText.text = currentTime.ToString("F2");
        
    }

    public void Timer()
    {
        Manager managerVar = Manager.GetInstance();
        var timeVar = managerVar.totalTime;
        currentTime = timeVar;

        var t0 = (int) currentTime;
        var m = t0/60;
        var s = t0 - m*60;
        var ms = (int)((currentTime - t0)*100);
        timeText.text = $"{m:00}:{s:00}:{ms:00}";
    }


}

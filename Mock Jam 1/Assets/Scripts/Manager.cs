using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager GetInstance() {return me;}
    public static Manager me;
    public float totalTime { get; private set; }
    public float startTime { get; private set; }
    private bool isTimerRunning;

    void Awake()
    {
        if (me != null)
        {
            Destroy(gameObject);
            return;
        }

        me = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        totalTime = GetTotalTimeElapsed();
    }

    void InitializeGame()
    {
        totalTime = 0;
        startTime = Time.time;
        isTimerRunning = true;
    }


    #region Time
    public float GetTotalTimeElapsed()
    {
        return Time.time - startTime;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void StartTimer()
    {
        if(isTimerRunning)
        {
            startTime = Time.time - GetTotalTimeElapsed();
            isTimerRunning = true; 
        }
    }
    #endregion
}

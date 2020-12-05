using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimedownCounter : MonoBehaviour
{
    public float timeRemaining = 60;
    public Text txt;
    bool stopTime = false;
    bool flag = false;
    public GameObject player;

    void Awake()
    {
        flag = false;
    }
    void Update()
    {
        if (timeRemaining > 0 && !stopTime)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if(timeRemaining <= 0)
        {
            print(flag);
            if (!flag)
            {
                player.SendMessage("StartEndSequence");
                flag = true;
            }
            
        }
        int timeInt = (int)timeRemaining;
        txt.text = timeInt.ToString();
    }

    void SetStopTime()
    {
        stopTime = true;
    }
     void SetContinueTime()
    {
        stopTime = false;
    }
    void ChangeFlag()
    {
        flag = false;
    }
}
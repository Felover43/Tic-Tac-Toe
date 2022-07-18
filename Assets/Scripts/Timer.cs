using UnityEngine;
using UnityEngine.UI;
using System;

/*
 As Timer is a simple property I wanted to keep
timer simple and self relient but very easy to control and access.
It acts as the stop watch with the usage of delta time properties.
 */

public class Timer : MonoBehaviour
{
    bool m_isRunning = true;
    private float m_duration = 5;
    private float m_time;
    public Text m_text;
    Action m_tigger;


    private void Awake()
    {
        m_text = GetComponent<Text>();
    }
    void Start()
    {
        ResetTimer();
    }
    //On update the timer will remove delta time from m_time till time is 0 or lower
    //the trigger will activate the action resulting in the ending of the game in our case
    //but this could be changed to some other use by changing the action from the actions class.
    //leaving timer closed for modifcations
    private void Update()
    {

        if (m_time > 0 && m_isRunning)
        {
            m_time -= Time.deltaTime;
            UpdateDisplay();
        }
        else
        {
            if (m_isRunning)
                Trigger();
        }
    }
    public void ResetTimer()
    {
        
        m_time = m_duration;
        m_isRunning = true;
    }
    private void UpdateDisplay()
    {
        m_text.text = Mathf.CeilToInt(m_time).ToString();
    }
    public void SetTime(float time)
    {
      
        m_duration = time;
        ResetTimer();
    }
    public void Pause()
    {
        m_isRunning = false;

    }
    public void SetTrigger(Action action)
    {
        m_isRunning = false;
        m_tigger = action;

    }
    private void Trigger()
    {
        m_tigger?.Invoke();
    }

}

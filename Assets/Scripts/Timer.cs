using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    public float time = 0;
    public bool timerIsRunning = false;
    [SerializeField]  Text timeText;
    [SerializeField]  Image timeImage;
    
    private void Start()
    {
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            time += Time.deltaTime;
            timeImage.fillAmount = time / 50;
            // timeText.text = time.ToString();
            if (time >= 30)
            {
                timeImage.color = Color.red;
                timeText.color = Color.red;
            }

            
            DisplayTime(time);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        // float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        // timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = seconds.ToString();
    }
}

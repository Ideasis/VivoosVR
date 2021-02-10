using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public int totalMinutes = 5;
    int seconds = 60;

    TextMeshPro timerTxt;

    // Start is called before the first frame update
    void Start()
    {
        timerTxt = GetComponentInChildren<TextMeshPro>();

        StartCoroutine(CountTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountTime()
    {
        totalMinutes -= 1;

        string timeLeft;
        string timeLeftSide;
        string timeRightSide;


        while (totalMinutes >= 0 && seconds >= 0)
        {
            seconds--;

            if(totalMinutes < 10)
            {
                timeLeftSide = "0" + totalMinutes;
            }
            else
            {
                timeLeftSide = ""+totalMinutes;
            }

            if(seconds >= 10)
            {
                timeRightSide = "" + seconds;
            }
            else
            {
                timeRightSide = "0" + seconds;
            }

            timeLeft = timeLeftSide + ":" + timeRightSide;

            timerTxt.text = timeLeft;

            if(seconds == 0)
            {
                seconds = 60;
                totalMinutes -= 1;
            }

            yield return new WaitForSeconds(1);
        }
    }
}

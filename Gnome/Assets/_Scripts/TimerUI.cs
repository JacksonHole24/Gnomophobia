using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] Color offColour;
    [SerializeField] Color onColour;

    private void Start()
    {
        text.color = offColour;
    }

    public void SetTimerOn(float time)
    {
        int timeInt = (int)time;
        if(timeInt < 10)
        {
            text.text = "00:0" + timeInt.ToString();
        }
        else
        {
            text.text = "00:" + timeInt.ToString();
        }
        text.color = onColour;
    }

    public void SetTimerOff()
    {
        text.color = offColour;
        text.text = "00:00";
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public void Enter(string text)
    {
        Debug.Log("Enter: " + text);
    }

    public void Exit(string text)
    {
        Debug.Log("Exit: " + text);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Interactor : XRDirectInteractor
{
    
    public void Drop()
    {
        allowSelect = false;

        StartCoroutine(DelaySeconds(0.1f));

        Debug.Log("Dropped supposedly");
    }

    IEnumerator DelaySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        allowSelect = true;
    }

    public void ReGrab(Interactable interactable)
    {
        //OnSelectEntered(interactable);
    }

}

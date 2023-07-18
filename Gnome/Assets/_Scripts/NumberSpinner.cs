using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NumberSpinner : MonoBehaviour
{
    [SerializeField] float zeroRot;
    [SerializeField] float oneRot;
    [SerializeField] float twoRot;
    [SerializeField] float threeRot;
    [SerializeField] float fourRot;
    [SerializeField] float fiveRot;
    [SerializeField] float sixRot;
    [SerializeField] float sevenRot;
    [SerializeField] float eightRot;
    [SerializeField] float nineRot;

    [SerializeField] float spinTime;

    Quaternion targetRot;

    int targetNum;
    bool spin = false;


    public void Spin(int num)
    {
        targetNum = num;
        spin = true;
    }

    private void Update()
    {
        if (spin)
        {
            switch (targetNum)
            {
                case 0:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(zeroRot, 0f, 0f), spinTime);
                    targetRot = new Quaternion(zeroRot, 0f, 0f, 0f);
                    break;
                case 1:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(oneRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(oneRot, 0f, 0f);
                    break;
                case 2:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(twoRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(twoRot, 0f, 0f);
                    break;
                case 3:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(threeRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(threeRot, 0f, 0f);
                    break;
                case 4:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(fourRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(fourRot, 0f, 0f);
                    break;
                case 5:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(fiveRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(fiveRot, 0f, 0f);
                    break;
                case 6:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(sixRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(sixRot, 0f, 0f);
                    break;
                case 7:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(sevenRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(sevenRot, 0f, 0f);
                    break;
                case 8:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(eightRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(eightRot, 0f, 0f);
                    break;
                case 9:
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(nineRot, 0f, 0f), spinTime);
                    targetRot = Quaternion.Euler(nineRot, 0f, 0f);
                    break;
            }

            if(transform.localRotation == targetRot)
            {
                spin = false;
            }
        }
    }
}

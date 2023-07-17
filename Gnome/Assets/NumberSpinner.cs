using DotLiquid.Tags;
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
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(zeroRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(zeroRot, 0f, 0f, 0f);
                    break;
                case 1:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(oneRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(oneRot, 0f, 0f, 0f);
                    break;
                case 2:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(twoRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(twoRot, 0f, 0f, 0f);
                    break;
                case 3:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(threeRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(threeRot, 0f, 0f, 0f);
                    break;
                case 4:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(fourRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(fourRot, 0f, 0f, 0f);
                    break;
                case 5:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(fiveRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(fiveRot, 0f, 0f, 0f);
                    break;
                case 6:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(sixRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(sixRot, 0f, 0f, 0f);
                    break;
                case 7:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(sevenRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(sevenRot, 0f, 0f, 0f);
                    break;
                case 8:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(eightRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(eightRot, 0f, 0f, 0f);
                    break;
                case 9:
                    transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(nineRot, 0f, 0f, 0f), spinTime);
                    targetRot = new Quaternion(nineRot, 0f, 0f, 0f);
                    break;
            }

            if(transform.rotation == targetRot)
            {
                spin = false;
            }
        }
    }
}

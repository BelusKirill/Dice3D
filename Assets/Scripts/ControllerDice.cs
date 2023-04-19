using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDice : MonoBehaviour
{
    public float rotationSpeed = 0f; // Скорость поворота
    public Transform[] dices;
    
    public bool[] isRunDices;

    private void Start() {
        isRunDices = new bool[dices.Length];

        for (int i = 0; i < isRunDices.Length; i++)
        {
            isRunDices[i] = false;
        }
    }

    public void Spinning()
    {
        for (int i = 0; i < isRunDices.Length; i++)
        {
            if (isRunDices[i] == true) return;
        }

        for (int i = 0; i < dices.Length; i++)
        {
            RandomParamSpinning(dices[i], i);
        }
    }

    private void RandomParamSpinning(Transform dice, int index)
    {
        int coutSipn = Random.Range(4, 8);
        int result = Random.Range(1, 7);
        Debug.Log(result);

        StartCoroutine(RotateMeNow(dice, coutSipn, result, index));
    }

    IEnumerator RotateMeNow(Transform dice, int coutSipn, int result, int index)
    {
        isRunDices[index] = true;
        Quaternion targetAngles = new Quaternion();
        float targetAngleX = 0;
        float targetAngleY = 0;
        float targetAngleZ = 0;

        for (int i = 0; i < coutSipn; i++)
        {
            if (i != coutSipn-1)
            {
                targetAngleX = Random.Range(-360, 360);
                targetAngleY = Random.Range(-360, 360);
                targetAngleZ = Random.Range(-360, 360);

                targetAngles = Quaternion.Euler(targetAngleX, targetAngleY, targetAngleZ);
            }
            else
            {
                Vector3 vector3 = DeterminingAngleD6(result);

                targetAngleX = vector3.x;
                targetAngleY = vector3.y;
                targetAngleZ = vector3.z;

                targetAngles = Quaternion.Euler(targetAngleX, targetAngleY, targetAngleZ);
            }

            while (true)
            {
                if (EqualAngleTo(Quaternion.Euler(dice.eulerAngles.x, dice.eulerAngles.y, dice.eulerAngles.z), targetAngles, 2))
                {
                    if (targetAngleX < 0)
                        targetAngleX += 360; 
                    if (targetAngleY < 0)
                        targetAngleY += 360; 
                    if (targetAngleZ < 0)
                        targetAngleZ += 360; 
                    dice.eulerAngles = new Vector3(targetAngleX, targetAngleY, targetAngleZ);
                    break;
                }
                if (i != coutSipn-1)
                    dice.rotation = Quaternion.Slerp(dice.rotation, targetAngles, 0.01f * rotationSpeed); //Time.deltaTime * rotationSpeed
                else
                    dice.rotation = Quaternion.Slerp(dice.rotation, targetAngles, Time.deltaTime * 2 * rotationSpeed);
                yield return null;
            }
        }
        isRunDices[index] = false;
    }

    private Vector3 DeterminingAngleD6(int result)
    {
        switch (result)
        {
            case 1:
                return new Vector3(90, 90, 0);
            case 2:
                return new Vector3(180, 0, 90);
            case 3:
                return new Vector3(90, 90, 90);
            case 4:
                return new Vector3(270, 0, 0);
            case 5:
                return new Vector3(180, 180, 90);
            case 6:
                return new Vector3(90, 0, 90);                
            default:
                return new Vector3(0, 0, 0);
        }
    }

    private bool EqualTo(float value1, float value2, float epsilon)
    {
        if (value1 < 0)
            value1 += 360;
        if (value2 < 0)
            value2 += 360; 
        return Mathf.Abs(value1 - value2) < epsilon;
    }

    private bool EqualAngleTo(Quaternion value1, Quaternion value2, float epsilon)
    {
        return Quaternion.Angle(value1, value2) < epsilon;
    }
}

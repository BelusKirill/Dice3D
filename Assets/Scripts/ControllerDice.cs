using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class ControllerDice : MonoBehaviour
{
    public float rotationSpeed = 0f; // Скорость поворота
    public List<Dice> dices;
    public bool[] isRunDices;
    public int[] results;

    public TopPanel topPanel;

    private void Start() {
        if (dices == null || dices.Count == 0)
        {
            dices = new List<Dice>();
            return;            
        }

        ResetParam();
    }

    public void AddDice(Dice dice)
    {
        dices.Add(dice);
        ResetParam();
    }

    public void DelDice(string typeD)
    {
        foreach (Dice dice in dices)
        {
            if (dice.type == typeD)
            {
                dices.Remove(dice);
                Destroy(dice.transform.gameObject);
                ResetParam();
                return;
            }
        }
    }

    public void ClearDices()
    {
        foreach (Dice dice in dices)
        {
            Destroy(dice.transform.gameObject);
        }
        dices = new List<Dice>();
        ResetParam();
    }

    public bool IsRunSping()
    {
        for (int i = 0; i < isRunDices.Length; i++)
        {
            if (isRunDices[i] == true) return true;
        }
        return false;
    }

    public void Spinning()
    {
        if (dices.Count == 0)
            return;

        if (IsRunSping()) return;
        
        topPanel.AnimClose();
        for (int i = 0; i < dices.Count; i++)
        {
            RandomParamSpinning(dices[i].transform, i);
        }

        StartCoroutine(CheckResults());
    }

    private void RandomParamSpinning(Transform dice, int index)
    {
        int coutSipn = Random.Range(4, 9);
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
                    dice.rotation = Quaternion.Slerp(dice.rotation, targetAngles, Time.deltaTime * 2 * rotationSpeed); //Time.deltaTime * rotationSpeed
                else
                    dice.rotation = Quaternion.Slerp(dice.rotation, targetAngles, Time.deltaTime * rotationSpeed);
                yield return null;
            }
        }

        isRunDices[index] = false;
        results[index] = result;
    }

    IEnumerator CheckResults()
    {
        Debug.Log(results.All(element => element > 0));
        while(true)
        {
            if(results.All(element => element > 0))
                break;
            yield return null;
        }

        Debug.Log(results.All(element => element > 0));
        topPanel.AnimOpen();

        string strResult = string.Join("\\", results) + " (" + results.Sum() +")";
        topPanel.SetResult(strResult);
        ResetResult();
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

    private void ResetResult()
    {
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = 0;
        }
    }

    private void PositionDetection()
    {
        switch (dices.Count)
        {
            case 1:
                dices[0].transform.position = new Vector3(0,0,0);
                break;  
            case 2:
                dices[0].transform.position = new Vector3(1,0,0);
                dices[1].transform.position = new Vector3(-1,0,0);
                break; 
            case 3:
                dices[0].transform.position = new Vector3(2,0,0);
                dices[1].transform.position = new Vector3(0,0,0);
                dices[2].transform.position = new Vector3(-2,0,0);
                break; 
            case 4:
                dices[0].transform.position = new Vector3(1,-1,0);
                dices[1].transform.position = new Vector3(-1,-1,0);
                dices[2].transform.position = new Vector3(1,1,0);
                dices[3].transform.position = new Vector3(-1,1,0);
                break; 
            case 5:
                dices[0].transform.position = new Vector3(1,-1,0);
                dices[1].transform.position = new Vector3(-1,-1,0);
                dices[2].transform.position = new Vector3(2,1,0);
                dices[3].transform.position = new Vector3(-2,1,0);
                dices[4].transform.position = new Vector3(0,1,0);
                break;    
            case 6:
                dices[0].transform.position = new Vector3(2,-1,0);
                dices[1].transform.position = new Vector3(-2,-1,0);
                dices[2].transform.position = new Vector3(2,1,0);
                dices[3].transform.position = new Vector3(-2,1,0);
                dices[4].transform.position = new Vector3(0,1,0);
                dices[5].transform.position = new Vector3(0,-1,0);
                break;   
            case 7:
                dices[0].transform.position = new Vector3(2,-1,0);
                dices[1].transform.position = new Vector3(-2,-1,0);
                dices[2].transform.position = new Vector3(2,1,0);
                dices[3].transform.position = new Vector3(-2,1,0);
                dices[4].transform.position = new Vector3(0,2,0);
                dices[5].transform.position = new Vector3(0,-2,0);
                dices[6].transform.position = new Vector3(0,0,0);
                break;  
            case 8:
                dices[0].transform.position = new Vector3(2,-2,0);
                dices[1].transform.position = new Vector3(-2,-2,0);
                dices[2].transform.position = new Vector3(2,2,0);
                dices[3].transform.position = new Vector3(-2,2,0);
                dices[4].transform.position = new Vector3(0,2,0);
                dices[5].transform.position = new Vector3(0,-2,0);
                dices[6].transform.position = new Vector3(1,0,0);
                dices[7].transform.position = new Vector3(-1,0,0);
                break;  
            case 9:
                dices[0].transform.position = new Vector3(2,-2,0);
                dices[1].transform.position = new Vector3(-2,-2,0);
                dices[2].transform.position = new Vector3(2,2,0);
                dices[3].transform.position = new Vector3(-2,2,0);
                dices[4].transform.position = new Vector3(0,2,0);
                dices[5].transform.position = new Vector3(0,-2,0);
                dices[6].transform.position = new Vector3(2,0,0);
                dices[7].transform.position = new Vector3(-2,0,0);
                dices[8].transform.position = new Vector3(0,0,0);
                break;  
            case 10:
                dices[0].transform.position = new Vector3(2,-2,0);
                dices[1].transform.position = new Vector3(-2,-2,0);
                dices[2].transform.position = new Vector3(2,3,0);
                dices[3].transform.position = new Vector3(-2,3,0);
                dices[4].transform.position = new Vector3(0,3,0);
                dices[5].transform.position = new Vector3(0,-2,0);
                dices[6].transform.position = new Vector3(2,0.5f,0);
                dices[7].transform.position = new Vector3(-2,0.5f,0);
                dices[8].transform.position = new Vector3(0,1.35f,0);
                dices[9].transform.position = new Vector3(0,-0.3f,0);
                break;  
            case 11:
                dices[0].transform.position = new Vector3(2,-2,0);
                dices[1].transform.position = new Vector3(-2,-2,0);
                dices[2].transform.position = new Vector3(2,3,0);
                dices[3].transform.position = new Vector3(-2,3,0);
                dices[4].transform.position = new Vector3(0,3,0);
                dices[5].transform.position = new Vector3(0,-2,0);
                dices[6].transform.position = new Vector3(2,1.35f,0);
                dices[7].transform.position = new Vector3(-2,0.5f,0);
                dices[8].transform.position = new Vector3(0,1.35f,0);
                dices[9].transform.position = new Vector3(0,-0.3f,0);
                dices[10].transform.position = new Vector3(2,-0.3f,0);
                break;  
            case 12:
                dices[0].transform.position = new Vector3(2,-2,0);
                dices[1].transform.position = new Vector3(-2,-2,0);
                dices[2].transform.position = new Vector3(2,3,0);
                dices[3].transform.position = new Vector3(-2,3,0);
                dices[4].transform.position = new Vector3(0,3,0);
                dices[5].transform.position = new Vector3(0,-2,0);
                dices[6].transform.position = new Vector3(2,1.35f,0);
                dices[7].transform.position = new Vector3(-2,1.35f,0);
                dices[8].transform.position = new Vector3(0,1.35f,0);
                dices[9].transform.position = new Vector3(0,-0.3f,0);
                dices[10].transform.position = new Vector3(2,-0.3f,0);
                dices[11].transform.position = new Vector3(-2,-0.3f,0);
                break;  
        }
    }

    private void ResetParam()
    {
        isRunDices = new bool[dices.Count];
        results = new int[dices.Count];

        for (int i = 0; i < isRunDices.Length; i++)
        {
            isRunDices[i] = false;
        }

        ResetResult();
        PositionDetection();
    }
}

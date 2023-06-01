using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading.Tasks;

public class ControllerDice : MonoBehaviour
{
    public float rotationSpeed = 0f; // Скорость поворота
    public bool isAnim = true;
    public List<Dice> dices;
    public bool[] isRunDices;
    public int[] results;

    public TopPanel topPanel;
    public GameObject toggleGroupResult;

    public Animator animCamera;


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

    public void UpdateDices()
    {
        int typeTheam = gameObject.GetComponent<Theams>().GetTypeTheam();
        foreach (Dice dice in dices)
        {
            Transform newDiceT = Instantiate(gameObject.GetComponent<Theams>().GetDice(dice.type), dice.transform.position, dice.transform.rotation);
            newDiceT.SetParent(transform);

            Destroy(dice.transform.gameObject);

            dice.transform = newDiceT;
            dice.typeTheam = typeTheam;
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

    public void ChangeSpeedSpin(Slider slider)
    {
        rotationSpeed = slider.value;
    }

    public void ChangeAnimSpin(Toggle toggle)
    {
        isAnim = toggle.isOn;
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
        
        if (isAnim)
        {
            topPanel.AnimClose();
            int resRand = Random.Range(0, 2);

            switch (resRand)
            {
                case 0:
                    animCamera.SetTrigger("Shak");
                    break;

                case 1:
                    animCamera.SetTrigger("Shak2");
                    break;
            }
        }

        for (int i = 0; i < dices.Count; i++)
        {
            if (isAnim)
            {
                RandomParamSpinning(dices[i].transform, i, dices[i].type);
            }
            else
            {
                NoAnimSpin(dices[i].transform, i, dices[i].type);
            }
        }

        StartCoroutine(CheckResults());
    }

    private void RandomParamSpinning(Transform dice, int index, string type)
    {
        int coutSipn = Random.Range(4, 9);
        int result = GetRandomResult(type);
        
        StartCoroutine(RotateMeNow(dice, coutSipn, result, index, type));
    }

    IEnumerator RotateMeNow(Transform dice, int coutSipn, int result, int index, string type)
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
                Vector3 vector3 = DeterminingAngle(type, result);

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

    private void NoAnimSpin(Transform dice, int index, string type)
    {
        //isRunDices[index] = true;
        int result = GetRandomResult(type);

        Vector3 vector3 = DeterminingAngle(type, result);

        float targetAngleX = vector3.x;
        float targetAngleY = vector3.y;
        float targetAngleZ = vector3.z;

        Quaternion targetAngles = Quaternion.Euler(targetAngleX, targetAngleY, targetAngleZ);

        if (targetAngleX < 0)
            targetAngleX += 360; 
        if (targetAngleY < 0)
            targetAngleY += 360; 
        if (targetAngleZ < 0)
            targetAngleZ += 360; 
        dice.eulerAngles = new Vector3(targetAngleX, targetAngleY, targetAngleZ);

        //isRunDices[index] = false;
        results[index] = result;
    }

    private int GetRandomResult(string type)
    {
        int result = 0;
        switch (type)
        {
            case "D3":
                result = Random.Range(1, 4);
                break;
            case "D4":
                result = Random.Range(1, 5);
                break;
            case "D6":
                result = Random.Range(1, 7);
                break;
        }
        return result;
    }

    IEnumerator CheckResults()
    {
        while(true)
        {
            if(results.All(element => element > 0))
                break;
            yield return null;
        }

        topPanel.AnimOpen();

        string strResult = "";

        switch (toggleGroupResult.GetComponent<ToggleGroup>().GetIdToggleIsON())
        {
            case 1:
                strResult = results.Sum().ToString();
                break;
            case 2:
                strResult = string.Join("\\", results) + " (" + results.Sum() +")";
                break;  
            case 3:
                strResult = string.Join("+", results) + "=" + results.Sum();
                break;  
            default:
                strResult = results.Sum().ToString();
                break;
        }

        //string strResult = string.Join("\\", results) + " (" + results.Sum() +")";
        topPanel.SetResult(strResult, results);
        ResetResult();
    }

    private Vector3 DeterminingAngle(string type, int result)
    {
        switch (type)
        {
            case "D3":
                return DeterminingAngleD3(result);
            case "D4":
                return DeterminingAngleD4(result);
            case "D6":
                return DeterminingAngleD6(result);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    private Vector3 DeterminingAngleD3(int result)
    {
        switch (result)
        {
            case 1:
                return new Vector3(0, 0, 0);
            case 2:
                return new Vector3(90, 0, 0);
            case 3:
                return new Vector3(90, 90, 0);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    private Vector3 DeterminingAngleD4(int result)
    {
        switch (result)
        {
            case 1:
                return new Vector3(90, 0, 0);
            case 2:
                return new Vector3(217, 16, 28);
            case 3:
                return new Vector3(157, 147, -14);
            case 4:
                return new Vector3(95, 0, 112);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    private Vector3 DeterminingAngleD6(int result)
    {
        switch (result)
        {
            case 1:
                return new Vector3(180, 0, 180);
            case 2:
                return new Vector3(270, 0, 180);
            case 3:
                return new Vector3(270, 90, 180);
            case 4:
                return new Vector3(270, 180, 180);
            case 5:
                return new Vector3(270, 270, 180);
            case 6:
                return new Vector3(180, 180, 0);                
            default:
                return new Vector3(180, 0, 180);
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

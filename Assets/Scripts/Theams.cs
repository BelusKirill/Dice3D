using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theams : MonoBehaviour
{
    public int idActualTheam;
    public PrefabsTheam[] prefabsTheams;
    public GameObject toggleGroupTheams;

    public Transform GetDice(string type)
    {
        idActualTheam = toggleGroupTheams.GetComponent<ToggleGroup>().GetIdToggleIsON() - 1;

        switch (type)
        {
            case "D3":
                return prefabsTheams[idActualTheam].d3;
            case "D4":
                return prefabsTheams[idActualTheam].d4;
            case "D6":
                return prefabsTheams[idActualTheam].d6;
            default:
                return null;
        }
    }
}

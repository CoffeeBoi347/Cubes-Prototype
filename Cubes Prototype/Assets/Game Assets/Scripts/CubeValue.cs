using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeValue : MonoBehaviour
{
    public int cubeValue;
    public List<int> values = new List<int>() { 2, 4, 8, 16, 32, 64, 128, 256, 512 };

    private void Start()
    {
        int numToChoose = Random.Range(0, values.Count - 1);
        cubeValue = values[numToChoose];
        TMP_Text t = transform.GetComponentInChildren<TMP_Text>();
        t.text = cubeValue.ToString();
    }
}
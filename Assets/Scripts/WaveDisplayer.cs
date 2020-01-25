using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WaveNumber;
    public void DisplayCurrentWave(int currentWave)
    {
        WaveNumber.text = currentWave.ToString();
    }
}

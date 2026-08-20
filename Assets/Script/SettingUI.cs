using System;
using TMPro;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private int indexSensitivity;
    [SerializeField] private TextMeshProUGUI textSensitivityStatus;

    private string[] _listIndexing = { "Low", "Normal", "High" };

    private void Start()
    {
        indexSensitivity = 1; //hardcoded opsi Normal
        textSensitivityStatus.text = $"{_listIndexing[indexSensitivity]} Sensitivity";
    }

    public void SensitivityConfig()
    {
        indexSensitivity++;
        if (indexSensitivity >= Enum.GetValues(typeof(Sensitivity)).Length)
            indexSensitivity = 0;

        Sensitivity targetSens = new Sensitivity(); 

        switch (indexSensitivity)
        {
            case 0:
                targetSens = Sensitivity.LOW;
                break;
            case 1:
                targetSens = Sensitivity.NORMAL;
                break;
            case 2:
                targetSens = Sensitivity.HIGH;
                break;
            default:
                break;
        }

        GameManager.instance.ConfigSensitivity(targetSens);
        textSensitivityStatus.text = $"{_listIndexing[indexSensitivity]} Sensitivity";
    }
}

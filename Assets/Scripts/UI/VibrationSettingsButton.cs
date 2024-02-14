using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class VibrationSettingsButton : MonoBehaviour
{

    [SerializeField] private GameObject offUI;
    [SerializeField] private GameObject onUI;

    void OnEnable()
    {
        UpdateUI();
    }


    public void SwitchVibration()
    {
        var isVibrationOn = PlayerPrefs.GetInt("isVibrationOn", 1);

        if (isVibrationOn == 1)
        {
            PlayerPrefs.SetInt("isVibrationOn", 0);
        }
        else
        {
            PlayerPrefs.SetInt("isVibrationOn", 1);
        }

        UpdateUI();
    }


    void UpdateUI()
    {
        var isVibrationOn = PlayerPrefs.GetInt("isVibrationOn", 1);

        if (isVibrationOn == 1)
        {
            offUI.SetActive(false);
            onUI.SetActive(true);
        }
        else
        {
            offUI.SetActive(true);
            onUI.SetActive(false);
        }

        // turn vibration on/off for mobile devices
        MMVibrationManager.SetHapticsActive(isVibrationOn == 1);

    }
}

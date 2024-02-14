using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject offUI;
    [SerializeField] private GameObject onUI;


    void OnEnable()
    {
        UpdateUI();
    }



    public void SwitchSound()
    {
        var isSoundOn = PlayerPrefs.GetInt("isSoundOn", 1);

        if (isSoundOn == 1)
        {
            PlayerPrefs.SetInt("isSoundOn", 0);
        }
        else
        {
            PlayerPrefs.SetInt("isSoundOn", 1);
        }

        UpdateUI();
    }


    void UpdateUI()
    {
        var isSoundOn = PlayerPrefs.GetInt("isSoundOn", 1);

        if (isSoundOn == 1)
        {
            offUI.SetActive(false);
            onUI.SetActive(true);

        }
        else
        {
            offUI.SetActive(true);
            onUI.SetActive(false);
        }

        // switch sound on/off
        AudioListener.pause = isSoundOn == 0;
    }
}

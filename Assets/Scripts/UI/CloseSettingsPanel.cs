using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void ClosePanel()
    {
        settingsPanel.SetActive(false);
    }
}

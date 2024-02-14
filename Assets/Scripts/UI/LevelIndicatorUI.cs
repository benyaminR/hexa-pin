using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIndicatorUI : MonoBehaviour
{
    private void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        var level = PlayerPrefs.GetInt("CurrentLevel", 0);
        var levelText = GetComponent<TMPro.TextMeshProUGUI>();
        levelText.text = "LEVEL " + (level + 1).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicyButton : MonoBehaviour
{
    [SerializeField] private string privacyPolicyUrl = "https://www.fatmoai.com/";

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyUrl);
    }
}

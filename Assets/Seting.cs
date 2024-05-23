using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetingMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;

    public void ChangeGraphicsQualyfi()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathText : MonoBehaviour
{
    public TMP_Text textElement;

    public void HideText()
    {
        textElement.enabled = false;
    }

    public void ShowText()
    {
        textElement.enabled = true;
    }
}

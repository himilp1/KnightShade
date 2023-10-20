using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    public TMP_Text textElement;

    public void SetText(string text)
    {
        textElement.text = text;
    }

    public void ShowText()
    {
        textElement.enabled = true;
    }

    public void HideText()
    {
        textElement.enabled = false;
    }
}

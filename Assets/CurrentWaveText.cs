using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentWaveText : MonoBehaviour
{
    public TMP_Text textElement;

    private void Start()
    {
        textElement.text = "Wave 1";
    }

    public void SetWave(int wave)
    {
        textElement.text = "Wave " + wave.ToString();
    }
}

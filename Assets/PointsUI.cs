using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public TMP_Text textElement;

    private void Start()
    {
        textElement.text = "Points: 0";
    }

    public void SetPoints(int points)
    {
        textElement.text = "Points: " + points.ToString();
    }
}

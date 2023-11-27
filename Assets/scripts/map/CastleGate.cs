using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGate : MonoBehaviour
{
    public float raiseAmount = 2.0f; // Adjust this value to control how much the gate raises.
    public AudioSource openSound;

    public void Open()
    {
        // Raise the CastleGate on the Y-axis
        Vector3 newPosition = transform.position + Vector3.up * raiseAmount;
        transform.position = newPosition;

        openSound.Play();

        Debug.Log("Opened Gate");
    }
}

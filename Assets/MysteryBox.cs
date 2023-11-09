using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject boxLid;

    public float targetXRotation = 0.0f;
    public float rotationSpeed = 3.0f;

    public float openTime = 2.0f;

    public List<GameObject> weapons = new List<GameObject>();

    private void Start()
    {
        boxLid = transform.Find("Chest_Open_Cap").gameObject;
    }

    public void Open()
    {
        // Open Box
        float newXRotation = Mathf.MoveTowards(transform.eulerAngles.x, targetXRotation, Time.deltaTime * rotationSpeed);
        // Apply the new X-angle to the object's rotation
        boxLid.transform.eulerAngles = new Vector3(newXRotation, transform.eulerAngles.y, transform.eulerAngles.z);

        Debug.Log("Opened Chest");

        // Generate a random outcome
        string[] possibleOutcomes = { "Outcome1", "Outcome2", "Outcome3", "Outcome4" };
        string selectedOutcome = possibleOutcomes[UnityEngine.Random.Range(0, possibleOutcomes.Length)];
        Debug.Log("Selected Outcome: " + selectedOutcome);

        Invoke("Close", openTime);
    }

    // TODO: Make the close animation actually work
    private void Close()
    {
        float closedAngle = 90.0f;

        float newXRotation = Mathf.MoveTowards(transform.eulerAngles.x, closedAngle, Time.deltaTime * rotationSpeed);
        boxLid.transform.eulerAngles = new Vector3(newXRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        Debug.Log("Closed Chest");
    }
}

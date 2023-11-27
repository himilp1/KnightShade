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
    public Transform spawnLocation;

    public AudioSource boxOpenSound;

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

        boxOpenSound.Play();

        // Generate a random outcome
        GameObject weaponToSpawn = weapons[Random.Range(0, weapons.Count)];
        Debug.Log("Selected Outcome: " + weaponToSpawn);
        Instantiate(weaponToSpawn, spawnLocation.position, Quaternion.identity);
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

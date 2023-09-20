using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScriptShouldNotWork : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 720.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculates movement direction
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement.Normalize();

        // Apply the movement to the GameObject.
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        if(movement != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
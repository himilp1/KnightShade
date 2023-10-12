using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour{
    public Transform target;
    public Vector3 offset = new Vector3(0f,2f,-10f);
    public float smoothSpeed = 5.0f;

    void LateUpdate(){
        if(target == null){
            return;
        }

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
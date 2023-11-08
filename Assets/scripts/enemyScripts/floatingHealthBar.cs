using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class floatingHealthBar : MonoBehaviour{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    public Transform target;
    [SerializeField] private Vector3 offset;
    public Transform player;
    public void UpdateHealthBar(float currentValue, float maxValue){
        slider.value = currentValue / maxValue;
    }
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        FindHighestParentTransform();
    }
    void Update(){
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }

    void FindHighestParentTransform()
    {
        Transform highestParent = transform;
        Transform currentParent = transform.parent;

        while (currentParent != null)
        {
            highestParent = currentParent;
            currentParent = currentParent.parent;
        }

        target = highestParent;
    }


}
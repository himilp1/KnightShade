using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
    public Rigidbody rb;
    public float speed;
    public GameObject Explosion;

    void Start() {
        rb.AddForce(transform.forward * speed);
        Destroy(this.gameObject, 6);
    }

    void Update() {
        RaycastHit hit;
        Debug.Log(Physics.Raycast(transform.position, transform.forward, out hit, 3));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("MC01")) {
            GameObject go = Instantiate(Explosion, transform.position, transform.rotation);
            go.SetActive(true);
            Destroy(go, 1);
            Destroy(this.gameObject);
        }
    }
}

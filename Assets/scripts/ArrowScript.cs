// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ArrowScript : MonoBehaviour
// {
//     public Rigidbody rb;
//     public float speed;
//     public GameObject Explosion;
//     // Start is called before the first frame update
//     private void Start()
//     {
//         rb.AddForce(transform.forward * speed);
//         Destroy(this.gameObject, 6);
//     }
//     void Update()
//     {
//         public RaycastHit hit;
//     console.Log(Physics.Raycast(transform.position, transform.forward, out hit, 3));
//         // if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
//         // {
//         //     if (hit.collider.gameObject.tag == "MC01")
//         //     {
//         //         GameObject go = Instantiate(Explosion, transform.position, transform.rotation);
//         //         go.SetActive(true);
//         //         Destroy(go, 1);
//         //         Destroy(this.gameObject);
//         //     }
//         // }

//     }

// private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.tag == "MC01")
//         {
//             GameObject go = Instantiate(Explosion, transform.position, transform.rotation);
//             go.SetActive(true);
//             Destroy(go, 1);
//             Destroy(this.gameObject);
//         }
//     }

//     // Update is called once per frame

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // public Collider player;
    // public float speed;
    // public GameObject Explosion;

    void Start()
    {
        // rb.AddForce(transform.forward * speed);
        // Destroy(this.gameObject, 6);
        Debug.Log("Arrow fired!");
    }

    void Update()
    {
        // RaycastHit hit;
        // transform.rotation = Quaternion.LookRotation(rb.velocity);
        // Debug.Log(Physics.Raycast(transform.position, transform.forward, out hit, 3));
        // Debug.Log("Arrow moving!");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // GameObject go = Instantiate(Explosion, transform.position, transform.rotation);
            Debug.Log("Player hit!");
            // go.SetActive(true);
            // Destroy(go, 1);
            Destroy(this.gameObject);
        }
        Debug.Log("Arrow hit!");
        Destroy(this.gameObject, 7);
    }
}
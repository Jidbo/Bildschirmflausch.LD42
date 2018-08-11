using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float speed = 100;

    Rigidbody rb;
    float xSpeed;
    float zSpeed;
    float rotationAngle;
    Transform t;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        xSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        zSpeed = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
    }

    private void FixedUpdate() {
        rb.velocity = new Vector3(xSpeed, 0, zSpeed);
        t.LookAt(t.position + rb.velocity);
    }
}

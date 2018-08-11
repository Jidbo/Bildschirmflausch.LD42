using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float speed = 100;
    [SerializeField]
    float acceleration = 0.9f;

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
        xSpeed = Input.GetAxis("Horizontal") * speed;
        zSpeed = Input.GetAxis("Vertical") * speed;
		rotationAngle+=0.1f;
    }

    private void FixedUpdate() {
		rb.velocity = rb.velocity * acceleration + (1 - acceleration) * new Vector3(xSpeed, 0, zSpeed);

        if(rb.velocity.magnitude >= 0.1)
			t.LookAt(t.position + new Vector3((float)System.Math.Sin(rotationAngle), 0, (float)System.Math.Cos(rotationAngle)));
    }
}

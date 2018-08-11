using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    //[SerializeField]
    float rotationSpeed = 150;
	//[SerializeField]
    float movementSpeed = 10F;
    [SerializeField]
    float acceleration = 0.9f;

    Rigidbody rb;
    float forwardSpeed;
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
        zSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        forwardSpeed = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, zSpeed, 0);
        transform.Translate(forwardSpeed, 0, 0);
    }

  //  private void FixedUpdate() {
		//rb.velocity = rb.velocity * (1 - acceleration) + acceleration * new Vector3(xSpeed, rb.velocity.y, zSpeed);

   //     if(rb.velocity.magnitude >= 0.1)
			//t.LookAt(t.position + new Vector3((float)System.Math.Sin(rotationAngle), 0, (float)System.Math.Cos(rotationAngle)));
    //}
}

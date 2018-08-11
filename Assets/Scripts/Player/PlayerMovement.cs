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

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
        zSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        forwardSpeed = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, zSpeed, 0);
        transform.Translate(forwardSpeed, 0, 0);
    }
}

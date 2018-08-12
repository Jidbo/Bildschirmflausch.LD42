using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float rotationSpeed = 2.3f;

    float movementSpeed = 10;

    float rotationAngle = (float) Math.PI * 0.5f;
    
	void FixedUpdate () {
        rotationAngle = (rotationAngle + (Input.GetAxis("Vertical") < 0 ? 1 : -1) * Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed);
        while (rotationAngle < -Math.PI)
            rotationAngle += 2 * (float) Math.PI;
        while (rotationAngle > Math.PI)
            rotationAngle -= 2 * (float)Math.PI;
        Vector3 facing = Vector3.Normalize(new Vector3((float) Math.Cos(rotationAngle), 0, (float) Math.Sin(rotationAngle)));
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.LookAt(transform.position + facing);
        rb.velocity = facing * movementSpeed * Input.GetAxis("Vertical");
    }
}

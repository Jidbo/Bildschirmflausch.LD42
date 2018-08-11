using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    //[SerializeField]
    float rotationSpeed = 150;
	//[SerializeField]
    float movementSpeed = 10F;
    
    float forwardSpeed;
    float zSpeed;
    float rotationAngle;

	// Update is called once per frame
	void Update () {
        zSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        forwardSpeed = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, zSpeed, 0);
        transform.Translate(forwardSpeed, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    float rotationSpeed = 150;
    float movementSpeed = 10;
    
    float mSpeed;
    float rSpeed;
    float rotationAngle;

	// Update is called once per frame
	void Update () {
        mSpeed = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        rSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;

        if (mSpeed <= 0) {
            mSpeed /= 2;
        }

        transform.Rotate(0, rSpeed, 0);
        transform.Translate(0, 0, mSpeed);
    }
}

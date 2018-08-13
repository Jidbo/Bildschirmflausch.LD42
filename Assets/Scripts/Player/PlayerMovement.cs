﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float rotationSpeed = 2.3f;
    [SerializeField]
    float movementSpeed = 10;

    float hSpeed;
    float vSpeed;

    bool controllable = true;

    [SerializeField]
    float rotationAngle = (float) Math.PI * 0.5f;

    private void Update() {
        if (controllable) {
            hSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
            vSpeed = Input.GetAxis("Vertical") * movementSpeed;
            try {
                AudioControl audioControl = GameController.instance.GetAudio();
                if (Input.GetAxis("Vertical") < 0) {
                    if (audioControl.SfxPlaying(0)) {
                        audioControl.SfxStop(0);
                    }
                    if (!audioControl.SfxPlaying(1)) {
                        audioControl.SfxPlay(1);
                    }
                } else {
                    if (audioControl.SfxPlaying(1)) {
                        audioControl.SfxStop(1);
                    }
                    if (!audioControl.SfxPlaying(0)) {
                        audioControl.SfxPlay(0);
                    }
                }
            }
            catch (Exception e) {

            }
        }
    }

    void FixedUpdate() {
        if (controllable) {
            rotationAngle = (rotationAngle + (vSpeed >= 0 ? -1 : 1) * hSpeed);
            while (rotationAngle < -Math.PI)
                rotationAngle += 2 * (float)Math.PI;
            while (rotationAngle > Math.PI)
                rotationAngle -= 2 * (float)Math.PI;
            Vector3 facing = Vector3.Normalize(new Vector3((float)Math.Cos(rotationAngle), 0, (float)Math.Sin(rotationAngle)));
            Rigidbody rb = GetComponent<Rigidbody>();
            transform.LookAt(transform.position + facing);
            rb.velocity = facing * vSpeed;
        }
    }

    public void ToggleControlledMovement(bool shouldBeMoveable) {
        controllable = shouldBeMoveable;
    }
}

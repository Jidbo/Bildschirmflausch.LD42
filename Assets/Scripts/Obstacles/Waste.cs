﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour {
    
    [SerializeField]
    float maxTimeTillBoom;
    [SerializeField]
    float regenerationAmount;
    [SerializeField]
    float currentTimeTillBoom;
    [SerializeField]
    float explosionForce = 100;
    [SerializeField]
    float explosionRadius = 7;
    [SerializeField]
    GameObject explosionParticleSystem;
    bool exploded = false;

    private void Start() {
        currentTimeTillBoom = maxTimeTillBoom;
	}

    private void FixedUpdate() {
        if (CollidesWithWaste()) {
            GameController.instance.flashNotification();
            currentTimeTillBoom -= Time.deltaTime;
            if (currentTimeTillBoom <= 0) {
                Explode();
            }
        } else if (currentTimeTillBoom < maxTimeTillBoom) {
            currentTimeTillBoom += regenerationAmount;
        }

        if (CollidesWithHazard())
            Explode();
    }

    private bool CollidesWithWaste() {
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(0.4f, 0.8f, 0.4f), transform.rotation);
        foreach (Collider c in colliders) {
            if(c.gameObject.CompareTag("waste") && c.gameObject != gameObject) {
                return true;
            }
        }
        return false;
    }

    private bool CollidesWithHazard() {
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(0.4f, 0.8f, 0.4f), transform.rotation);
        foreach (Collider c in colliders) {
            if (c.gameObject.CompareTag("hazard") && c.gameObject != gameObject) {
                return true;
            }
        }
        return false;
    }

    private void Explode() {
        if (!exploded) {
            exploded = true;
            Vector3 explosionPosition = gameObject.transform.position;
            explosionParticleSystem.GetComponent<ParticleSystem>().Play(true);
            explosionParticleSystem.transform.SetParent(null);
            Destroy(gameObject);
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
            foreach (Collider c in colliders) {
                Storage storage = c.GetComponent<Storage>();
                if (storage != null) {
                    while (!storage.IsEmpty()) {
                        GameObject storageItem = storage.TakeFromStorage();
                        Rigidbody sirb = storageItem.GetComponent<Rigidbody>();
                        if (sirb != null) {
                            sirb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                        }
                        if (storageItem.CompareTag("waste")) {
                            storageItem.GetComponent<Waste>().Explode();
                        }
                    }
                }
                Rigidbody cRB = c.gameObject.GetComponent<Rigidbody>();
                if (cRB != null) {
                    cRB.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                }
                if (c.gameObject.CompareTag("waste")) {
                    c.gameObject.GetComponent<Waste>().Explode();
                }
            }
        }
    }
}

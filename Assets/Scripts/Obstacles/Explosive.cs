using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

    [SerializeField]
    float maxTimeTillBoom;
    [SerializeField]
    float regenerationAmount;
    private float currentTimeTillBoom;
    [SerializeField]
    float explosionForce = 100;
    [SerializeField]
    float explosionRadius = 7;
    [SerializeField]
    GameObject explosionParticleSystem;
    Animator animator;

    bool exploded = false;

    private void Start() {
        currentTimeTillBoom = maxTimeTillBoom;
        animator = GetComponent<Animator>();
    }


    private bool CollidesWithWaste() {
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position + (gameObject.transform.up * 0.45f), new Vector3(0.4f, 0.8f, 0.4f), transform.rotation);
        foreach (Collider c in colliders) {
            if (c.gameObject.GetComponent<Waste>() != null && c.gameObject != gameObject) {
                return true;
            }
        }
        return false;
    }

    private void FixedUpdate() {
        if (CollidesWithWaste()) {
            if (animator != null) {
                animator.SetBool("exploding", true);
            }
            try {
                GameController.instance.flashNotification();
            }
            catch (Exception e) {
                Debug.Log(e);
            }
            currentTimeTillBoom -= Time.deltaTime;
            if (currentTimeTillBoom <= 0) {
                Explode();
            }
        } else if (currentTimeTillBoom < maxTimeTillBoom) {
            if (animator != null) {
                animator.SetBool("exploding", false);
            }
            currentTimeTillBoom += regenerationAmount;
        }
    }

    public void Explode() {
        if (!exploded) {
            exploded = true;
            Vector3 explosionPosition = gameObject.transform.position;
            if (explosionParticleSystem != null) {
                explosionParticleSystem.GetComponent<ParticleSystem>().Play(true);
                explosionParticleSystem.transform.SetParent(null);
            }
            Destroy(gameObject);
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
            foreach (Collider c in colliders) {
                Storage storage = c.GetComponent<Storage>();
                if (storage != null) {
                    while (!storage.IsEmpty()) {
                        GameObject storageItem = storage.Remove();
                        Rigidbody sirb = storageItem.GetComponent<Rigidbody>();
                        if (sirb != null) {
                            sirb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                        }
                        Explosive storageItemExplosive = storageItem.GetComponent<Explosive>();
                        if (storageItemExplosive != null) {
                            storageItemExplosive.Explode();
                        }
                    }
                }
                Rigidbody cRB = c.gameObject.GetComponent<Rigidbody>();
                if (cRB != null) {
                    cRB.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                }
                Explosive colliderExplosive = c.gameObject.GetComponent<Explosive>();
                if (colliderExplosive != null) {
                    colliderExplosive.Explode();
                }
            }
            try {
                GameController.instance.showGameOverUI();
            }
            catch (Exception e) {
                Debug.Log(e);
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Explosion : MonoBehaviour {
    private float explosionRadius = 0;
    private float explosionForce = 0;
    private ParticleSystem[] explosionParticles;
    private bool detonated = false;

    private void Awake() {
        explosionParticles = GetComponents<ParticleSystem>();
    }

    public void setValues(Vector3 pos, float r, float power) {
        transform.position = pos;
        explosionRadius = r;
        explosionForce = power;
    }

    public void Detonate() {
        foreach (ParticleSystem ps in explosionParticles) {
            ps.Play();
        }
        AudioControl audioControl = GameController.instance.GetAudio();
        if(audioControl != null) {
            audioControl.SfxPlay(2);
        }
        detonated = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in colliders) {
            StorageSystem storage = c.GetComponent<StorageSystem>();
            if (storage != null) {
                List<GameObject> items = storage.RemoveAllContents();
                foreach (GameObject storageItem in items) {
                    Rigidbody sirb = storageItem.GetComponent<Rigidbody>();
                    if (sirb != null) {
                        sirb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    }
                    Explosive storageItemExplosive = storageItem.GetComponent<Explosive>();
                    if (storageItemExplosive != null) {
                        storageItemExplosive.Explode();
                    }
                }
            }
            Rigidbody cRB = c.gameObject.GetComponent<Rigidbody>();
            if (cRB != null) {
                cRB.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            Explosive colliderExplosive = c.gameObject.GetComponent<Explosive>();
            if (colliderExplosive != null) {
                colliderExplosive.shouldExplode = true;
            }
        }
        try {
            GameController.instance.showGameOverUI();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }
    
    void Update() {
        if(!ParticlesPlaying() && detonated) {
            Destroy(gameObject);
        }
    }

    private bool ParticlesPlaying() {
        foreach(ParticleSystem ps in explosionParticles) {
            if (ps.isPlaying)
                return true;
        }
        return false;
    }
}

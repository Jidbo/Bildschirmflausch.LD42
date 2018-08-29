using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {
    [SerializeField]
    float health = 10;
    private float currentHealth;
    [SerializeField]
    float regenerationAmount = 0.01f;
    [SerializeField]
    float poisonCoolDown = 0.5f;
    float currentPoisonCoolDown;
    [SerializeField]
    float explosionForce = 100;
    [SerializeField]
    float explosionRadius = 7;
    [SerializeField]
    GameObject explosion;
    Animator animator;
    bool poisoned;

    bool exploded = false;

    private void Start() {
        currentHealth = health;
        currentPoisonCoolDown = poisonCoolDown;
        animator = GetComponent<Animator>();
    }

    public void AddPoisoning(float amount) {
        poisoned = true;
        currentPoisonCoolDown = poisonCoolDown;
        currentHealth -= amount;
    }

    private void FixedUpdate() {
        if (currentHealth <= 0) {
            Explode();
        }
        if (currentPoisonCoolDown > 0) {
            currentPoisonCoolDown -= Time.deltaTime;
        }
        if (currentPoisonCoolDown <= 0) {
            poisoned = false;
            if (animator != null) {
                animator.SetBool("exploding", false);
            }
            if (currentHealth <= health) {
                currentHealth += regenerationAmount;
                if (currentHealth > health) {
                    currentHealth = health;
                }
            }
        }
        else if (poisoned) {
            
            if (animator != null) {
                animator.SetBool("exploding", true);
            }
            try {
                GameController.instance.flashNotification();
            }
            catch (Exception e) {
                Debug.Log(e);
            }
        }
    }

    public void Explode() {
        if (!exploded) {
            exploded = true;
            Vector3 explosionPosition = gameObject.transform.position;
            Destroy(gameObject);
            GameObject newExplosion = Instantiate(explosion);
            Explosion expl = newExplosion.GetComponent<Explosion>();
            if (expl != null) {
                expl.setValues(explosionPosition, explosionRadius, explosionForce);
                expl.Detonate();
            }
        }
    }
}

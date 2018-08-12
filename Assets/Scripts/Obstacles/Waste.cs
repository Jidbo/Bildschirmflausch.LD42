using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour {

    private bool collided;
    [SerializeField]
    float maxTimeTillBoom;
    [SerializeField]
    float regenerationAmount;
    [SerializeField]
    float currentTimeTillBoom;
    
    private void Start() {
        currentTimeTillBoom = maxTimeTillBoom;
	}

    private void OnCollisionStay(Collision collision) {
        if (currentTimeTillBoom > 0 && collision.gameObject.CompareTag("waste")) {
            currentTimeTillBoom -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        if (currentTimeTillBoom <= 0) {
            Explode();
            return;
        }
        if (currentTimeTillBoom < maxTimeTillBoom) {
            currentTimeTillBoom += regenerationAmount * Time.deltaTime;
        }
    }

    private void Explode() {
        Destroy(gameObject);
    }
}

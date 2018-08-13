using System.Collections;
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
    bool exploded = false;

    private void Start() {
        currentTimeTillBoom = maxTimeTillBoom;
	}

    private void FixedUpdate() {
        if (CollidesWithWaste()) {
            currentTimeTillBoom -= Time.deltaTime;
            if (currentTimeTillBoom <= 0) {
                Explode();
            }
        } else if (currentTimeTillBoom < maxTimeTillBoom) {
            currentTimeTillBoom += regenerationAmount;
        }
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

    private void Explode() {
        if (!exploded) {
            exploded = true;
            Debug.Log(gameObject + "Exploded");
            Vector3 explosionPosition = gameObject.transform.position;
            Destroy(gameObject);
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
            foreach (Collider c in colliders) {
                Debug.Log(c.gameObject);
                Storage storage = c.GetComponent<Storage>();
                if (storage != null) {
                    while (!storage.IsEmpty()) {
                        Rigidbody itemRB = storage.TakeFromStorage().GetComponent<Rigidbody>();
                        if (itemRB != null) {
                            itemRB.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                        }
                    }
                }
                Rigidbody cRB = c.gameObject.GetComponent<Rigidbody>();
                if (cRB != null) {
                    cRB.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                }
                if (c.gameObject.CompareTag("waste")) {
                    Debug.Log(c.gameObject);
                    c.gameObject.GetComponent<Waste>().Explode();
                }
            }
        }
    }
}

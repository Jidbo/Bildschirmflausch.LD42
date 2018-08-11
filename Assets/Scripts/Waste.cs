using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour {

    private bool collided;

    private void Start() {
        collided = false;
	}

	private void OnCollisionEnter(Collision collision) {
        Waste other = collision.collider.GetComponent<Waste>();
        if (other != null && !other.collided && !collided) {
            other.setCollided(true);
            collided = true;
            // TODO EXPLOSION
            Debug.Log("EXPLOSION");
        }
	}

    public void setCollided(bool collided) {
        this.collided = collided;
    }
}

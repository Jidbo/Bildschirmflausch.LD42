using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    GameObject manager;

	// Use this for initialization
	void Start () {
        manager = GameController.instance.PowerUpManager;
    }
	
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            manager.GetComponent<PowerUpManager>().OnPickup("slow");
            Destroy(gameObject);
        }
    }
}

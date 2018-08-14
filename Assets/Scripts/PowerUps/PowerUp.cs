using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    public float timeout;
    private float despawnTime;

    GameObject manager;

	// Use this for initialization
	void Start () {
        manager = GameController.instance.PowerUpManager;
        despawnTime = timeout;
    }

    public void Update() {
        despawnTime -= Time.deltaTime;
    }

    public void FixedUpdate() {
        if (despawnTime < 0)
            Destroy(gameObject);
    }
	
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            manager.GetComponent<PowerUpManager>().OnPickup("slow");
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

    [SerializeField]
    private float timeTillDeactivate;

    private float timeTillDespawn = 5;
    private float despawnTimer;
    private float deactivateTimer;
    private bool active;

    private void Start() {
        despawnTimer = timeTillDespawn;
        deactivateTimer = timeTillDeactivate;
        active = false;
    }

    public void Update() {
        if (active) {
            deactivateTimer -= Time.deltaTime;
        } else {
            despawnTimer -= Time.deltaTime;
        }
    }

    public void FixedUpdate() {
        if (active) {
            OnActive();
        }
        if (active && deactivateTimer <= 0) {
            OnDeactivate();
            Destroy(gameObject);
        } else if(!active && despawnTimer <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!active && other.gameObject.CompareTag("Player")) {
            OnActivate();
            active = true;
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            if(renderer != null) {
                renderer.enabled = false;
            }
        }
    }

    protected abstract void OnActivate();

    protected abstract void OnActive();

    protected abstract void OnDeactivate();
}

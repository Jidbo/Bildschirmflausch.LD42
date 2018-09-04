using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlowSpawners : PowerUp {
    private ObjectSpawner[] spawners;
    private float speedMultiplier = 1.5f;

    protected override void OnActivate() {
        spawners = FindObjectsOfType<ObjectSpawner>();
        foreach (ObjectSpawner spawner in spawners) {
            if (spawner != null && spawner.isActiveAndEnabled) {
                spawner.cooldown *= speedMultiplier;
            }
        }
    }

    protected override void OnActive() {

    }

    protected override void OnDeactivate() {
        spawners = FindObjectsOfType<ObjectSpawner>();
        foreach (ObjectSpawner spawner in spawners) {
            if (spawner != null && spawner.isActiveAndEnabled) {
                spawner.cooldown /= speedMultiplier;
            }
        }
    }
}

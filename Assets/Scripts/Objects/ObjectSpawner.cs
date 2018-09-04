using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    [SerializeField]
    public float cooldown = 2.5f;
    [SerializeField]
    private float cooldownVariation = 0.0f;
    [SerializeField]
    private List<GameObject> toSpawn;
    [SerializeField]
    private int spawnAmount = 1;

    private System.Random rnd;
    [SerializeField]
    public float timeTillSpawning;

    void Start() {
        rnd = new System.Random();
        timeTillSpawning = GetNextCooldown() / 3;
    }

    private void Update() {
        timeTillSpawning -= Time.deltaTime;
    }

    void FixedUpdate() {
        if (timeTillSpawning <= 0) {
            timeTillSpawning = GetNextCooldown();
            NextSpawnWave();
        }
    }

    private void NextSpawnWave() {
        if (toSpawn.Count > 0) {
            for (int i = 0; i < spawnAmount; i++) {
                GameObject newObstacle = Instantiate(toSpawn[rnd.Next(toSpawn.Count)]);
                newObstacle.transform.position = transform.position;
            }
        }
    }

    private float GetNextCooldown() {
        return cooldown + (((float) rnd.NextDouble() * 2 - 1) * cooldownVariation);
    }
}

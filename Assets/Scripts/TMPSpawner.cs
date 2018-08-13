using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPSpawner : MonoBehaviour {
    [SerializeField]
    public float cooldown = 2.5f;
    [SerializeField]
    public float timeTillSpawning;
    [SerializeField]
    public List<GameObject> toSpawn;
    [SerializeField]
    public float rotation = 30f;

    System.Random rnd;

    void Start() {
        rnd = new System.Random();
    }

    private void Update() {
        timeTillSpawning -= Time.deltaTime;
    }

    void FixedUpdate() {
        if (timeTillSpawning <= 0) {
            timeTillSpawning = cooldown;
            SpawnObject();
        }
    }

    private void SpawnObject() {
        if (toSpawn.Count > 0) {
            GameObject newObstacle = Instantiate(toSpawn[rnd.Next(toSpawn.Count)]);
            newObstacle.transform.position = transform.position;
            if (newObstacle.CompareTag("waste")) {
                GameController.instance.updateScore(10);
            }
        }
    }
}

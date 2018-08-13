using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    [SerializeField]
    GameObject spawnerCopyWaste;
    [SerializeField]
    GameObject spawnerCopyPlank;
    [SerializeField]
    GameObject spawnerCopyCrate;
    [SerializeField]
    GameObject powerUp;

    public bool running;
    private float timeLeft;
    private float duration = 30;
    private float spawnerCooldownWaste;
    private float spawnerCooldownPlank;
    private float spawnerCooldownCrate;
    private string powerUpType;
    private bool powerUpEnabled;

	// Use this for initialization
	void Start () {
        running = false;
        powerUpEnabled = false;
        spawnerCooldownWaste = spawnerCopyWaste.GetComponent<TMPSpawner>().cooldown;
        spawnerCooldownPlank = spawnerCopyPlank.GetComponent<TMPSpawner>().cooldown;
        spawnerCooldownCrate = spawnerCopyCrate.GetComponent<TMPSpawner>().cooldown;
        powerUpType = "";
	}
	
	// Update is called once per frame
	void Update () {


        if (powerUpType.Equals("slow")) {
            if (running) {
                if (!powerUpEnabled) {
                    spawnerCopyWaste.GetComponent<TMPSpawner>().cooldown = spawnerCooldownWaste * 1.337f;
                    spawnerCopyPlank.GetComponent<TMPSpawner>().cooldown = spawnerCooldownPlank * 1.337f;
                    spawnerCopyCrate.GetComponent<TMPSpawner>().cooldown = spawnerCooldownCrate * 1.337f;
                    powerUpEnabled = true;
                    timeLeft = duration;
                }

                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0) {
                    running = false;
                    powerUpEnabled = false;
                    spawnerCopyWaste.GetComponent<TMPSpawner>().cooldown = spawnerCooldownWaste;
                    spawnerCopyPlank.GetComponent<TMPSpawner>().cooldown = spawnerCooldownPlank;
                    spawnerCopyCrate.GetComponent<TMPSpawner>().cooldown = spawnerCooldownCrate;
                }
            }        
        }


	}


    public void OnPickup(string type) {
        powerUpType = type;
        running = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    StoragePlayer storage;
    BoxCollider collider;

	void Start () {
        storage = GetComponent(typeof(StoragePlayer)) as StoragePlayer;
        collider = GetComponent<BoxCollider>();
	}

    // Update is called once per frame
    void Update() {

    }
}

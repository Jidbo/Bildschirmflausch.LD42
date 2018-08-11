using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    StoragePlayer playerStorage;
    [SerializeField]
    GameObject spaceCheck;
    [SerializeField]
    LayerMask obstacleLayerMask;

    void Start() {
        playerStorage = GetComponent<StoragePlayer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            //gets all objects in front of the player
            Collider[] colliders = Physics.OverlapBox(spaceCheck.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.rotation, obstacleLayerMask);

            // tries to drop the item
            if (colliders.Length == 0 && playerStorage.GetFromStorage()) {
                return;
            }
            //runs through all colliders
            foreach (Collider c in colliders) {
                //tries to get the collider's Storage component and saves it in colliderStorage
                Storage colliderStorage = c.gameObject.GetComponent<Storage>();

                //checks if the collider has a Storage attached
                if (colliderStorage != null) {
                    //checks if the player is holding an item
                    if (playerStorage.IsFull()) {
                        //takes the item from the player 
                        GameObject obj = playerStorage.GetFromStorage();
                        //tries to insert the item into the object's storage
                        if (colliderStorage.AddToStorage(obj)) {
                            return;
                        } else {
                            //returns the item to the player, if storing failed
                            playerStorage.AddToStorage(obj);
                        }
                    } else {
                        if (!colliderStorage.IsEmpty()) {
                            //takes an item from the object's storage and transfers it to the player's storage
                            playerStorage.AddToStorage(colliderStorage.GetFromStorage());
                            return;
                        } else {
                            //tries to transfer the gameobject to the player's storage
                            if (playerStorage.AddToStorage(c.gameObject)) {
                                return;
                            }
                        }
                    }
                } else {
                    //if the collider has no storage attached (e.g. crate)
                    //tries to add the collider's gameobject to the player
                    if (playerStorage.AddToStorage(c.gameObject)) {
                        return;
                    }
                }
            }
        }
    }
}

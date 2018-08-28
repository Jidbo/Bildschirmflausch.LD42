using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Storage playerStorage;
    [SerializeField]
    GameObject spaceCheck;
    [SerializeField]
    LayerMask obstacleLayerMask;

    void Start() {
        playerStorage = GetComponent<Storage>();
    }

    private void UseAction() {
        //gets all colliders
        Collider[] colliders = Physics.OverlapBox(spaceCheck.transform.position, new Vector3(1.5f, 1.5f, 0.75f), transform.rotation, obstacleLayerMask);
        //checker if there is space in front of the player
        bool isSpaceInFront = true;
        //the Item the player currently holds
        GameObject playerItem = playerStorage.Remove();
        //runs through all colliders
        foreach (Collider c in colliders) {
            //there is no space when there is a collider on front
            isSpaceInFront = false;
            //the Storage of the collider
            Storage colliderStorage = c.gameObject.GetComponent<Storage>();
            //when the collider has a Storage component
            if (colliderStorage != null) {
                //when the player holds an Item
                if (playerItem != null) {
                    //tries to add the held item to the collider-Storage
                    if (colliderStorage.Add(playerItem)) {
                        return;
                    }
                }
                //when the player doesn't hold an Item
                else {
                    //takes an Item from the colliderStorage
                    GameObject obj = colliderStorage.Remove();
                    //when there is no takeable item
                    if (obj == null) {
                        //tries to add the collider to the playerstorage
                        if (playerStorage.Add(c.gameObject)) {
                            return;
                        }
                    }
                    //when there is an item
                    else {
                        //tries to add the item to the playerstorage
                        if (playerStorage.Add(obj)) {
                            return;
                        }
                        //returns the item to the colliderstorage
                        else {
                            colliderStorage.Add(obj);
                        }
                    }
                }
            }
            //tries to add the collider to the playerstorage
            else if (playerItem == null && playerStorage.Add(c.gameObject)) {
                return;
            }
        }
        //adds the currently held item back to the playerStorage when no space is found
        if (playerItem != null && !isSpaceInFront) {
            playerStorage.Add(playerItem);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            UseAction();
        }
        GetComponent<Animator>().SetBool("isLiftUp", playerStorage.IsFull());
    }
}

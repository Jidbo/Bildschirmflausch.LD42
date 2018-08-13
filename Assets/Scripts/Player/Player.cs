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

    private void UseAction() {
        //gets all objects in front of the player
        Collider[] colliders = Physics.OverlapBox(spaceCheck.transform.position, new Vector3(1.5f, 1.5f, 0.75f), transform.rotation, obstacleLayerMask);
        bool isSpaceInFront = true;

        //runs through all colliders
        foreach (Collider c in colliders)
        {
            if (c.gameObject.layer == 12) {
                isSpaceInFront = false;
            }
            if (c.gameObject.transform.parent == null)
            {
                isSpaceInFront = false;
                //tries to get the collider's Storage component and saves it in colliderStorage
                Storage colliderStorage = c.gameObject.GetComponent<Storage>();

                //checks if the collider has a Storage attached
                if (colliderStorage != null)
                {
                    //checks if the player is holding an item
                    if (playerStorage.IsFull())
                    {
                        //takes the item from the player 
                        GameObject obj = playerStorage.TakeFromStorage();
                        //tries to insert the item into the object's storage
                        if (colliderStorage.AddToStorage(obj))
                        {
                            return;
                        }
                        else
                        {
                            //returns the item to the player, if storing failed
                            playerStorage.AddToStorage(obj);
                        }
                    }
                    else
                    {
                        GameObject obj = colliderStorage.TakeFromStorage();
                        if (obj == null)
                        {
                            playerStorage.AddToStorage(c.gameObject);
                            return;
                        }
                        else
                        {
                            if (playerStorage.AddToStorage(obj))
                            {
                                return;
                            }
                            else
                            {
                                colliderStorage.AddToStorage(obj);
                            }
                        }
                    }
                }
                else if (playerStorage.AddToStorage(c.gameObject))
                {
                    return;
                }
            }
        }
        //tries to drop the current Item
        if (isSpaceInFront)
        {
            playerStorage.TakeFromStorage();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            UseAction();
        }
        Animator playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("isLiftUp", (playerStorage.IsFull() ? true : false));
    }
}

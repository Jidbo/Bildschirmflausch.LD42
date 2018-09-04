using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSplitShelf : PowerUp {

    [SerializeField]
    private GameObject shelf;

    protected override void OnActivate() {
        GameObject[] bigShelves = GameObject.FindGameObjectsWithTag("BigShelf");
        foreach(GameObject bigShelf in bigShelves) {
            Vector3 oldUp = bigShelf.transform.up;
            if(bigShelf.transform.up.y <= 0.5) {
                Vector3 oldPos = bigShelf.transform.position;
                Quaternion oldRotation = bigShelf.transform.rotation;
                Destroy(bigShelf);
                GameObject shelfBottom = Instantiate(shelf);
                shelfBottom.transform.rotation = oldRotation;
                shelfBottom.transform.position = oldPos;
                GameObject shelfTop = Instantiate(shelf);
                shelfTop.transform.rotation = oldRotation;
                shelfTop.transform.position = oldPos + (oldUp * 1.5f);
                return;
            }
        }
    }

    protected override void OnActive() {

    }

    protected override void OnDeactivate() {

    }
}

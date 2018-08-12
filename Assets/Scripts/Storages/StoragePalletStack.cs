using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePalletStack : Storage {

    [SerializeField]
    GameObject pallet;

    public StoragePalletStack() : base(new string[] { "pallet" }) {

    }

    protected override void OnObjectAdded(GameObject go) {
        
    }

    protected override void OnObjectRemoved(GameObject go) {
        if (IsEmpty()) {
            GameObject newPallet = UnityEngine.Object.Instantiate(pallet);
            newPallet.transform.position = transform.position;
            newPallet.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }
}

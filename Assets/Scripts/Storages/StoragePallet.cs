using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePallet : Storage {

    [SerializeField]
    GameObject shelf;

    [SerializeField]
    GameObject palletStack;

    public StoragePallet() : base(new string[] { "pallet", "crate" }) {

    }

    protected override void OnObjectAdded(GameObject go) {
        if (IsFull()) {
            GameObject newShelf = UnityEngine.Object.Instantiate(shelf);
            newShelf.transform.position = transform.position;
            newShelf.transform.rotation = transform.rotation;
            foreach (GameObject c in content) {
                Destroy(c);
            }
            Destroy(gameObject);
        }
        else if (go.CompareTag("pallet")) {
            GameObject newPalletStack = Instantiate(palletStack);
            newPalletStack.transform.position = transform.position;
            newPalletStack.transform.rotation = transform.rotation;
            StoragePalletStack palletStackStorage = newPalletStack.GetComponent<StoragePalletStack>();
            Debug.Log(palletStackStorage.IsFull());
            palletStackStorage.AddToStorage(go);
            Destroy(gameObject);
        }
    }

    protected override bool CanStore(GameObject go) {
        if (go.CompareTag("pallet"))
            return IsEmpty();
        return base.CanStore(go);
    }

    protected override void OnObjectRemoved(GameObject go) {

    }
}

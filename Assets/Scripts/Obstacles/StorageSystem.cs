using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSystem : MonoBehaviour {
    enum EnumStorageType { EXCLUDING, ADDITIVE };
    [SerializeField]
    List<Storage> storages;
    [SerializeField]
    EnumStorageType type;

    public bool Add(GameObject obj) {
        if (IsStanding()) {
            if(type == EnumStorageType.ADDITIVE || IsEmpty()) {
                foreach (Storage storage in storages) {
                    if (storage.Add(obj)) {
                        return true;
                    }
                }
            } else if(type == EnumStorageType.EXCLUDING) {
                foreach (Storage storage in storages) {
                    if (storage.IsEmpty()) {
                        continue;
                    } else {
                        return storage.Add(obj);
                    }
                }
            }
        }
        return false;
    }

    public GameObject Remove() {
        foreach (Storage storage in storages) {
            GameObject removedItem = storage.Remove();
            if (removedItem != null) {
                return removedItem;
            }
        }
        return null;
    }

    public List<GameObject> RemoveAllContents() {
        List<GameObject> removedItems = new List<GameObject>();
        foreach(Storage storage in storages) {
            removedItems.AddRange(storage.RemoveAllContents());
        }
        return removedItems;
    }

    public bool IsEmpty() {
        foreach(Storage storage in storages) {
            if (!storage.IsEmpty()) {
                return false;
            }
        }
        return true;
    }

    public bool IsFull() {
        foreach (Storage storage in storages) {
            if (!storage.IsFull()) {
                return false;
            }
        }
        return true;
    }

    private void FixedUpdate() {
        if (!IsStanding()) {
            RemoveAllContents();
        }
    }

    public bool IsStanding() {
        return transform.up.y >= 0.9;
    }
}

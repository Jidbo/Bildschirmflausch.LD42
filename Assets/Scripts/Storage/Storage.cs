using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {
    [SerializeField]
    int scoreForFilling;
    [SerializeField]
    Vector3 minSize;
    [SerializeField]
    Vector3 maxSize;
    protected GameObject[] contents;
    [SerializeField]
    protected List<Transform> positions;

    bool wasFilled = false;

    private void Awake() {
        GameObject[] oldContent = contents;
        contents = new GameObject[positions.Count];
        if (oldContent != null) {
            for (int i = 0; i < oldContent.Length && i < contents.Length; i++) {
                contents[i] = oldContent[i];
            }
        }
    }

    /// <summary>
    /// Adds the given GameObject to the first empty slot
    /// </summary>
    /// <param name="newGO">The gameobject which should be stored.</param>
    /// <returns>Returns true, if the object is storable</returns>
    public bool Add(GameObject newGO) {
        int pos = GetPositionToAddTo();
        if (CanStore(newGO) && pos >= 0) {
            addToStorage(pos, newGO);
            return true;
		} else {
            return false;
        }
    }

    private int GetPositionToAddTo() {
        for(int i = 0; i < contents.Length; i++) {
            if(contents[i] == null) {
                return i;
            }
        }
        return -1;
    }

    private int GetPositionToTakeFrom() {
        for (int i = contents.Length - 1; i >= 0; i--) {
            if (contents[i] != null) {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Gets called when an object is added to the storage (Used for crafting)
    /// </summary>
    /// <param name="go">The object which should be added.</param>
    protected virtual void OnObjectAdded(GameObject go) {
        if (!wasFilled && IsFull()) {
            wasFilled = true;
            try {
                GameController.instance.updateScore(scoreForFilling);
            } catch(Exception e) {
                Debug.Log(e);
            }
        }
    }

    /// <summary>
    /// Checks whether or not the type of the given GameObject can be stored
    /// </summary>
    /// <param name="go">The Gameobject wich should be checked.</param>
    /// <returns>True, if the object is storable.</returns>
    private bool CanStore(GameObject go) {
        StorableItem item = go.GetComponent<StorableItem>();
        return item != null && FitsStorage(item);
    }

    private bool FitsStorage(StorableItem item) {
        return ((minSize.x < 0 || minSize.x <= item.GetSize().x) && (maxSize.x < 0 || item.GetSize().x <= maxSize.x))
            && ((minSize.y < 0 || minSize.y <= item.GetSize().y) && (maxSize.y < 0 || item.GetSize().y <= maxSize.y))
            && ((minSize.z < 0 || minSize.z <= item.GetSize().z) && (maxSize.z < 0 || item.GetSize().z <= maxSize.z));
    }

    /// <summary>
    /// Removes and returns the first GameObject it finds
    /// </summary>
    /// <returns>The first gameobject found.</returns>
    public GameObject Remove() {
        int pos = GetPositionToTakeFrom();
        if (pos >= 0) {
            return removeFromStorage(pos);
        }
        return null;
    }

    protected virtual void OnObjectRemoved(GameObject go) {

    }

    private void addToStorage(int pos, GameObject newGO) {
        contents[pos] = newGO;
        newGO.transform.parent = positions[pos];
        newGO.transform.localPosition = Vector3.zero;
        newGO.transform.localRotation = new Quaternion();
        Rigidbody rb = newGO.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.detectCollisions = false;
        OnObjectAdded(newGO);
    }

    private GameObject removeFromStorage(int pos) {
        GameObject lastItem = contents[pos];
        if (lastItem != null) {
            contents[pos] = null;
            lastItem.transform.parent = null;
            Rigidbody rb = lastItem.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.detectCollisions = true;
            OnObjectRemoved(lastItem);
            return lastItem;
        }
        return null;
    }

    public List<GameObject> RemoveAllContents() {
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < contents.Length; i++) {
            GameObject obj = removeFromStorage(i);
            if(obj != null) {
                objects.Add(obj);
            }
        }
        return objects;
    }

    public bool IsEmpty() {
        foreach (GameObject go in contents) {
            if (go != null) {
                return false;
            }
        }
        return true;
    }

    public bool IsFull() {
        foreach(GameObject go in contents) {
            if (go == null) {
                return false;
            }
        }
        return true;
    }

    public List<GameObject> GetAllContents() {
        List<GameObject> allContents = new List<GameObject>();
        foreach(GameObject content in contents) {
            if (content != null) {
                allContents.Add(content);
            }
        }
        return allContents;
    }
}

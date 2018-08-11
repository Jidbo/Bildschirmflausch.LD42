using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour {
    
    protected int maxCapacity;

    protected List<GameObject> content;
    ArrayList storableTags = new ArrayList();

    private void Start() {
        content = new List<GameObject>();
    }

    /// <summary>
    /// Constructor for a storage object.
    /// </summary>
    /// <param name="storableTags">A list of tags which can be stored.</param>
    public Storage(string[] storableTags, int maxCapacity) {
        this.storableTags.AddRange(storableTags);
        this.maxCapacity = maxCapacity;
    }

    /// <summary>
    /// Adds the given GameObject to the first empty slot
    /// </summary>
    /// <param name="newGO">The gameobject which should be stored.</param>
    /// <returns>Returns true, if the object is storable</returns>
    public bool AddToStorage(GameObject newGO) {
        Debug.Log("Collect Item");
        // TODO correct scaling and position
        if (!IsFull() && CanStore(newGO)) {
            content.Add(newGO);
            OnObjectAdded(newGO);
            newGO.transform.parent = gameObject.transform;
            Rigidbody rb = newGO.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;
            return true;
		} else {
            return false;
        }
    }

    /// <summary>
    /// Gets called when an object is added to the storage (Used for crafting)
    /// </summary>
    /// <param name="go">The object which should be added.</param>
    protected abstract void OnObjectAdded(GameObject go);
    
    /// <summary>
    /// Checks whether or not the type of the given GameObject can be stored
    /// </summary>
    /// <param name="go">The Gameobject wich should be checked.</param>
    /// <returns>True, if the object is storable.</returns>
    protected bool CanStore(GameObject go) {
        return storableTags.Contains(go.tag);
    }

    /// <summary>
    /// Removes and returns the first GameObject it finds
    /// </summary>
    /// <returns>The first gameobject found.</returns>
    public GameObject GetFromStorage() {
        if (!IsEmpty()) {
            GameObject lastItem = content[content.Count - 1];
            content.Remove(lastItem);
            lastItem.transform.parent = null;
            Rigidbody rb = lastItem.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.detectCollisions = true;
            return lastItem;
        }
        return null;
    }

    /// <summary>
    /// Returns true when no objects are stored
    /// </summary>
    public bool IsEmpty() {
        if (content.Count <= 0) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true when the capacity limit is reached
    /// </summary>
    public bool IsFull() {
        if (content.Count >= maxCapacity) {
            return true;
        }
        return false;
    }
}

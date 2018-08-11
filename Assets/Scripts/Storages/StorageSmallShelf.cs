using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSmallShelf : StorageShelf
{
    /// <summary>
    /// Crafts a big shelf out of itself and another shelf (out of its content)
    /// if the content is empty otherwise
    /// </summary>
    protected override void OnObjectAdded(GameObject go)
    {
        bool craft = false;
        if (go.CompareTag("shelf")) {
           if (this.content[0].CompareTag("shelf")) {
                craft = true;
                for (int i = 1; i < content.Length; i++) {
                    if (content[i] != null) {
                        craft = false;
                        break;
                    }
                }

                if (craft) {
                  // craft BigShelf
                }
            }
        }     
    }
}

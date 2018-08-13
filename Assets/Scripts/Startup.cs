using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Startup : MonoBehaviour
{
    [SerializeField]
    GameObject bigShelf;
    [SerializeField]
    GameObject shelf;
    [SerializeField]
    GameObject waste;

    public Startup()
    {
        GameObject newBigShelf01 = Instantiate(bigShelf);
        newBigShelf01.transform.position = new Vector3((long) 6.41811, (long) -2.861023e-06, (long) -5.716771);
        newBigShelf01.transform.rotation = new Quaternion(0, (long) -53.662, 0, 0);
    }
}
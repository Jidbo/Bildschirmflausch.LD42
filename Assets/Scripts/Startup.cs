using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Startup : MonoBehaviour {
    [SerializeField]
    GameObject bigShelf;
    [SerializeField]
    GameObject shelf;
    [SerializeField]
    GameObject waste;

    private void Start() {
        GameObject newBigShelf01 = Instantiate(bigShelf);
        newBigShelf01.transform.position = new Vector3((long) 6.41811, (long) -2.861023e-06, (long) -5.716771);
        newBigShelf01.transform.Rotate(0, (float)-53.662, 0);

        /* Remove the broken shelf */
        //GameObject newBigShelf02 = Instantiate(bigShelf);
        //newBigShelf02.transform.position = new Vector3((long)4.466072, (long)1.506221, (long)6.428141);
        //newBigShelf02.transform.Rotate(0, (float)47.826, (float)-90.172);

        GameObject newBigShelf03 = Instantiate(bigShelf);
        newBigShelf03.transform.position = new Vector3((long)-6.767082, (long)0.003471136, (long)0.7262506);
        newBigShelf03.transform.Rotate(0, (float)-59.176, 0);
        newBigShelf03.GetComponent<Storage>().Add(Instantiate(waste));
        newBigShelf03.GetComponent<Storage>().Add(Instantiate(waste));
    }
}
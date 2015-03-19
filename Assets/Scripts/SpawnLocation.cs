using UnityEngine;
using System.Collections;

public class SpawnLocation : MonoBehaviour {
    public bool HasChild;       // whether or not this node holds an object spawned at it

    // should suffice in most cases for accessing the node spawned at this location
    public Transform GetFirstChild() {
        return transform.GetChild(0);
    }
}

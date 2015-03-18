using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    void Start() {
        this.GetComponent<GroundTileSpawner>().SpawnInitialGroundTiles();
    }
}

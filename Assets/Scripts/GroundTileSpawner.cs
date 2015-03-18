using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundTileSpawner : MonoBehaviour {

    public GameObject groundTile;
    public GameObject empty;
    private List<Vector3> spawnLocations;
    private int tileCount = 0;
    public GameObject cityGameObject;
    public GameObject forestGameObject;

    void Awake() {
        spawnLocations = new List<Vector3>();
        CreateSpawnLocObject(Vector3.zero);

        // ignore these or something for now
        //empty = new GameObject();
        //cityGameObject = new GameObject();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnGroundTile();
        }
    }

    public void SpawnInitialGroundTiles() {
        // spawn 3 ground tiles initially
        print("initial tiles");
        SpawnGroundTile();
        SpawnGroundTile();
        SpawnGroundTile();
    }

    public void SpawnGroundTile() {
        int index = Random.Range(0, spawnLocations.Count - 1);
        Vector3 location = spawnLocations[index];
        GameObject tempNodeObject = Instantiate(groundTile, location, Quaternion.identity) as GameObject;

        if (tileCount == 0) {
            SpawnNode(tempNodeObject, cityGameObject);
        } else {
            // spawn some extra trees
            SpawnNode(tempNodeObject, forestGameObject);
        }
        SpawnNode(tempNodeObject, forestGameObject);
        SpawnNode(tempNodeObject, forestGameObject);


        CreateAdjacentSpawnLocations(tempNodeObject.transform.position);

        tileCount++;
        spawnLocations.RemoveAt(index);
    }

    // set up all platform spawn locations on all 4 sides of the recently spawned platform
    private void CreateAdjacentSpawnLocations(Vector3 loc) {
        Vector3 newLoc = Vector3.zero;
        int width = 10;
        newLoc = new Vector3(loc.x - width, 0.0f, loc.z);    // left
        CheckAndSpawn(newLoc);
        newLoc = new Vector3(loc.x + width, 0.0f, loc.z);    // right
        CheckAndSpawn(newLoc);
        newLoc = new Vector3(loc.x, 0.0f, loc.z + width);    // up
        CheckAndSpawn(newLoc);
        newLoc = new Vector3(loc.x, 0.0f, loc.z - width);    // down
        CheckAndSpawn(newLoc);
    }

    // make sure there's nothing already spawned where we're going to
    private void CheckAndSpawn(Vector3 pos) {
        if (!Physics.CheckSphere(pos, 0.1f)) CreateSpawnLocObject(pos);
    }

    private void CreateSpawnLocObject(Vector3 pos) {
        GameObject spawnLoc;
        spawnLoc = Instantiate(empty, pos, Quaternion.identity) as GameObject;
        spawnLoc.transform.parent = this.transform;
        spawnLocations.Add(spawnLoc.transform.position);
    }

    private void SpawnNode(GameObject currentGroundTile, GameObject itemToSpawn) {
        currentGroundTile.GetComponent<GroundTile>().SpawnNode(itemToSpawn);
    }
}

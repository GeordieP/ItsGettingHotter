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
    public GameObject oilGameObject;
	public GameObject farmGameObject;

    void Awake() {
        spawnLocations = new List<Vector3>();
        CreateSpawnLocObject(Vector3.zero);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnCityTile();
        }
    }

    public void SpawnInitialGroundTiles() {
        // spawn 3 ground tiles initially
        SpawnCityTile();
    }

	// Randomly choose tiles to spawn - called when a city has a surplus of materials, ie. enough to build a new city
	public void SpawnExpansionTiles() {
		bool citySpawned = false;		// keep track of whether we've spawned a city during this expansion, since there should only be one allowed per expansion

		// for now we'll do 3 tiles - move this to balance later if necessary
		for (int i = 0; i < 3; i++) {
			if (Random.Range(0, 10) < 3 && !citySpawned) {		// 30 percent chance of a city spawning
				SpawnCityTile();
				citySpawned = true;
			} else {
				SpawnResourceTile();
			}

			switch (Random.Range(0, 3)) {
				case 0:
					if (!citySpawned) {
						SpawnCityTile();
						citySpawned = true;
					} else {
						SpawnResourceTile();
					}
					break;
				case 1:
					SpawnResourceTile();
					break;
				case 2:
					break;
			}
		}
	}

    // spawn a tile with a city on it
    public void SpawnCityTile() {
		int index = Random.Range(0, spawnLocations.Count - 1);
        Vector3 location = spawnLocations[index];
        GameObject groundTileObject = Instantiate(groundTile, location, Quaternion.identity) as GameObject;

        SpawnNode(groundTileObject, cityGameObject);
		SpawnNodesOnTile(groundTileObject);

        CreateAdjacentSpawnLocations(groundTileObject.transform.position);

        tileCount++;
        spawnLocations.RemoveAt(index);
    }


    // spawn a tile with no cities, just resources
    // maybe we'll be able to build cities on these later? who knows
    public void SpawnResourceTile() {
        int index = Random.Range(0, spawnLocations.Count - 1);
        Vector3 location = spawnLocations[index];
		GameObject groundTileObject = Instantiate(groundTile, location, Quaternion.identity) as GameObject;

		SpawnNodesOnTile(groundTileObject);


        CreateAdjacentSpawnLocations(groundTileObject.transform.position);

        tileCount++;
        spawnLocations.RemoveAt(index);
    }

	private void SpawnNodesOnTile(GameObject groundTileObject) {
		int nodeCount = Random.Range(4, 10);		// decide how many nodes are going to be on this tile
		for (int i = 0; i < nodeCount; i++) {
			switch (Random.Range(0, 3)) {			// decide which node type to spawn at each location
				// for now hard code the number of types // TODO: rewrite with something better
				case 0:
					SpawnNode(groundTileObject, farmGameObject);
					break;
				case 1:
					SpawnNode(groundTileObject, oilGameObject);
					break;
				case 2:
					SpawnNode(groundTileObject, forestGameObject);
					break;
			}
		}
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
        //spawnLoc.transform.parent = this.transform;
        spawnLocations.Add(spawnLoc.transform.position);
    }

    private void SpawnNode(GameObject currentGroundTile, GameObject itemToSpawn) {
        currentGroundTile.GetComponent<GroundTile>().SpawnNode(itemToSpawn);
    }

	//private void OnDrawGizmos() {
	//	Gizmos.color = Color.red;
	//	foreach (Vector3 g in spawnLocations) {
	//		Gizmos.DrawSphere(g, 0.5f);
	//	}
	//}
}

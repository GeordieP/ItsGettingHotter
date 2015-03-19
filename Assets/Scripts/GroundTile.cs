using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour {
    private List<Transform> spawnLocations;
    private int groundTileSize;

    public GameObject temp;

    void Awake() {
        spawnLocations = new List<Transform>();
        groundTileSize = 10;        // a tile is 10 units by 10 units
        int num = 10;

        for (int i = -(num / 2) + 1; i < (num / 2); i += groundTileSize / (num / 2)) {
            for (int j = -(num / 2) + 1; j < (num / 2); j += groundTileSize / (num / 2)) {
                // TODO: these are spawning at Y 0, so they'll probably be inside something. fix it
                // what does the above comment even mean, is it relevant anymore?

                //spawnLocations.Add(new Vector3((i) + transform.position.x, 1.5f, (j) + transform.position.z));
                Vector3 location = new Vector3((i) + transform.position.x, 1.5f, (j) + transform.position.z);
                GameObject lol = Instantiate(temp, location, Quaternion.identity) as GameObject;
                lol.transform.parent = this.transform.FindChild("GroundTileContent").transform;
                spawnLocations.Add(lol.transform);
            }
        }
    }

	// This gets called from GroundTileSpawner right now, which seems a bit strange
    public void SpawnNode(GameObject nodeGameObject) {
        int randomSpanwLoc = Random.Range(0, spawnLocations.Count - 1);
        SpawnLocation nodeToSpawnAt = spawnLocations[randomSpanwLoc].transform.GetComponent<SpawnLocation>();

        while (nodeToSpawnAt.HasChild) {
			// In the case the desired node already has something in it, pick a different one
            randomSpanwLoc = Random.Range(0, spawnLocations.Count - 1);
            nodeToSpawnAt = spawnLocations[randomSpanwLoc].transform.GetComponent<SpawnLocation>();
        }

        GameObject newNode = Instantiate(nodeGameObject, spawnLocations[randomSpanwLoc].transform.position, Quaternion.identity) as GameObject;
        newNode.transform.parent = spawnLocations[randomSpanwLoc].transform;
        nodeToSpawnAt.HasChild = true;
    }
}

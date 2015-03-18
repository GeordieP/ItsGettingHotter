using UnityEngine;
using System.Collections;

public class HomeNode : MonoBehaviour {
    public GameObject unitPrefab;

	// Use this for initialization
	void Start () {
        SpawnUnit(5);
	}
	
    void SpawnUnit(int numToSpawn = 1) {
        for (int i = 0; i < numToSpawn; i++) {
            GameObject tempUnit = Instantiate(unitPrefab, this.transform.position + RandomVec3(), Quaternion.identity) as GameObject;
            tempUnit.GetComponent<Unit>().homeNode = this.transform;
            tempUnit.transform.parent = this.transform;     // TODO: probably shouldnt parent the units to nodes, as when the node is destroyed as will be the child
            //print(tempUnit.GetComponent<Unit>());
            //Main.Instance.AllUnits.Add(tempUnit.GetComponent<Unit>());
        }
        GameObject.Find("MAIN").GetComponent<ClickHandling>().RefreshAllUnitsList();
    }

    private Vector3 RandomVec3() {
        return new Vector3(Random.value, 0.0f, Random.value);
    }
}

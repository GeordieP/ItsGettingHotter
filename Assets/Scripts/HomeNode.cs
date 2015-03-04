using UnityEngine;
using System.Collections;

public class HomeNode : MonoBehaviour {
    public GameObject unitTemplate;
    private GameObject tempUnit;
    private Transform unitSpawnLocation;

	void Start () {
        unitSpawnLocation = transform.FindChild("UnitSpawnLoc");
        SpawnUnits(10);
	}

	void Update () {
	
	}

    public void SpawnUnits(int numUnits) {
        for (int i = 0; i < numUnits; i++) {
            SpawnUnit();
        }
    }

    public void SpawnUnit() {
        if (unitSpawnLocation)
            tempUnit = Instantiate(unitTemplate, unitSpawnLocation.position + GetRandomVector(), Quaternion.identity) as GameObject;
        else
            tempUnit = Instantiate(unitTemplate, this.transform.position, Quaternion.identity) as GameObject;
    }

    private Vector3 GetRandomVector() {
        return new Vector3(Random.value, Random.value, Random.value);
    }
}
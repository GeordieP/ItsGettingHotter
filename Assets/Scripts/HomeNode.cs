using UnityEngine;
using System.Collections;

public class HomeNode : MonoBehaviour {
    public GameObject unitTemplate;
    private GameObject tempUnit;

	void Start () {
        SpawnUnits(3);
	}

	void Update () {
	
	}

    public void SpawnUnits(int numUnits) {
        for (int i = 0; i < numUnits; i++) {
            SpawnUnit();
        }
    }

    public void SpawnUnit() {
        tempUnit = Instantiate(unitTemplate, this.transform.position, Quaternion.identity) as GameObject;
    }
}

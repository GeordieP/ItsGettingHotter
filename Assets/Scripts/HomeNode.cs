using UnityEngine;
using System.Collections;

public class HomeNode : MonoBehaviour {
    public GameObject unitPrefab;

	private int WoodCount, IronCount, FoodCount;

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

	public void AcceptResources(ResourcePackage _resourcePackage) {
		// Add the resources to their appropriate counter
		switch (_resourcePackage.resourceType) {
			case ResourcePackage.ResourceType.Wood:
				WoodCount += _resourcePackage.ResourceCount;
				GameObject.Find("MAIN").GetComponent<Main>().AddResource(ResourcePackage.ResourceType.Wood, _resourcePackage.ResourceCount);
				//print("home node recieved " + _resourcePackage.ResourceCount + " wood");
				break;
			case ResourcePackage.ResourceType.Iron:
				IronCount += _resourcePackage.ResourceCount;
				break;
			case ResourcePackage.ResourceType.Food:
				FoodCount += _resourcePackage.ResourceCount;
				break;
			default:
				break;
		}
	}
}

using UnityEngine;
using System.Collections;

public class CityNode : Node {
	public GameObject unitPrefab;

	void Start() {
		StartCoroutine(SpawnUnitsDelayed());

		WoodCount = Balance.CityWoodStartCount;
		IronCount = Balance.CityIronStartCount;
		FoodCount = Balance.CityFoodStartCount;
	}

	private IEnumerator SpawnUnitsDelayed() {
		yield return new WaitForSeconds(0.5f);
		SpawnUnit(5);
	}

	private void SpawnUnit(int numToSpawn = 1) {
		for (int i = 0; i < numToSpawn; i++) {
			GameObject tempUnit = Instantiate(unitPrefab, this.transform.position + RandomVec3() * 2, Quaternion.identity) as GameObject;
		}
		GameObject.Find("MAIN").GetComponent<ClickHandling>().RefreshAllUnitsList();
	}

	private Vector3 RandomVec3() {
		return new Vector3(Random.value, 0.0f, Random.value);
	}

	public override UnitTask GetTask() {
		return new DepositTask();
	}

	public override void AcceptResources(ResourcePackage _resourcePackage) {
		// Add the resources to their appropriate counter
		switch (_resourcePackage.ResourceType) {
			case Balance.ResourceTypes.Wood:
				WoodCount += _resourcePackage.ResourceCount;
				GameObject.Find("MAIN").GetComponent<Main>().AddResource(Balance.ResourceTypes.Wood, _resourcePackage.ResourceCount);
				break;
			case Balance.ResourceTypes.Iron:
				IronCount += _resourcePackage.ResourceCount;
				break;
			case Balance.ResourceTypes.Food:
				FoodCount += _resourcePackage.ResourceCount;
				break;
			default:
				break;
		}

		CheckResourceCount();
	}

	private void CheckResourceCount() {
		// doing it in main doesnst seem to work
		GameObject.Find("MAIN").GetComponent<Main>().CheckResourceValues();

		//if (WoodCount >= 500) {
		//    // conditions to spawn a new tile and city
		//    GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnCityTile();
		//    GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnResourceTile();
		//}
	}
}

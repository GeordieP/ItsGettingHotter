using UnityEngine;
using System.Collections;

public class CityNode : Node {
	public GameObject unitPrefab;

	void Start() {
		// Do everything in init so we can call init on base as well
		Init();
	}

	protected override void Init() {
		NodeName = "City";
		StartCoroutine(SpawnUnitsDelayed());

		WoodCount = Balance.CityWoodStartCount;
		OilCount = Balance.CityOilStartCount;
		FoodCount = Balance.CityFoodStartCount;

		base.Init();
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
			case Balance.ResourceTypes.Oil:
				OilCount += _resourcePackage.ResourceCount;
				break;
			case Balance.ResourceTypes.Food:
				FoodCount += _resourcePackage.ResourceCount;
				break;
			default:
				break;
		}

		// this will just update the attached GUI - easier to have the update call in 
		base.AcceptResources(_resourcePackage);
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

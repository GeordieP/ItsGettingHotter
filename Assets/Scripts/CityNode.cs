using UnityEngine;
using System.Collections;

public class CityNode : Node {
	public GameObject unitPrefab;
	private StandaloneNodeHealthbar standaloneNodeHealthbar;
	private bool citySpawnCondition = true;


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

	void Update() {
		// Emit the city emissions amount every 1 second
		World.Instance.WorldCO2 += Balance.CityCo2Emission * Time.deltaTime;
		World.Instance.WorldCH4 += Balance.CityCh4Emission * Time.deltaTime;
	}

	private IEnumerator SpawnUnitsDelayed() {
		yield return new WaitForSeconds(0.5f);

		// call this afterwards, so the base class has a chance to set up some dependencies (eg finding the UI spawn loc child)
		standaloneNodeHealthbar = GameObject.Find("MAIN").GetComponent<Main>().NewCityHealthBar(this);

		if (standaloneNodeHealthbar) {
			standaloneNodeHealthbar.UpdateHealthBar(health / maxHealth);
		}


		SpawnUnit(5);
	}

	private void SpawnUnit(int numToSpawn = 1) {
		for (int i = 0; i < numToSpawn; i++) {
			Instantiate(unitPrefab, this.transform.position + new Vector3(0f, -0.19f, 0f) + (RandomVec3() * 2), Quaternion.identity);
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

	protected override void UpdateAttachedGUI() {
		if (standaloneNodeHealthbar) {
			standaloneNodeHealthbar.UpdateHealthBar(health / maxHealth);
		}

		base.UpdateAttachedGUI();
	}

	private void CheckResourceCount() {
		// doing it in main doesnst seem to work
		//GameObject.Find("MAIN").GetComponent<Main>().CheckResourceValues();

		//if (WoodCount >= 500) {
		//    // conditions to spawn a new tile and city
		//    GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnCityTile();
		//    GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnResourceTile();
		//}

		if (citySpawnCondition && WoodCount >= Balance.CityWoodCost && FoodCount >= Balance.CityFoodCost && OilCount >= Balance.CityOilCost) {
			// we've got a surplus of resources, a new city can be created
			citySpawnCondition = false;

			print(string.Format("WoodCount {0}, FoodCount {1}, OilCount{2}", WoodCount, FoodCount, OilCount));

			// first, consume the necessary resources that we do have
			TakeResources(Balance.ResourceTypes.Wood, Balance.CityWoodCost);
			TakeResources(Balance.ResourceTypes.Food, Balance.CityFoodCost);
			TakeResources(Balance.ResourceTypes.Oil, Balance.CityOilCost);

			// spawn the new tiles
			GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnExpansionTiles();

			StartCoroutine(CitySpawnCooldown());
		}
	}

	private IEnumerator CitySpawnCooldown() {
		yield return new WaitForSeconds(1f);
		citySpawnCondition = true;
	}

	public override void AttachPopup(GUIPopup _guiPopup) {
		standaloneNodeHealthbar.Disable();
		base.AttachPopup(_guiPopup);
	}

	public override void DetachPopup() {
		standaloneNodeHealthbar.Enable();
		base.DetachPopup();
	}
}

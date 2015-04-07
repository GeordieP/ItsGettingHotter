using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour {
	private int WoodCount, IronCount, FoodCount;
	public GameObject woodText;

    void Start() {
        this.GetComponent<GroundTileSpawner>().SpawnInitialGroundTiles();
		WoodCount = IronCount = FoodCount = 0;

        // initially set this to the amount we need to build a city, so we can spawn the first one
        WoodCount = Balance.CityWoodCost;
    }

	public void AddResource(Balance.ResourceTypes _resourceType, int count) {
		// Add the resources to their appropriate counter
		switch (_resourceType) {
			case Balance.ResourceTypes.Wood:
				WoodCount += count;
                UpdateGUI();
				break;
			case Balance.ResourceTypes.Iron:
				IronCount += count;
				break;
			case Balance.ResourceTypes.Food:
				FoodCount += count;
				break;
			default:
				break;
		}
	}

    public void UseResources(string type, int amount) {
        if (type == "Wood") {
            WoodCount -= amount;
        }
        UpdateGUI();
    }

    private void UpdateGUI() {
        woodText.GetComponent<Text>().text = "" + WoodCount;
    }

    public void CheckResourceValues() {
        if (WoodCount > 500) {
            GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnCityTile();
            GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnResourceTile();
        }
    }
}

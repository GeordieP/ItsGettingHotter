using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour {
	private int WoodCount, IronCount, FoodCount;
	public GameObject woodText;

    void Start() {
        this.GetComponent<GroundTileSpawner>().SpawnInitialGroundTiles();
		WoodCount = IronCount = FoodCount = 0;
    }

	public void AddResource(ResourcePackage.ResourceType _resourceType, int count) {
		// Add the resources to their appropriate counter
		switch (_resourceType) {
			case ResourcePackage.ResourceType.Wood:
				WoodCount += count;
				woodText.GetComponent<Text>().text = "" + WoodCount;
				break;
			case ResourcePackage.ResourceType.Iron:
				IronCount += count;
				break;
			case ResourcePackage.ResourceType.Food:
				FoodCount += count;
				break;
			default:
				break;
		}
	}
}

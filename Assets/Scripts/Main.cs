using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour {
	private int WoodCount, FoodCount, OilCount;
	public GameObject woodText;

	public GameObject GUIPopupObject;		// The single GUI element prefab for the popup GUI
	private GUIPopup guiPopupScript;			// a reference to the main script of the GUI Popup Object

	// Regarding the standalone healthbars to display above each city node
	public GameObject StandaloneHealthbarPrefab;
	public Canvas UICanvasReference;
	private Transform CityHealthbarContainer;		// holds all the city health bars we create

    void Start() {
		CityHealthbarContainer = UICanvasReference.transform.FindChild("CityHealthbarContainer");

        this.GetComponent<GroundTileSpawner>().SpawnInitialGroundTiles();
		WoodCount = FoodCount = OilCount = 0;

		if (GUIPopupObject) {
			guiPopupScript = GUIPopupObject.GetComponent<GUIPopup>();
		}

        // initially set this to the amount we need to build a city, so we can spawn the first one
        WoodCount = Balance.CityWoodCost;
    }

	public void AddResource(Balance.ResourceTypes _resourceType, int count) {
		// Add the resources to their appropriate counter
		switch (_resourceType) {
			case Balance.ResourceTypes.Wood:
				WoodCount += count;
				break;
			case Balance.ResourceTypes.Food:
				FoodCount += count;
				break;
			case Balance.ResourceTypes.Oil:
				OilCount += count;
				break;
			default:
				break;
		}
	}

	// Keep this in main for easy access from any script, and reuse the same GUI element since there should only ever be one shown at a time
	public void EnableNodePopupGUI(Node _target) {
		guiPopupScript.SetTarget(_target);
	}

	public void DisableNodePopupGUI() {
		guiPopupScript.DetatchFromTarget();
		guiPopupScript.Disable();
	}

	public StandaloneNodeHealthbar NewCityHealthBar(Node _target) {
		GameObject newHealthbar = Instantiate(StandaloneHealthbarPrefab) as GameObject;
		newHealthbar.GetComponent<StandaloneNodeHealthbar>().SetTarget(_target);
		newHealthbar.transform.SetParent(CityHealthbarContainer);
		return newHealthbar.GetComponent<StandaloneNodeHealthbar>();
	}

    public void UseResources(string type, int amount) {
        if (type == "Wood") {
            WoodCount -= amount;
        }
    }

    public void CheckResourceValues() {
        if (WoodCount > 500) {
            GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnCityTile();
            GameObject.Find("MAIN").GetComponent<GroundTileSpawner>().SpawnResourceTile();
        }
    }
}

using UnityEngine;
using System.Collections;

public abstract class Node : MonoBehaviour {
	protected bool selected = false;
	protected int WoodCount, FoodCount, OilCount;
	protected string NodeName = "GenericNode";

	private GUIPopup guiPopup;

	public abstract UnitTask GetTask();

	public void ToggleSelected() {
		selected = !selected;
	}

	public void TakeResources(Balance.ResourceTypes _type, int _count) {
		switch (_type) {
			case Balance.ResourceTypes.Wood:
				WoodCount -= _count;
				break;
			case Balance.ResourceTypes.Food:
				FoodCount -= _count;
				break;
			case Balance.ResourceTypes.Oil:
				OilCount -= _count;
				break;
			default:
				break;
		}

		if (guiPopup) {
			guiPopup.UpdateResourceText(WoodCount, FoodCount, OilCount);
		}
	}

	public virtual void AcceptResources(ResourcePackage _resourcePackage) {
		if (guiPopup) {
			guiPopup.UpdateResourceText(WoodCount, FoodCount, OilCount);
		}
	}

	public void AttachPopup(GUIPopup _guiPopup) {
		guiPopup = _guiPopup;
		guiPopup.UpdateText(NodeName, WoodCount, FoodCount, OilCount);
	}

	public void DetachPopup() {
		guiPopup = null;
	}
}
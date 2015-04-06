using UnityEngine;
using System.Collections;

public abstract class Node : MonoBehaviour {
	protected bool selected = false;
	protected int WoodCount, IronCount, FoodCount;

	public abstract UnitTask GetTask();
	public abstract void AcceptResources(ResourcePackage _resourcePackage);

	public void ToggleSelected() {
		selected = !selected;
	}

	public void TakeResources(Balance.ResourceTypes _type, int _count) {
		switch (_type) {
			case Balance.ResourceTypes.Wood:
				WoodCount -= _count;
				break;
			case Balance.ResourceTypes.Iron:
				IronCount -= _count;
				break;
			case Balance.ResourceTypes.Food:
				FoodCount -= _count;
				break;
			default:
				break;
		}
	}
}
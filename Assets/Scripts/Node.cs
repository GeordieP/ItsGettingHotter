using UnityEngine;
using System.Collections;

public abstract class Node : MonoBehaviour {
	protected bool selected = false;
	protected int WoodCount, FoodCount, OilCount;
	protected string NodeName = "GenericNode";
	protected float maxHealth = 1f;
	protected float health = 1f;

	private GUIPopup guiPopup;

	private Transform guiSpawnLocation;

	public Transform GUISpawnLocation {
		get { return guiSpawnLocation; }
	}

	protected virtual void Init() {
		// for now set the max health to double the start count of each resource, and current to half of max
		maxHealth = (WoodCount + OilCount + FoodCount) * 2;
		health = maxHealth / 2;

		guiSpawnLocation = transform.FindChild("GUISpawnLocation");
	}

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
		health = WoodCount + OilCount + FoodCount;
		if (health <= 0) {
			Destroy(gameObject);
			DetachPopup();
		}

		UpdateAttachedGUI();
	}

	public virtual void AcceptResources(ResourcePackage _resourcePackage) {
		health = WoodCount + OilCount + FoodCount;

		UpdateAttachedGUI();
	}

	public virtual void AttachPopup(GUIPopup _guiPopup) {
		guiPopup = _guiPopup;
		UpdateAttachedGUI();
	}

	protected virtual void UpdateAttachedGUI() {
		if (guiPopup) {
			guiPopup.UpdateText(NodeName, WoodCount, FoodCount, OilCount);
			guiPopup.UpdateHealthBar(health / maxHealth);
		}
	}

	public virtual void DetachPopup() {
		if (guiPopup != null) {
			guiPopup.Disable();
			guiPopup = null;
		}
	}
}
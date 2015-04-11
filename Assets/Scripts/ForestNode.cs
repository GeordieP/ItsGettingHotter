using UnityEngine;
using System.Collections;

public class ForestNode : Node {
	void Start() {
		NodeName = "Forest";
		// Forest starts with max wood count, and zero of other resources
		WoodCount = Balance.WoodResourceCount;
		FoodCount = 0;
		OilCount = 0;
	}

	public override UnitTask GetTask() {
		return new GatherTask(Balance.ResourceTypes.Wood);
	}

	public override void AcceptResources(ResourcePackage _resourcePackage) {
		throw new System.NotImplementedException();
	}
}

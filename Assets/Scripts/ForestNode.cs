using UnityEngine;
using System.Collections;

public class ForestNode : Node {
	void Start() {
		// Forest starts with max wood count, and zero of other resources
		WoodCount = Balance.WoodResourceCount;
		IronCount = 0;
		FoodCount = 0;
	}

	public override UnitTask GetTask() {
		return new GatherTask(Balance.ResourceTypes.Wood);
	}

	public override void AcceptResources(ResourcePackage _resourcePackage) {
		throw new System.NotImplementedException();
	}
}

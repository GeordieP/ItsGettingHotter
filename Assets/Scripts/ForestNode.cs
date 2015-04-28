using UnityEngine;
using System.Collections;

public class ForestNode : Node {
	void Start() {
		// Do everything in init so we can call init on base as well
		Init();
	}

	protected override void Init() {
		NodeName = "Forest";
		// Forest starts with max wood count, and zero of other resources
		WoodCount = Balance.ForestStartWoodCount;
		FoodCount = 0;
		OilCount = 0;

		base.Init();
	}

	public override UnitTask GetTask() {
		return new GatherTask(Balance.ResourceTypes.Wood);
	}

	public override void AcceptResources(ResourcePackage _resourcePackage) {
		throw new System.NotImplementedException();
	}
}

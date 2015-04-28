using UnityEngine;
using System.Collections;

public class FarmNode : Node {

	void Start() {
		// Do everything in init so we can call init on base as well
		Init();
	}
	
	protected override void Init() {
		NodeName = "Farm";
		// Forest starts with max wood count, and zero of other resources
		WoodCount = 0;
		FoodCount = Balance.FarmStartFoodCount;
		OilCount = 0;
		
		base.Init();
	}
	
	public override UnitTask GetTask() {
		return new GatherTask(Balance.ResourceTypes.Food);
	}
	
	public override void AcceptResources(ResourcePackage _resourcePackage) {
		throw new System.NotImplementedException();
	}
}

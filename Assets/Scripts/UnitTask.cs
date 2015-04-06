using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class UnitTask : MonoBehaviour {
	public float taskTime;										// How long (in seconds) the task takes to complete - should be set based on values in Balance class
    protected ResourcePackage resourcePackage;				// The resource package that gets moved through the task, in one direction or another

	public abstract void TaskCompleted(Unit unit, Node node);

	private IEnumerator WaitForTaskTime() {
		yield return new WaitForSeconds(taskTime);
	}
}

public class GatherTask : UnitTask {
    public enum GatherType { Wood, Iron, Food }
    public GatherType gatherType;

    public GatherTask() {
        gatherType = GatherType.Wood;
		taskTime = Balance.WoodTaskTime;
		resourcePackage = new ResourcePackage(ResourcePackage.ResourceType.Wood, Balance.WoodResourceCount);
    }

    public GatherTask(GatherType _gatherType) {
        gatherType = _gatherType;

		switch (gatherType) {
			case GatherType.Wood:
				taskTime = Balance.WoodTaskTime;
				resourcePackage = new ResourcePackage(ResourcePackage.ResourceType.Wood, Balance.WoodResourceCount);
				break;
			case GatherType.Iron:
				taskTime = Balance.FoodTaskTime;
				resourcePackage = new ResourcePackage(ResourcePackage.ResourceType.Iron, Balance.IronResourceCount);
				break;
			case GatherType.Food:
				taskTime = Balance.FoodTaskTime;
				resourcePackage = new ResourcePackage(ResourcePackage.ResourceType.Food, Balance.FoodResourceCount);
				break;
			default:
				break;
		}
    }

    public override void TaskCompleted(Unit unit, Node node) {
		node.TakeResources(resourcePackage.ResourceCount);
		unit.AcceptResourcePackage(resourcePackage);
    }
}

public class DepositTask : UnitTask {
	public DepositTask() {
		taskTime = Balance.WoodTaskTime;
	}

	public override void TaskCompleted(Unit unit, Node node) {
		resourcePackage = unit.TakeResourcePackage();
		if (resourcePackage.ResourceCount > 0)		// only pass the package to the home node if there's something in it
			node.transform.GetComponent<HomeNode>().AcceptResources(resourcePackage);			// TODO: change this when Node and HomeNode are set up a bit better!
	}
}
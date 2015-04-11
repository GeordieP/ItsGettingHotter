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
	Balance.ResourceTypes resourceType;

    public GatherTask(Balance.ResourceTypes _resourceType) {
		resourceType = _resourceType;

		switch (_resourceType) {
			case Balance.ResourceTypes.Wood:
				taskTime = Balance.WoodTaskTime;
				resourcePackage = new ResourcePackage(Balance.ResourceTypes.Wood, Balance.WoodResourceCount);
				break;
			case Balance.ResourceTypes.Food:
				taskTime = Balance.FoodTaskTime;
				resourcePackage = new ResourcePackage(Balance.ResourceTypes.Food, Balance.FoodResourceCount);
				break;
			case Balance.ResourceTypes.Oil:
				taskTime = Balance.FoodTaskTime;
				resourcePackage = new ResourcePackage(Balance.ResourceTypes.Oil, Balance.OilResourceCount);
				break;
			default:
				break;
		}
    }

    public override void TaskCompleted(Unit unit, Node node) {
		node.TakeResources(resourceType, resourcePackage.ResourceCount);
		unit.AcceptResourcePackage(resourcePackage);
    }
}

public class DepositTask : UnitTask {
	public DepositTask() {
		taskTime = Balance.WoodTaskTime;
	}

	public override void TaskCompleted(Unit unit, Node node) {
		resourcePackage = unit.TakeResourcePackage();
		if (resourcePackage.ResourceCount > 0)		// only pass the package to the node if there's something in it
			node.transform.GetComponent<Node>().AcceptResources(resourcePackage);
	}
}
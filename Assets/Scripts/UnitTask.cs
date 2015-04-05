using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class UnitTask : MonoBehaviour {
	public float taskTime;										// How long (in seconds) the task takes to complete - should be set based on values in Balance class
    protected ResourcePackage resourcePackage;				// The resource package that this task will yield

    public virtual void ExecuteTask(Unit unit) {					// Need unit here just so child classes can use it
		StartCoroutine(WaitForTaskTime());
	}
	public abstract void TaskCompleted(Unit unit, Node node);
	//public abstract ResourcePackage TaskCompleted(Unit unit);

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

	public override void ExecuteTask(Unit unit) {
		base.ExecuteTask(unit);
		//TaskCompleted(unit, node);
	}

    public override void TaskCompleted(Unit unit, Node node) {
		node.TakeResources(resourcePackage.ResourceCount);
		unit.AcceptResourcePackage(resourcePackage);
		//return resourcePackage;
    }
}

public class DepositTask : UnitTask {
	public DepositTask() {
		taskTime = Balance.WoodTaskTime;
		//resourcePackage = new ResourcePackage(ResourcePackage.ResourceType.Wood, Balance.WoodResourceCount);
	}

	public override void ExecuteTask(Unit unit) {
		base.ExecuteTask(unit);
		//TaskCompleted(unit, node);
	}

	public override void TaskCompleted(Unit unit, Node node) {
		resourcePackage = unit.TakeResourcePackage();
		
		// TODO: change this when Node and HomeNode are set up a bit better!
		node.transform.GetComponent<HomeNode>().AcceptResources(resourcePackage);

		//node.TakeResources(resourcePackage.ResourceCount);
		//unit.RecieveResourcePackage(resourcePackage);
		//return resourcePackage;
	}
}

// saving for later, once one task type is built and we can see what's needed
//public class BuildTask : UnitTask {
//    public enum BuildType { City, Forest }
//    public BuildType buildType;

//    public BuildTask() {
//        buildType = BuildType.City;
//    }

//    public BuildTask(BuildType _buildType) {
//        buildType = _buildType;
//    }

//    public override void ExecuteTask() {
//        switch (buildType) {
//            case BuildType.City:
//                break;
//            case BuildType.Forest:
//                break;
//            default:
//                break;
//        }
//    }

//    public override void TaskCompleted() {
//        switch (buildType) {
//            case BuildType.City:
//                break;
//            case BuildType.Forest:
//                break;
//            default:
//                break;
//        }
//    }
//}


/*

TASKS TO ADD:
 * Deposit, for giving resource back to a city

*/
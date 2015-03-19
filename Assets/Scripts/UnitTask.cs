using UnityEngine;
using System.Collections;

public abstract class UnitTask {
	public float taskTime;										// How long (in seconds) the task takes to complete - should be set based on values in Balance class
    protected ResourcePackage resourcePackage;				// The resource package that this task will yield

	//public abstract void ExecuteTask();
    public abstract ResourcePackage TaskCompleted();
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

    public override ResourcePackage TaskCompleted() {
		return resourcePackage;
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
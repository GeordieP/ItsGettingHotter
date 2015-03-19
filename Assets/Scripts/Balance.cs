using UnityEngine;

public static class Balance {
    // Amount of resources the respective node offers
    public const int WoodResourceCount = 50;                  // Wood from a forest
    public const int IronResourceCount = 50;                    // Iron from a mine
    public const int FoodResourceCount = 100;                   // Food from a farm

	// Task times (seconds)
	public const float WoodTaskTime = 2.0f;
	public const float IronTaskTime = 10.0f;
	public const float FoodTaskTime = 7.0f;
}

/*

TO ADD
 * "Full" resource amounts for when nodes are new/full of their resource
	* for example, if a full forest node has 200 wood, and a unit gathers 50 each time, only 4 units will be able to use it before it's depleted.
 * City "happy" values
	* The value for a certain resource type at which point the city has what it needs for now.
		* Going past this will be considered a surplus, and a surplus of enough stuff will trigger a new city to be built
*/
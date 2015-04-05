using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
    private bool selected = false;

    void Start() {}

	public void ToggleSelected() {
		selected = !selected;
	}

	public UnitTask GetTask() {
		// TODO: this is where we really need to know the type of node -- probably decided based on the current transform's name (ugh) and stored in a local enum called NodeType or something

		// TODO: change this when Node and HomeNode are set up a bit better!
		if (gameObject.name.Contains("MiniCity")) {
			// we're a city 
			return new DepositTask();
		} else if (gameObject.name.Contains("Forest")) {
			// we're a forest
			return new GatherTask(GatherTask.GatherType.Wood);		// for now only gather because we don't know what type nodes are yet
		}
		return new GatherTask(GatherTask.GatherType.Wood);		// for now only gather because we don't know what type nodes are yet
	}

	// should only ever get called on nodes that have resources, from GatherTask.TaskCompleted()
	public void TakeResources(int amount) {
		// TODO: Nodes need to keep track of their own resource count!
		//print(string.Format("A unit has taken {0} resources", amount));
	}
}
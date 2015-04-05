using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public UnitTask task;                  // the task units will carry out at this node

    private bool selected = false;

    void Start() {
		// TODO: For now, all nodes are initialized as a forest; in the future, decide type when spawning node with specific type
        task = new GatherTask(GatherTask.GatherType.Wood);
    }

	public void ToggleSelected() {
		selected = !selected;
	}

	//public IEnumerator ExecuteTask(Unit unit) {
	//	unit.TaskCompleted();
	//}

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


	//public void ExecuteTask(Unit unit) {
	//	//task.ExecuteTask(unit);
	//	StartCoroutine(ExecuteAfterTaskTime());
	//}

	//public ResourcePackage TaskCompleted() {
	//	selected = false;
	//	this.transform.gameObject.SetActive(false);

	//	// UnitTask.TaskCompleted will return the correct resource package for this type of node
	//	return task.TaskCompleted();
	//}


	// should only ever get called on nodes that have resources, from GatherTask.TaskCompleted()
	public void TakeResources(int amount) {
		print(string.Format("A unit has taken {0} resources", amount));
	}

    public void TaskCompleted(bool isHomeNode) {
        selected = false;
    }
}
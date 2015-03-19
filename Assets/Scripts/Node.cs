using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public float taskTime = 0.5f;       // seconds
    public UnitTask task;                  // the task units will carry out at this node

    private bool selected = false;

    void Start() {
		// TODO: For now, all nodes are initialized as a forest; in the future, decide type when spawning node with specific type
        task = new GatherTask(GatherTask.GatherType.Wood);
    }

	public void ToggleSelected() {
		selected = !selected;
	}

    public ResourcePackage TaskCompleted() {
        selected = false;
        this.transform.gameObject.SetActive(false);

		// UnitTask.TaskCompleted will return the correct resource package for this type of node
		return task.TaskCompleted();
    }

    public void TaskCompleted(bool isHomeNode) {
        selected = false;
		//if (!isHomeNode) this.transform.gameObject.SetActive(false);
    }
}

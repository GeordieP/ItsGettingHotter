/*
 * Created by Geordie Powers
 * Feb 5 2015
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Unit : MonoBehaviour {

    private enum States {
        Idle,
        Walking,
        Working
    }

    private States state = States.Idle;

    public GameObject planet;

    private Transform currentTarget;

    private List<Transform> targetNodes;
    private float currentTaskTime = 0.0f;
    private float speed = 10.3f;
    private Vector3 cross = Vector3.zero;       // TODO: rename to something like axis?
    private bool foundTarget = false;

    private Vector3 temploc = new Vector3(4.67f, 6.37f, -5.84f);

	// Use this for initialization
	void Start () {
        targetNodes = new List<Transform>();
	}

	// Update is called once per frame
	void Update () {
        //print("state: " + foundTarget);
        switch (state) {
            case States.Idle:
                this.renderer.material.color = Color.white;
                break;
            case States.Walking:
                if (!foundTarget) {
                    this.renderer.material.color = Color.blue;
                    transform.RotateAround(planet.transform.position, cross, speed * Time.deltaTime);
                } else {
                    currentTaskTime = currentTarget.GetComponent<Node>().taskTime;      // time it takes to do a task is determined by the node itself for now - later it will also be influenced by the properties of the unit (movespeed taskspeed etc)
                    StartCoroutine(ExecuteTask());
                    state = States.Working;
                }
                break;
            case States.Working:
                break;
            default:
                break;
        }
	}

    IEnumerator ExecuteTask() {
        this.renderer.material.color = Color.red;
        yield return new WaitForSeconds(currentTaskTime);
        // task is completed, move on
        currentTarget.GetComponent<Node>().TaskCompleted();
        NextTarget();
    }

    private void NextTarget() {
        state = States.Idle;
        targetNodes.RemoveAt(0);
        foundTarget = false;

        if (targetNodes.Count > 0) {
            SetTarget(targetNodes[0]);
            state = States.Walking;
        } else {
            // no more nodes left in the list
            state = States.Idle;
            // return to home node?
        }
    }

    public void AddTarget(Transform _target) {
        targetNodes.Add(_target);

        if (state == States.Idle) {
            SetTarget(targetNodes[0]);
            state = States.Walking;
        }
    }

    public void SetTarget(Transform _target) {
        currentTarget = _target;
        cross = Vector3.Cross(this.transform.position, currentTarget.position);

        if (state == States.Idle) {
            state = States.Walking;
        }
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.gameObject.transform == currentTarget) {
            // we've made it to the target
            foundTarget = true;
            print("we at the combination pizza hut and taco bell");
        }
    }
}

/*
 * Created by Geordie Powers
 * Feb 5 2015
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Unit : MonoBehaviour {

    // States
    private States state = States.Idle;
    private enum States {
        Idle,
        Walking,
        Working
    }

    // Planet
    public GameObject planet;
    
    // Nodes
    private Transform currentTarget;
    private List<Transform> targetNodes;
    private bool foundTarget = false;

    // Unit
    Color idleColor = Color.white;

    // Unit Movement
    private Vector3 moveAxis = Vector3.zero;
    private float speed = 10.3f;
    private float currentTaskTime = 0.0f;

	// Use this for initialization
	void Start () {
        targetNodes = new List<Transform>();
	}

	// Update is called once per frame
	void Update () {
        switch (state) {
            case States.Idle:
                this.renderer.material.color = idleColor;
                break;
            case States.Walking:
                if (!foundTarget) {
                    this.renderer.material.color = Color.blue;
                    transform.RotateAround(planet.transform.position, moveAxis, speed * Time.deltaTime);
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

    public void SetColor() {
        idleColor = Color.blue;
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
        moveAxis = Vector3.Cross(this.transform.position, currentTarget.position);

        if (state == States.Idle) {
            state = States.Walking;
        }
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.gameObject.transform == currentTarget) {
            // we've made it to the target
            foundTarget = true;
        }
    }
}

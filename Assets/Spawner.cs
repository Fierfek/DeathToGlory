using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class Spawner : MonoBehaviour {

	SphereCollider sphereCollider;
	Queue<Enemy> spawnQueue;
	public Enemy test;

	//Is the area clear to spawn;
	int clear = 0;

	// Use this for initialization
	void Start () {
		sphereCollider = GetComponent<SphereCollider>();
		spawnQueue = new Queue<Enemy>();

		//Test only
		if(test != null) {
			spawnQueue.Enqueue(test);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnQueue.Count > 0) {
			if (clear == 0) {
				spawnQueue.Dequeue().gameObject.transform.position = transform.position;
			}
		}
	}

	//The enemy should already be build at this point.
	public void SpawnEnemy(Enemy enemy) {
		spawnQueue.Enqueue(enemy);
	}

	private void OnCollisionEnter(Collision collision) {
		clear++;
	}

	private void OnCollisionExit(Collision collision) {
		clear--;
	}
}

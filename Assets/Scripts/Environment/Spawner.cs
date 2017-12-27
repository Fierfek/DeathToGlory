using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Queue<Enemy> enemyPool = new Queue<Enemy>();
	private List<Enemy> actives = new List<Enemy>();
	public Enemy enemyPrefab;
	public GameObject holdingPoint;
	private Enemy setActive;

	int poolSize = 10; // Adjust for how many enemies we need at one time.
	float despawnTimer = 0;
	float despawnTime = 10;

	void Start() {
		//Create base enemy from prefab and load into enemyPool

		for (int i = 0; i < poolSize; i++) {
			Enemy newEnemy;
			newEnemy = Instantiate(enemyPrefab, holdingPoint.transform.position, holdingPoint.transform.rotation);
			newEnemy.gameObject.SetActive(false);
			enemyPool.Enqueue(newEnemy);
		}
	}

	//This allows us to attach the type of enemy script we want, then spawn it at the desired spawn point. Also returns if the process was successful
	public bool Spawn(Enemy enemy, SpawnPoint spawnPoint) {
		if(enemyPool.Count > 0) {
			setActive = enemyPool.Dequeue();
			setActive.gameObject.SetActive(true);
			spawnPoint.SpawnEnemy(setActive);

			actives.Add(setActive);

			return true;
		} else {
			return false;
		}
	}

	//Look for inactive enemies to pull back into the queue. Might want to make this time based.
	//Might come up with a better system for this.
	void Update() {
		despawnTimer -= Time.deltaTime;

		if(despawnTimer <= 0) {
			despawnTimer = despawnTime;

			for(int i = 0; i < actives.Count - 1; i++) {
				if(!actives[i].gameObject.activeSelf) {
					enemyPool.Enqueue(actives[i]);
					actives.RemoveAt(i);
				}
			}
		}
	}
}

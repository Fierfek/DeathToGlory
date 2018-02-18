using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour {

	public Ledge left, right;
	private Transform[] children;
	private List<Transform> nodes;

	void Start() {
		children = GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		foreach(Transform m in children) {
			if(m.name.Equals("Node")) {
				nodes.Add(m);
			}
		}
	}

	public Ledge getLeft() {
		return left;
	}
	
	public Ledge getRight() {
		return right;
	}

	public Transform[] getNodes() {
		return nodes.ToArray();
	}

	public BoxCollider getCollider() {
		return GetComponent<BoxCollider>();
	}
}

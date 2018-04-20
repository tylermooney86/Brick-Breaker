using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBallScript : MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void Drop() {
		rb.AddForce(0, -150f, 0);
	}
	public void Die() {
		//Debug.Log("Losing Life");
		Destroy(gameObject);
	}
}

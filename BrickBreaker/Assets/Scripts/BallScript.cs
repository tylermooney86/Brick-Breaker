using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour {
	//public Rigidbody rb;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void Die() {
		Destroy(gameObject);
	}
	public void DieWithRespawn() {
		Destroy(gameObject);
		GameManagerScript gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		gameManagerScript.SpawnBall();
		gameManagerScript.loseLife();
	}
}

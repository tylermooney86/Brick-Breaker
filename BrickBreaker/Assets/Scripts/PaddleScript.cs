using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour {

	public float paddleSpeed = 15;
	public float englishForce = 200;
	float gameBoardWidth = 4.25f;

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		// Move paddle
		transform.Translate(paddleSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);
		// Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.Translate(Input.GetAxis("Mouse X") * Time.deltaTime * paddleSpeed, 0, 0);
		// Check for edges of gameboard
		if (transform.position.x >= gameBoardWidth) {
			transform.position = new Vector3(gameBoardWidth, transform.position.y, transform.position.z);
		}
		if (transform.position.x <= -gameBoardWidth) {
			transform.position = new Vector3(-gameBoardWidth, transform.position.y, transform.position.z);
		}
	}

	// Add english to ball
	void OnCollisionEnter(Collision col) {
		foreach (ContactPoint contact in col.contacts) {
			if (contact.thisCollider == GetComponent<Collider>()) {
				float english = contact.point.x - transform.position.x;
				contact.otherCollider.GetComponent<Rigidbody>().AddForce(englishForce * english, 0, 0);
			}
		}
	}

	public void Die() {
		Destroy(gameObject);
	}


}

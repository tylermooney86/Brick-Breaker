using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour {

	void OnTriggerEnter(Collider otherCollider) {
		//Destroy Ball
		if(otherCollider.gameObject.name == "Ball(Clone)") {
			BallScript ballScript = otherCollider.GetComponent<BallScript>();
			ballScript.DieWithRespawn();
		}
		// Destroy Life
		if(otherCollider.gameObject.name == "LifeSphere(Clone)") {
			LifeBallScript lifeBallScript = otherCollider.GetComponent<LifeBallScript>();
			lifeBallScript.Die();
		}
		// Destroy exploded cube
		if(otherCollider.gameObject.name == "Cube(Clone)") {
			CubeScript cubeScript = otherCollider.GetComponent<CubeScript>();
			cubeScript.Die();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCreatorScript : MonoBehaviour {
	public GameObject brickPreFab;
	int numBricks = 0;
	Vector3 position = new Vector3(0,0,-0.5f);

	Vector3[] positionVectors1 = new Vector3[] { new Vector3(2f,0,0), new Vector3(0,0,0), new Vector3(-2f,0,0),
	 																						new Vector3(2f,3f,0), new Vector3(0,3f,0), new Vector3(-2f,3f,0) };

	Vector3[] positionVectors2 = new Vector3[] { new Vector3(2f,0,0), new Vector3(-2f,0,0),
	 																						new Vector3(2f,3f,0), new Vector3(-2f,3f,0) };

	Vector3[] positionVectors3 = new Vector3[] { new Vector3(2f,0,0), new Vector3(-2f,1.5f,0), new Vector3(-2f,0,0),
	 																						new Vector3(2f,3f,0), new Vector3(2f,1.5f,0), new Vector3(-2f,3f,0) };

	Vector3[] positionVectors4 = new Vector3[] { new Vector3(2f,0,0), new Vector3(0,1.5f,0), new Vector3(-2f,0,0),
	 																						new Vector3(2f,3f,0), new Vector3(-2f,3f,0) };

	Vector3[] positionVectors5 = new Vector3[] { new Vector3(4f,0,0), new Vector3(0,0,0), new Vector3(-4f,0,0),
																								new Vector3(2f,3f,0), new Vector3(-2f,3f,0) };

	Vector3[] positionVectors6 = new Vector3[] { new Vector3(4f,3f,0), new Vector3(-4f,3f,0),
																								new Vector3(2f,0,0), new Vector3(0,3f,0), new Vector3(-2f,0,0) };

	Vector3[] positionVectors7 = new Vector3[] { new Vector3(4f,3f,0), new Vector3(2f,3f,0), new Vector3(0,3f,0), new Vector3(-2f,3f,0), new Vector3(-4f,3f,0),
																								new Vector3(4f,0,0), new Vector3(2f,0,0), new Vector3(0,0,0), new Vector3(-2f,0,0) , new Vector3(-4f,0,0) };

	Vector3[] positionVectors8 = new Vector3[] { new Vector3(4f,3f,0), new Vector3(2f,3f,0), new Vector3(-2f,3f,0), new Vector3(-4f,3f,0),
																								new Vector3(4f,0,0), new Vector3(2f,0,0), new Vector3(-2f,0,0) , new Vector3(-4f,0,0) };

	Vector3[] positionVectors9 = new Vector3[] { new Vector3(4f,3f,0), new Vector3(0,3f,0), new Vector3(-4f,3f,0),
																								new Vector3(4f,0,0), new Vector3(0,0,0), new Vector3(-4f,0,0) };

	Vector3[] positionVectors10 = new Vector3[] { new Vector3(4f,1.5f,0),  new Vector3(2f,3f,0), new Vector3(-2f,3f,0), new Vector3(-4f,1.5f,0),
																							new Vector3(2f,0,0), new Vector3(-2f,0,0) };

	public void CreateBricks(int hp) {
		int layout = Random.Range(0,10);
		Vector3[] positionVectors = new Vector3[8];
		if (layout == 0) {
			positionVectors = positionVectors1;
		} else if (layout == 1) {
			positionVectors = positionVectors2;
		} else if (layout == 2) {
			positionVectors = positionVectors3;
		} else if (layout == 3) {
			positionVectors = positionVectors4;
		} else if (layout == 4) {
			positionVectors = positionVectors5;
		} else if (layout == 5) {
			positionVectors = positionVectors6;
		} else if (layout == 6) {
			positionVectors = positionVectors7;
		} else if (layout == 7) {
			positionVectors = positionVectors8;
		} else if (layout == 8) {
			positionVectors = positionVectors9;
		} else {
			positionVectors = positionVectors10;
		}
		foreach (Vector3 v in positionVectors) {
			GameObject brick = Instantiate(brickPreFab, position + v, Quaternion.identity);
			BrickScript brickScript = brick.GetComponent<BrickScript>();
			brickScript.SetMaxHP(hp);
			numBricks++;
		}
	}
}

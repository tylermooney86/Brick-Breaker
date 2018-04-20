using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickScript : MonoBehaviour {

	public GameObject cubePreFab;

	public int points = 100;
	static int numBricks = 0;
	static int maxHP = 1;
	int hitPoints = 1;


	public float cubeSize = 0.2f;
	public int cubesInRow = 5;

	float cubesPivotDistance;
	Vector3 cubesPivot;

	public float explosionForce = 50f;
	public float explosionRadius = 4f;
	public float explosionUpward = 0.4f;

	// Use this for initialization
	void Start () {
		//calculate pivot distance
		cubesPivotDistance = cubeSize * cubesInRow / 2;
		//use this value to create pivot vector)
		cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
		hitPoints = maxHP;
		numBricks++;
	}

	// Update is called once per frame
	void Update () {

	}
	//Check for collisions
	void OnCollisionEnter( Collision col) {
		//Debug.Log(col.gameObject.name);
		if (col.gameObject.name == "Ball(Clone)") {
			hitPoints--;
			if (hitPoints > 0) {
					ChangeColor();
					GameManagerScript gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
					gameManagerScript.UpdateScore(points * maxHP);
			}

			if (hitPoints <= 0) {
				explode(false);
				Die();
			}
		}
	}

	void Die() {
		if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().getIsGameOver() == false){
			GameObject gameStatsObject = GameObject.Find("GameStats");
			GameManagerScript gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
			gameManagerScript.UpdateScore(points * maxHP);
		}
		Destroy(gameObject);
		numBricks--;
		if (numBricks <= 0) {
			//load new level
			Debug.Log("Win!");
			GameManagerScript gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
			gameManagerScript.LoadNextLevel();
		}
	}
	void ChangeColor() {

		Material material = new Material(Shader.Find("Standard"));
		material.color = Random.ColorHSV();
    GetComponent<Renderer>().material = material;
	}
	public void SetMaxHP(int num) {
		maxHP = num;
	}
	// Explosion
	public void explode(bool gravOff) {
			//make object disappear
			gameObject.SetActive(false);

			//loop 3 times to create 2X x Y x Z pieces in x,y,z coordinates
			for (int x = 0; x < cubesInRow; x++) {
					for (int y = 0; y < cubesInRow; y++) {
							for (int z = 0; z < cubesInRow; z++) {
									createPiece(x, y, z);
							}
					}
			}

			//get explosion position
			Vector3 explosionPos = transform.position;
			//get colliders in that position and radius
			Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
			//add explosion force to all colliders in that overlap sphere
			foreach (Collider hit in colliders) {
					//get rigidbody from collider object
					Rigidbody rb = hit.GetComponent<Rigidbody>();
					if (rb != null) {
							//add explosion force to this body with given parameters
							if(gravOff) {
								rb.useGravity = false;
							}

							rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
					}
			}
			//destroy game object
			if(gravOff){
				Die();
			}

	}

	void createPiece(int x, int y, int z) {
			//create piece
			GameObject piece;
			//piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
			piece = Instantiate(cubePreFab);
			piece.GetComponent<Renderer>().material = GetComponent<Renderer>().material;

			//set piece position and scale
			piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
			piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

			//add rigidbody and set mass
			piece.AddComponent<Rigidbody>();
			piece.GetComponent<Rigidbody>().mass = cubeSize;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	int lives = 0;
	int score = 0;
	public float ballLaunchForce = 300;
	int levelCount = 1;
	bool isGameOver = false;
	bool isIntro = true;

	public Text scoreText;
	public GameObject ballPreFab;
	public GameObject lifeBallPreFab;
	public GameObject paddlePreFab;
	public Text gameOverText;
	int maxLives = 10;
	GameObject[] lifeArr = new GameObject[10];
	Vector3 lifePosition = new Vector3(3f,-5.4f,-0.3f);
	Vector3 paddleHeight = new Vector3(0,0.5f,0);
	float lifePositionAdjuster = 0.5f;
	bool mousePressed;
	Vector3 mouseStartPosition;
	Vector3 mouseEndPosition;

	GameObject attachedBall = null;
	GameObject paddle = null;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	void StartGame() {
		PopulateLives();
		CreatePaddle();
		SpawnBall();
		SpawnBricks();
		gameOverText.text = "";
	}

	// Update is called once per frame
	void Update () {

		if (isIntro) {
			gameOverText.text = "PRESS ENTER";
			if (Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)) {
				isIntro = false;
				StartGame();

			}
		}

		//launch ball
		if (isGameOver == false && isIntro == false){
			if (attachedBall){
				Rigidbody ballRB = attachedBall.GetComponent<Rigidbody>();
				ballRB.position = paddle.transform.position + paddleHeight;
				if (Input.GetButtonDown("Jump")) {
					ballRB.GetComponent<Rigidbody>().isKinematic = false;
					ballRB.GetComponent<Rigidbody>().AddForce(ballLaunchForce * Input.GetAxis("Horizontal"), ballLaunchForce, 0);
					attachedBall = null;
				}
				if (Input.GetMouseButtonDown(0)) {
					mousePressed = true;
					Debug.Log("Mouse Pressed");

					Ray vRayStart = Camera.main.ScreenPointToRay(Input.mousePosition);

					mouseStartPosition = vRayStart.origin;

				}
				if (Input.GetMouseButtonUp(0)) {
					if(mousePressed) {
						Debug.Log("Mouse released");

						Ray vRayEnd = Camera.main.ScreenPointToRay(Input.mousePosition);

		      	mouseEndPosition = vRayEnd.origin;

		      	Vector3 heading = mouseEndPosition - mouseStartPosition;
		      	float distance = heading.magnitude;
		      	Vector3 direction = heading/distance;

						ballRB.GetComponent<Rigidbody>().isKinematic = false;
						ballRB.GetComponent<Rigidbody>().AddForce(ballLaunchForce * heading.x, ballLaunchForce, 0);
						attachedBall = null;
						mousePressed = false;
					}

				}
			}
		}
		//restart game if isGameOver == true
		if (isGameOver) {
			if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)) {
				SceneManager.LoadScene("Level1");
				isGameOver = false;
			}
		}

	if (Input.GetButtonDown("Cancel")) {
		if(isIntro) {
			Application.Quit();
		} else {
			lives = 0;
			loseLife();
			SceneManager.LoadScene("Level1");
		}
	}

	}
	// Create Paddle object
	void CreatePaddle() {
		paddle = Instantiate(paddlePreFab);
	}
	// populate lives
	void PopulateLives(){
		for (int index = 0; index < 3; index++) {
			addLife();
		}
	}
	public void addLife() {
		Debug.Log("adding life");
		if (lives < maxLives) {
			if (lives < maxLives / 2) {
				GameObject life = Instantiate(lifeBallPreFab, lifePosition + new Vector3((lives)*lifePositionAdjuster,0,0), Quaternion.identity);
				lifeArr[lives] = life;
				lives++;
			} else {
				GameObject life = Instantiate(lifeBallPreFab, lifePosition + new Vector3((lives-5)*lifePositionAdjuster,-0.4f,0), Quaternion.identity);
				lifeArr[lives] = life;
				lives++;
			}
		}
	}
	public void loseLife() {
		lives--;
		if(lives >= 0) {
			LifeBallScript lifeBallScript = lifeArr[lives].GetComponent<LifeBallScript>();
			lifeBallScript.Drop();
		}
		else {
			//explode all Bricks
			isGameOver = true;
			GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
			foreach (GameObject brick in bricks) {
				BrickScript brickScript = brick.GetComponent<BrickScript>();
				//brickScript.explode(false);
				brickScript.explode(true);
			}
			Destroy(paddle);
			//display game Over
			gameOverText.text = "GAME OVER";
			//Debug.Log("Game Over");
		}
	}
	// create a ball object
	public void SpawnBall() {
		if (lives > 0){
			attachedBall = Instantiate(ballPreFab,paddle.transform.position + paddleHeight, Quaternion.identity);
		}
	}
	// Create Bricks
	public void SpawnBricks() {
		int brickHealth = Mathf.RoundToInt(levelCount/3);
		if (brickHealth < 1) {
			brickHealth = 1;
		}
		Debug.Log(brickHealth);
		BrickCreatorScript brickCreatorScript = GameObject.Find("BrickCreator").GetComponent<BrickCreatorScript>();
		brickCreatorScript.CreateBricks(brickHealth);
	}
	public void UpdateScore(int points) {
		score += points;
		scoreText.text = score.ToString("N0");
	}
	// Load level and increase difficulty
	public void LoadNextLevel() {
		if(isGameOver == false) {
			levelCount++;
			Debug.Log("Level: " + levelCount.ToString());
			SpawnBricks();
			addLife();
		}
	}
	// return isGameOver
	public bool getIsGameOver() {
		return isGameOver;
	}

}

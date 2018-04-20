using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			//restart game
			GameStatsScript gameStatsScript = GameObject.Find("GameStats").GetComponent<GameStatsScript>();
			//gameStatsScript.ResetScore();
			SceneManager.LoadScene("Level1");

		}
	}
}

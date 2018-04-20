using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsScript : MonoBehaviour {
	public Text scoreText;
	int score = 0;
	public int lifeThreshold = 5000;

	public void UpdateScore(int points) {
		score += points;
		Debug.Log(score);
		scoreText.text = score.ToString("N0");
	}
}

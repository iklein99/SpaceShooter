using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float spawnStartWait;
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private int score;
	private bool isGameOver;
	private bool restart;

	// Use this for initialization
	void Start () {
		isGameOver = false;
		restart = false;

		restartText.text = "";
		gameOverText.text = "";
		StartCoroutine(spawnWave());
		score = 0;
		displayScore();
	}

	void Update()
	{
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene("Main");
			}
		}
	}

	IEnumerator spawnWave() {
		yield return new WaitForSeconds(spawnStartWait);

		while (true) {
			for (int i = 0; i < hazardCount; i++){
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate(hazard[Random.Range(0, hazard.Length)], spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (isGameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void incrementScore(int inc) {
		score += inc;
		displayScore();
	}


	public void gameOver() {
		gameOverText.text = "Game Over";
		isGameOver = true;
	}



	private void displayScore() {
		scoreText.text = "Score: " + score.ToString();
	}

}

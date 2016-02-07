using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public int incrementScore;
	public string[] ignoreTags;

	private GameController gameController;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		gameController = gameControllerObject.GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		foreach(string tag in ignoreTags) {
			if (other.tag == tag){
				return;
			}
		}
		if (other.tag != "Boundary") {
			Destroy(other.gameObject);
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
			gameController.incrementScore(incrementScore);

			if (other.tag == "Player") {
				Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
				gameController.gameOver();
			}
		}
	}
}

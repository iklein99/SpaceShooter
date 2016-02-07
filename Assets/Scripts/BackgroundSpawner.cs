using UnityEngine;
using System.Collections;

public class BackgroundSpawner : MonoBehaviour {

	public GameObject backGround;
	public Vector3 spawnPosition;

	void OnTriggerExit(Collider other) {
		Instantiate(backGround, spawnPosition, other.transform.rotation);
		Destroy(other.gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject bolt;
	public float shootFromRelativeZ;
	public Vector2 waitToShootRange;
	public int maxBurst;
	public float burstTimeBetweenShots;

	// Use this for initialization
	void Start () {
		StartCoroutine(shootAway());
	}

	IEnumerator shootAway() {
		Vector3 boltPosition;

		while (true) {
			yield return new WaitForSeconds(Random.Range(waitToShootRange.x, waitToShootRange.y));

			// Burst shots
			for (int i = 0; i < Random.Range(1, maxBurst); i++) {
				boltPosition = new Vector3(transform.position.x, 0, transform.position.z - shootFromRelativeZ);
				GameObject theBolt = (GameObject)Instantiate(bolt, boltPosition, transform.rotation);
				Mover mover = theBolt.GetComponent<Mover>();
				mover.speed = mover.speed * -1;
				yield return new WaitForSeconds(burstTimeBetweenShots);
			}
		}
	}
	
}

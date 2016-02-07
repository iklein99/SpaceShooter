using UnityEngine;
using System.Collections;

public class EvadeController : MonoBehaviour {
	public Vector2 turnRange;
	public Vector2 evadeTimeWaitRange;
	public float clipX;
	public float roll;
	public float smooth;

	private float targetVelocityX;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		targetVelocityX = 0;
		rb = gameObject.GetComponent<Rigidbody>();
		StartCoroutine(evade());
	}

	IEnumerator evade() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(evadeTimeWaitRange.x, evadeTimeWaitRange.y));
			targetVelocityX = Random.Range(turnRange.x, turnRange.y);
			yield return new WaitForSeconds(Random.Range(evadeTimeWaitRange.x, evadeTimeWaitRange.y));
			targetVelocityX = 0;
		}
	}
	
	void FixedUpdate() {
		float newX = Mathf.Lerp(rb.velocity.x, targetVelocityX, Time.deltaTime * smooth);
		rb.velocity = new Vector3(newX, rb.velocity.y, rb.velocity.z);
		// Roll the ship based on newX as an angle
		rb.rotation = Quaternion.Euler(0, 0, newX * roll * -1);

		// Keep the enemy in the game
		newX = Mathf.Clamp(rb.position.x, -clipX, clipX);
		rb.position = new Vector3(newX, rb.position.y, rb.position.z);
	}
}

using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour {

	private Rigidbody rb; 
	private AudioSource audioSource;
	private float screenMidPoint;
	private float touchMoveHorizontal, touchMoveVertical;
	private bool touchFire;

	public Boundary bounds;
	public float speed;
	public float tilt;
	public GameObject bolt;

	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		audioSource = gameObject.GetComponent<AudioSource>();
		screenMidPoint = Screen.width;
	}

	void Update() {
		if (Input.GetButtonDown("Jump")) {
			GameObject.Instantiate(bolt, transform.position, Quaternion.identity);
			audioSource.Play();
		}

		touchMoveVertical = touchMoveVertical = 0.0f;
		touchFire = false;

		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				if (touch.position.x < Screen.width / 2) {
					// Fire area
					if (touch.phase == TouchPhase.Began) {
						touchFire = true;
					}
				}
				else {
					// Move area
					if (touch.phase == TouchPhase.Moved) {
						touchMoveHorizontal = Mathf.Clamp(touch.deltaPosition.x, -1, 1);
						touchMoveVertical =  Mathf.Clamp(touch.deltaPosition.y, -1, 1);
					}
				}
			}
		}
	}
	
	void FixedUpdate () {
		float moveHorizontal;
		float moveVertical;

		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			moveHorizontal = touchMoveHorizontal;
			moveVertical = touchMoveVertical;
		}
		else {
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}

		rb.velocity = ((Vector3.right * moveHorizontal) + (Vector3.forward * moveVertical)) * speed;
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, bounds.xMin, bounds.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, bounds.zMin, bounds.zMax)
		);

		rb.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rb.velocity.x * -tilt));

	}
}

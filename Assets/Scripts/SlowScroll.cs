using UnityEngine;
using System.Collections;

public class SlowScroll : MonoBehaviour {
	public float speed;


	// Update is called once per frame
	void Update () {
		transform.position -= Vector3.forward * speed;
	}
}

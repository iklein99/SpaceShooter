using UnityEngine;
using System.Collections;

public class DestroyTimed : MonoBehaviour {
	private float startTime;
	public float timeToLive;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - startTime) >= timeToLive) {
			Destroy(gameObject);
		}

	}
}

using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Camera cam;
	private float speed = 0.5f;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		var x = cam.transform.forward.x * Time.deltaTime * speed;
		var z = cam.transform.forward.z * Time.deltaTime * speed;

		transform.Translate (x, 0, z);
	}
}

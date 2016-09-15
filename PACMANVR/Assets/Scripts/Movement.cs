using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Camera cam;
	public float speed = 0.5f;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		var x = Vector3.Normalize(cam.transform.forward).x * Time.deltaTime * speed;
		var z = Vector3.Normalize(cam.transform.forward).z * Time.deltaTime * speed;

		transform.Translate (x, 0, z);
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public Text gameStatus;
	public Camera cam;
	public float speed = 1.0f;
	private float currentSpeed;
	private bool canRestart;

	// Use this for initialization
	void Start () {
		canRestart = false;
		gameStatus.text = "Click to Start!";
		currentSpeed = 0;
	}

	// Update is called once per frame
	void Update () {
		var x = Vector3.Normalize(cam.transform.forward).x * Time.deltaTime * currentSpeed;
		var z = Vector3.Normalize(cam.transform.forward).z * Time.deltaTime * currentSpeed;

		transform.Translate (x, 0, z);
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			GameOver();
		}
	}

	public void Play(){
		gameStatus.text = "";
		currentSpeed = speed;
	}

	void GameOver() {
		canRestart = true;
		currentSpeed = 0;
		gameStatus.text = "Game Over! \nClick to Restart.";
	}

	public bool canRestartGame(){
		return canRestart;
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject maze, player;
	public GameObject[] monsters;
	private bool magnetDetectionEnabled = true;
	private PlayerMovement playerObj;
	private bool isStarted;

	// Use this for initialization
	void Start () {
		CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
		// Disable screen dimming:
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		isStarted = false;
		Instantiate(maze);
		GameObject playerInstantiated = Instantiate(player, GameObject.FindGameObjectWithTag ("SpawnPlayer").transform.position, Quaternion.identity) as GameObject;
		if (playerInstantiated != null)
			playerObj = playerInstantiated.GetComponent<PlayerMovement>();
		else
			Debug.Log ("Cannot find Player");
	}

	// Update is called once per frame
	void Update () {
		if (!magnetDetectionEnabled) return;
		if (CardboardMagnetSensor.CheckIfWasClicked() || Input.GetMouseButtonDown(0)) {
			if (!isStarted)
				PlayGame ();
			else if (playerObj.canRestartGame())
				RestartGame ();
			CardboardMagnetSensor.ResetClick();
		}
	}

	void SpawnMonsters(){
		ArrayList usedValues = new ArrayList ();
		foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnMonster")) {
			int random = Random.Range (0, monsters.Length);
			while(usedValues.Contains(random))
				random = Random.Range (0, monsters.Length);
			usedValues.Add (random);
			Instantiate (monsters [random], spawn.transform.position, Quaternion.identity);
		}
	}

	void PlayGame(){
		isStarted = true;
		SpawnMonsters ();
		playerObj.Play();
	}

	void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
		
}

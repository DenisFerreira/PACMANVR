using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject maze, player;
	public GameObject[] monsters;

	// Use this for initialization
	void Start () {
		Instantiate(maze);
		Instantiate (player, GameObject.FindGameObjectWithTag ("SpawnPlayer").transform.position, Quaternion.identity);
		ArrayList usedValues = new ArrayList ();
			foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnMonster")) {
				int random = Random.Range (0, monsters.Length);
				while(usedValues.Contains(random))
					random = Random.Range (0, monsters.Length);
				usedValues.Add (random);
				Instantiate (monsters [random], spawn.transform.position, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

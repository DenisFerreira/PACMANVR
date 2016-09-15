using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject maze;
	public GameObject[] monsters;
	public GameObject player;
	// Use this for initialization
	void Start () {
		GameObject maze1 = Instantiate(maze) as GameObject;
		Instantiate (player, GameObject.FindGameObjectWithTag("SpawnPlayer").transform.position, Quaternion.identity);
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

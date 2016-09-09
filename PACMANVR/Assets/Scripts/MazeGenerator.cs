using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

	public GameObject cube;
	private int[,] matrix = new int[101,101];  

	// Use this for initialization
	void Start () {
		initMatrix ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void initMatrix(){
		// Init Matrix with Closed Borders 
		for (int j = 0; j < 101; j++) {
			matrix[0,j] = 1;
			matrix[100,j] = 1;
		}

		for (int j = 0; j < 101; j++) {
			matrix[j,0] = 1;
			matrix[j,100] = 1;
		}

		for (int j = 1; j < 100; j++) {
			for (int i = 1; i < 100; i++) {
				matrix[i,j] = 0;
			}
		}

		buildWall ();	
	}

	private void buildWall(){
		int i, j;
		for (int x = 0; x <= 100; x++)
		{
			for (int z = 0; z <= 100; z++)
			{
				if (matrix[x,z] == 1)
					Instantiate(cube, new Vector3(x-50, 0.5f, z-50), Quaternion.identity);
			}
		}
	}
}

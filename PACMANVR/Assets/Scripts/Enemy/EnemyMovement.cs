using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float wanderRadius;
	private float timer, wanderTimer = 1;
	private Transform playerTransform;
	private NavMeshAgent agent;
	private AudioSource audio;
	private GameController gameController;

    void Awake ()
    {
		timer = wanderTimer;
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
        agent = GetComponent <NavMeshAgent> ();
		audio = GetComponent<AudioSource> ();
		InvokeRepeating ("playAudio", 1, Random.Range(5f, 30f));
    }

	void playAudio() {
		audio.Play();
	}

    void Update ()
    {
		timer += Time.deltaTime;
		if (timer >= wanderTimer) {
			if (Vector3.Distance (playerTransform.position, transform.position) > 5) {
				Vector3 newPos = RandomNavSphere (playerTransform.position, wanderRadius);
				agent.SetDestination (newPos);
				timer = 0;
			}
			else
				agent.SetDestination (playerTransform.position);
		}
    }

	public static Vector3 RandomNavSphere(Vector3 origin, float dist) {
		Vector3 randDirection = Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randDirection, out navHit, dist, NavMesh.AllAreas);
		return navHit.position;
	}
}

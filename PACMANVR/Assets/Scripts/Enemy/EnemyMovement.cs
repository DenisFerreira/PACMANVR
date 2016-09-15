using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float wanderRadius;
	private float timer, wanderTimer = 1;
	private Transform player;
	private NavMeshAgent agent;
	private AudioSource audio;

    void Awake ()
    {
		timer = wanderTimer;
        player = GameObject.FindGameObjectWithTag ("Player").transform;
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
			if (Vector3.Distance (player.position, transform.position) > 5) {
				Vector3 newPos = RandomNavSphere (player.position, wanderRadius);
				agent.SetDestination (newPos);
				timer = 0;
			}
			else
				agent.SetDestination (player.position);
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

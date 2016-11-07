using UnityEngine;
using System.Collections;

public class AnimateChest : MonoBehaviour {
	private Animator anim;

	// Update is called once per frame
	public void OpenChest () {
		anim.SetTrigger ("InteractChest");
	}

	void Awake() {
		anim = GetComponent<Animator> ();}
	}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloInteractable : InteractableBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnInteract() {
		Debug.Log("Hello, interact!");
	}
}

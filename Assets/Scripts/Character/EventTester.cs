using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		EventManager.TriggerEvent("SugarLevelChanged", new Hashtable() { { "SugarLevel", -0.5f } });
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Positionable p = new Positionable(new Vector2(1,1),null);
		Actor a = new Actor (new Vector2(50,50), null);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

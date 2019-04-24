using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour {

	List<Positionable> entities;


	// Use this for initialization
	void Start () {
        this.entities = new LevelParser(null);
        
        this.entities.Add(new Player(new Vector2(0, 0)));

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(this.entities != null){
			foreach( Positionable a in this.entities){
				a.update();

			}
		}
	}
}

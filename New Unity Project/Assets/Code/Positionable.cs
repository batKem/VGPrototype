using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positionable  {


	private Sprite sprite;
	private GameObject gameObject;

    //currently unused
	private Vector2 position;

	public Positionable(Vector2 position, Sprite sprite){
		// /!\ this call instantiates a new object in the game world
		this.gameObject = new GameObject(this.GetType().ToString());
		
		this.gameObject.AddComponent<SpriteRenderer>();

		this.sprite = sprite;
		this.position = position;
		updateGraphics();

	}
	virtual public void update(){
		this.updateGraphics();
	}

    //Updates graphic rendering of the positionable (will be used for sprite animation later)
	public void updateGraphics (){
		this.gameObject.GetComponent<SpriteRenderer>().sprite= this.sprite;
		this.gameObject.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);
	}

	public GameObject getGameObject(){
		return this.gameObject;
	}
	public void setPosition(Vector2 pos){this.gameObject.transform.position = pos;}
	public Vector2 getPosition(){return this.gameObject.transform.position;}

	public void Destroy(){

		GameObject.Destroy(this.gameObject);
	}
	
}

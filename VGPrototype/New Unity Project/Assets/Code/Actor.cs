using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Positionable{


	private Rigidbody2D physicsComponent;
	public Actor(Vector2 position, Sprite sprite) : base(position, sprite){
		this.physicsComponent = this.getGameObject().AddComponent<Rigidbody2D>();
	}
}

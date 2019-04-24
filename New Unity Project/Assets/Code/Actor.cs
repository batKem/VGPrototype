using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Actor : Positionable{

	
	private Rigidbody2D physicsComponent;
    private CapsuleCollider2D colliderComponent;
    public Actor(Vector2 position, Sprite sprite) : base(position, sprite){
		this.physicsComponent = this.getGameObject().AddComponent<Rigidbody2D>();
        this.colliderComponent = this.getGameObject().AddComponent<CapsuleCollider2D>();
    }
    public Rigidbody2D GetRigidbody2D() {
        return this.physicsComponent;
    }
    public CapsuleCollider2D GetCapsuleCollider2D() {
        return this.colliderComponent;
    }
	

	


}
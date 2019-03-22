using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Actor{

    public float velocity = 0.1f;
    public Vector3 velocityVect, acceleration;
    private Vector3 absoluteSize = new Vector3(0.2f, 0.3f, 1);
    private Camera mainCamera;

    public Player(Vector2 pos, Sprite sp) : base(pos,sp){}
    public Player(Vector2 pos) : base(pos, Resources.Load<Sprite>("Sprites/Tile")) {
        this.getGameObject().transform.localScale = absoluteSize;

        //TODO: camera follow on the player (currently static)
        //this.mainCamera = this.getGameObject().AddComponent<Camera>();
        //this.mainCamera = GameObject.Instantiate(mainCamera, getPosition(), Quaternion.identity);
    }

    override public void update(){
        
        this.setPosition(getPosition()+getAxis() );
        this.updateGraphics();
        //this.mainCamera.gameObject.transform.position = getPosition();
        
    }

    
    //Input management method
    private Vector2 getAxis(){
        //TODO: make movements acceleration based
        // ? use unity's gravity or blend custom gravity in our acceleration simulation?
        Vector2 direction = new Vector2(0,0);
        if (Input.GetKey(KeyCode.Q)){
            direction.x -= velocity;
        }
        
        if (Input.GetKey(KeyCode.D)){
            direction.x += velocity;
        }
        
        return direction;
    }


}
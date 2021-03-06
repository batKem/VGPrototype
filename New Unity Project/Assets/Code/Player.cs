using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Actor{

    public float velocity = 0.1f;
    private float inHorizontalForce = 0.2f;
    private float maxAbsolutSpeed = 1.0f;
    private float mass = 8.0f;

    private float groundDetection;

    public Vector3 velocityVect, acceleration;
    private float friction = 0.15f;
    private Vector3 absoluteSize = new Vector3(0.2f, 0.3f, 1);
    private GameObject mainCamera;

    public Player(Vector2 pos, Sprite sp) : base(pos,sp){}
    public Player(Vector2 pos) : base(pos, Resources.Load<Sprite>("Sprites/Tile")) {
        this.getGameObject().transform.localScale = absoluteSize;
        this.GetRigidbody2D().constraints = RigidbodyConstraints2D.FreezeRotation;

        //TODO: camera follow on the player (currently static)
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //this.mainCamera = GameObject.Instantiate(mainCamera, getPosition(), Quaternion.identity);
    }

    override public void update(){
        
        this.setPosition(getPosition() + move() );
        this.updateGraphics();
        this.mainCamera.transform.position = getCameraPosition();
        
    }

    
    //Input management method
    private Vector2 move(){

        Vector3 totalAcc = (new Vector3(inHorizontalForce, 0.0f, 0.0f) )/mass;
        

        if (Input.GetKey(KeyCode.Q))
        {
            velocityVect -= totalAcc;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            velocityVect += totalAcc;
        }


        groundDetection = isCollidingWithTag("floor") ? 1.0f : 0.0f;
        velocityVect -= velocityVect * friction;

        if (Input.GetKey(KeyCode.Z)) {
            Vector3 verticalImpulse = new Vector3(0.0f, 4.0f, 0.0f);

            float isFalling = velocityVect.y <= 0.01f ? 1.0f : 0.0f;

            velocityVect += groundDetection* isFalling *(verticalImpulse / mass);
        }
        

        return  boundVectOnAxis( velocityVect,0, -1.0f, 1.0f) ;
    }



    //external collision check with tags, null will check against all objects
    bool isCollidingWithTag(string tag) {
        bool isColliding = false;
        if (tag == null)
        {
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>() )
            {
                if (obj.GetComponent<BoxCollider2D>() != null)

                    isColliding = GetCapsuleCollider2D().IsTouching(obj.GetComponent<BoxCollider2D>()) ? true: isColliding;
            }
        }
        else
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                if (obj.GetComponent<BoxCollider2D>() != null)

                    isColliding = GetCapsuleCollider2D().IsTouching(obj.GetComponent<BoxCollider2D>()) ? true : isColliding;
            }
        }
        return isColliding;
    }
    
    private Vector3 getCameraPosition()
    {
        return new Vector3(getPosition().x, mainCamera.gameObject.transform.position.y, mainCamera.gameObject.transform.position.z);

    }

    //TODO: make a custom vector class that inherits Vector3 and add these methods to it
    private Vector2 argMinX(Vector3 vec, Vector3 that)
    {
        return (vec.x < that.x) ? vec : that;
    }
    private Vector2 argMaxX(Vector3 vec, Vector3 that)
    {
        return (vec.x < that.x ) ? that : vec;
    }
    private Vector2 argMinY(Vector3 vec, Vector3 that)
    {
        return (vec.y < that.y) ? vec : that;
    }
    private Vector2 argMaxY(Vector3 vec, Vector3 that)
    {
        return (vec.y < that.y) ? that : vec;
    }

    private Vector3 boundVectOnAxis(Vector3 v, int axis, float min, float max) {
        //only taking x and y into account
        if(axis == 0) {
            Vector3 bounderyMin = new Vector3(min, v.y, v.z);
            Vector3 bounderyMax = new Vector3(max, v.y, v.z);
            return argMinX(argMaxX(v, bounderyMin), bounderyMax);
        }
        else if (axis == 1) {
            Vector3 bounderyMin = new Vector3(v.x, min, v.z);
            Vector3 bounderyMax = new Vector3(v.x, max, v.z);
            return argMinY(argMaxY(v, bounderyMin), bounderyMax);
        }

        else return Vector3.zero;
    }
    

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonPlayable : Actor{
    private State state;
    public NonPlayable(Vector2 position, Sprite sprite) : base(position, sprite){

        this.state = State.IDLE;
    }
    

    abstract public State react(State s);
    
    //Action handler, calling overrides of this method in children classes
    public void stateAction(State s){
        this.state = react(this.state);
		

	}
    //TODO: define react in children classes

}
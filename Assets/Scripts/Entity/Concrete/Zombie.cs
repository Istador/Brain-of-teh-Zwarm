using UnityEngine;
using System.Collections;

public class Zombie : MovableEntity<Zombie> {
	
	public Zombie() : base(100) {
		MaxSpeed = 15.0f;
		MaxForce = 15.0f;
		
	}
	
	protected override void Start(){
		base.Start();
	}
	
	public void Follow(PlayerObject player, Vector3 offset){
		Steering.Offset = offset;
		Steering.Target = (MovableEntity<Entity>)player;
		Steering.OffsetPursuing = true;
	}
}

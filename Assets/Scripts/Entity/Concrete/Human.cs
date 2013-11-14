using UnityEngine;
using System.Collections;

public class Human : MovableEntity<Human> {
	
	
	public Human() : base(100) {
		MaxSpeed = 10.0f;
		MaxForce = 10.0f;		
		Steering.Wandering = true;
	}
	
	
	
	private Material sprite;
	
		
		
	protected override void Start(){
		base.Start();
		
		sprite = transform.GetChild(0).renderer.material;
	}
	
	
	
	protected override void Update(){
		base.Update();
		
		Vector2 tmp = sprite.mainTextureScale;
		
		//Guckt nach links
		if( IsLeft(Pos + rigidbody.velocity) )
			//Textur nicht vertikal spiegeln
			tmp = new Vector2( Mathf.Abs(tmp.x), tmp.y);
		else
			//Textur vertikal spiegeln
			tmp = new Vector2( -Mathf.Abs(tmp.x), tmp.y);
			
		
		sprite.mainTextureScale = tmp;
	}
	
	
	
}

using UnityEngine;
using System.Collections;

public class Human : MovableEntity<Human> {

	public static float f_slowSpeed = 2.0f;
	public static float f_normalSpeed = 2.75f;
	public static float f_runSpeed = 3.25f;

	public static float f_runningTime = 10.0f; // 10 Sekunden rennen, dann erschöpft
	
	public Human() : base(100) {
		//Zustandsautomaten initialisieren
		MoveFSM.CurrentState = SHumanWander.I;

		MaxSpeed = f_normalSpeed;
		MaxForce = f_normalSpeed;
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


	//fliehe vor Zombie
	public void Flee(MovableEntity<Entity> zombie){
		Steering.Target = zombie;
		MessageDispatcher.I.Dispatch(this, "flee");
	}
	
	
}

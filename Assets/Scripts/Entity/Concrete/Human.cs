using UnityEngine;
using System.Collections;

public class Human : MovableEntity {

	public static float f_slowSpeed = 2.0f;
	public static float f_normalSpeed = 2.75f;
	public static float f_runSpeed = 3.25f;

	public static float f_aimTime = 1.0f;
	public static float f_runningTime = 6.0f; // 6 Sekunden rennen, dann erschöpft
	public static float f_slowTime = 10.0f; // Zeit in der langsam geflohen wird

	public Human() : base(100) {
		//Zustandsautomaten initialisieren
		MoveFSM.CurrentState = SHumanWander.I;

		MaxSpeed = f_normalSpeed;
		MaxForce = f_normalSpeed;

		Steering.WallAvoiding = true;
	}
	
	
	
	private Material sprite;
	public IWeapon Weapon = Weapons.Random;
		
		
	protected override void Start(){
		base.Start();

		//Sprite durch Waffe wählen
		transform.GetChild(0).GetComponent<Renderer>().materials = Weapon.Material;
		sprite = transform.GetChild(0).GetComponent<Renderer>().material;
	}
	
	
	
	protected override void Update(){
		base.Update();

		Vector2 tmp = sprite.mainTextureScale;
		
		//Guckt nach links
		if( IsLeft(Pos + GetComponent<Rigidbody>().velocity) )
			//Textur nicht vertikal spiegeln
			tmp = new Vector2( Mathf.Abs(tmp.x), tmp.y);
		else
			//Textur vertikal spiegeln
			tmp = new Vector2( -Mathf.Abs(tmp.x), tmp.y);
			
		
		sprite.mainTextureScale = tmp;
	}


	//fliehe vor Zombie
	public void Flee(MovableEntity zombie){
		Steering.Target = zombie;
		MessageDispatcher.I.Dispatch(this, "flee");
	}
	
	
}

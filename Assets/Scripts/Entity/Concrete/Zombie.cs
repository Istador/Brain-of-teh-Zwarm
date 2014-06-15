using UnityEngine;
using System.Collections;

public class Zombie : MovableEntity {
	
	private static string[] sounds;
	
	
	/// <summary>
	/// The Sound probability.
	/// FixedUpdate: 0.02s			50 Würfe die Sekunde
	/// Probability/Update: 0.1%	Chance von 1/1000 pro Wurf
	/// </summary>
	private static float f_soundProbability = 0.001f;
	

	public Zombie() : base(100) {
		MaxSpeed = 2.65f;
		MaxForce = 2.65f;
		Steering.Wandering = true;
	}



	public bool IsPlayerControled { get; private set; }
	
	
	private Material sprite;
	
		
		
	protected override void Start(){
		base.Start();

		IsPlayerControled = false;

		if(sounds == null){
			sounds = new string[7]{
				"RCL/01_01",
				"RCL/01_02",
				"RCL/01_03",
				"RCL/01_04",
				"RCL/01_05",
				"RCL/01_06",
				"RCL/01_07",
			};
		}
		
		Steering.WallAvoiding = true;

		sprite = transform.GetChild(1).renderer.material;
	}
	
	
	
	protected override void Update(){

		//falls das Ziel nicht mehr existiert entferne es
		if(Steering.Pursuing && ( Steering.Target == null || Steering.Target.IsDead)){
			Steering.Target = null;
			Steering.Pursuing = false;
			Steering.f_WanderFactor = 1f;
		}

		base.Update();

		Vector2 tmp = sprite.mainTextureScale;
		
		//Guckt nach rechts
		if( IsRight(Pos + rigidbody.velocity) )
			//Textur nicht vertikal spiegeln
			tmp = new Vector2( Mathf.Abs(tmp.x), tmp.y);
		else
			//Textur vertikal spiegeln
			tmp = new Vector2( -Mathf.Abs(tmp.x), tmp.y);
			
		
		sprite.mainTextureScale = tmp;
	}
	
	
	protected override void FixedUpdate(){
		base.FixedUpdate();
		
		if(rnd.NextDouble() < f_soundProbability)
			PlayRandomSound();
	}
	
	
	
	private void PlayRandomSound(){
		if(sounds != null && !Audio.isPlaying)
			PlaySound(sounds[rnd.Next(sounds.Length)]);
	}


	public void ChaseHuman(Human h){
		if(!IsPlayerControled){
			//Menschen jagen
			Steering.Target = h;
			Steering.Pursuing = true;
			Steering.f_PursueFactor = 0.85f;
			Steering.f_WanderFactor = 0.15f;
		}
	}
	
	
	public void Follow(PlayerObject player, Vector3 offset){
		//Steering.Wandering = false;
		Steering.f_WanderFactor = 0.15f;

		//nicht mehr menschen verfolgen
		Steering.Pursuing = false;

		//Schneller werden
		SpeedBonus += 0.5f;

		//in Formation
		Steering.Offset = offset;
		Steering.Target = player;
		Steering.OffsetPursuing = true;
		Steering.f_OffPursueFactor = 0.85f;

		//als Mitglied der Gruppe darf man Menschen fressen
		IsPlayerControled = true;
	}
}

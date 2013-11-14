using UnityEngine;
using System.Collections;

public class Zombie : MovableEntity<Zombie> {
	
	private static AudioClip[] sounds;
	
	
	/// <summary>
	/// The Sound probability.
	/// FixedUpdate: 0.02s			50 Würfe die Sekunde
	/// Probability/Update: 0.1%	Chance von 1/1000 pro Wurf
	/// </summary>
	private static float f_soundProbability = 0.001f;
	
	
	public Zombie() : base(100) {
		MaxSpeed = 8.5f;
		MaxForce = 8.5f;		
		Steering.Wandering = true;
	}
	
	
	
	private Material sprite;
	
		
		
	protected override void Start(){
		base.Start();
		
		if(sounds == null){
			sounds = new AudioClip[7]{
				Resource.Sound["RCL/01_01"],
				Resource.Sound["RCL/01_02"],
				Resource.Sound["RCL/01_03"],
				Resource.Sound["RCL/01_04"],
				Resource.Sound["RCL/01_05"],
				Resource.Sound["RCL/01_06"],
				Resource.Sound["RCL/01_07"],
			};
		}
		
		gameObject.AddComponent<AudioSource>();
		
		sprite = transform.GetChild(0).renderer.material;
	}
	
	
	
	protected override void Update(){
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
		if(sounds != null && audio != null && !audio.isPlaying)
			audio.PlayOneShot(sounds[rnd.Next(sounds.Length)]);
	}
	
	
	
	public void Follow(PlayerObject player, Vector3 offset){
		Steering.Wandering = false;
		
		MaxSpeed = 15.0f;
		MaxForce = 15.0f;
		
		Steering.Offset = offset;
		Steering.Target = (MovableEntity<Entity>)player;
		Steering.OffsetPursuing = true;
	}
}

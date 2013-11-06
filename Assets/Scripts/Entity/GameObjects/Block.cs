using UnityEngine;
using System.Collections.Generic;

public class Block : GeneralObject {
	
	public bool PlayerInside { get; private set;}
	public bool PlayerLeaved { get; private set;}
	
	//Objekte die sich zurzeit in diesem Block befinden
	private HashSet<GameObject> inside = new HashSet<GameObject>();
	
	
	protected override void Start(){
		base.Start();
		PlayerInside = false;
		PlayerLeaved = false;
	}
	
	
	
	void OnTriggerEnter(Collider other) {
		//Kollision mit Spieler
		if(!PlayerInside && !PlayerLeaved && other.name == "Zombie_Hat"){
			Debug.Log("nextBlock reached");
			
			PlayerInside = true;
			
			//Level mitteilen das Spieler ein Block weiter ist
			MessageDispatcher.I.Dispatch(this, LevelScript.I, "nextBlock");
		}
		
		inside.Add(other.gameObject);
	}
	
	
	
	void OnTriggerExit(Collider other) {
		//Kollision mit Spieler
		if(other.name == "Zombie_Hat"){
			PlayerInside = false;
			PlayerLeaved = true;
		}
		
		inside.Remove(other.gameObject);
	}
	
	
	
	//Entfernt diesen Block und alle Objekte in ihm
	public void Remove(){
		//entferne Objekte die sich im Block befinden
		foreach(GameObject o in inside){
			o.SetActive(false);
			Destroy(o);
		}
		
		gameObject.SetActive(false);
		Destroy(gameObject);
	}
}

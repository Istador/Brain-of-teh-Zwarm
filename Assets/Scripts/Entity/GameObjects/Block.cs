using UnityEngine;
using System.Collections;

public class Block : GeneralObject {
	
	
	
	void OnTriggerEnter(Collider other) {		
		Debug.Log(LevelScript.I.gameObject.name);
		
		//Kollision nur mit Spieler
		if(other.name == "Zombie_Hat" && LevelScript.I != null){
			Debug.Log("nextBlock reached");
			
			//Level mitteilen das Spieler ein Block weiter ist
			MessageDispatcher.I.Dispatch(this, LevelScript.I, "nextBlock");
		}
	}
	
	
	
	//Entfernt diesen Block und alle Objekte in ihm
	public void Remove(){
		//TODO: entferne Objekte die nicht Children sind
		
		gameObject.SetActive(false);
		Destroy(gameObject);
	}
}

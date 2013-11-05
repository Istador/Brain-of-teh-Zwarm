using UnityEngine;
using System.Collections;

/// 
/// Das PlayerCollider Script wird bei beweglichen Gegnern auf einen 
/// unsichtbaren zweiten Trigger in einem Kind-Objekt gelegt, um beim Spieler
/// Schaden zu verursachen wenn dieser ihn berührt.
/// 
public class PlayerCollider : MonoBehaviour {
	
	
	
	/// <summary>
	/// Schadenswert pro Berührung.
	/// Vom Inspektor aus einstellbar, und mit dem Prefab gespeichert.
	/// </summary>
	public int damage;
	
	
	/// <summary>
	/// minimale Zeit in Sekunden zwischen zwei Berührungen.
	/// Dadurch wird der Schaden nur mit zeitlichen Abständen verursacht.
	/// Vom Inspektor aus einstellbar, und mit dem Prefab gespeichert.
	/// </summary>
	public float tickLength;
	
	
	
	void Start(){
		lastPlayerDamage = Time.time;
	}
	
	
	
	/// <summary>
	/// Kollision mit dem Spieler führt zu Schaden beim Spieler
	/// </summary>
	void OnTriggerEnter(Collider other) {
		DoPlayerDamage(other);
	}

	
	
	/// <summary>
	/// Kollision mit dem Spieler führt zu Schaden beim Spieler
	/// </summary>
	void OnTriggerStay(Collider other) {
		DoPlayerDamage(other);
	}
	
	
	
	/// <summary>
	/// Zeitpunkt zu dem das letzte mal Schaden beim Spieler verursacht wurde
	/// </summary>
	private float lastPlayerDamage;
	
	
	
	/// <summary>
	/// TriggerEnter oder TriggerStay wurde ausgelöst. Eine gemeinsame Funktionalität
	/// Verursacht Schaden wenn der Spieler diesen Trigger berührt, und es bereits
	/// eine bestimmte Zeit her ist dass das letzte mal Scahden verursacht wurde.
	/// </summary>
	void DoPlayerDamage(Collider other){
		//Kollision nur mit Spieler
		if(other.gameObject.tag == "Player"){
			//nur in bestimmten Zeitabständen
			if(lastPlayerDamage + tickLength < Time.time){
				//Schaden verursachen
				Vector3 dmg = (other.collider.bounds.center - collider.bounds.center).normalized * damage;
				other.SendMessage("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
				//Aktuelle Zeit merken
				lastPlayerDamage = Time.time;
			}
		}
	}
	
	
	
}

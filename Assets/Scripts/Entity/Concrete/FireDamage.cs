using UnityEngine;
using System.Collections.Generic;
public class FireDamage : GeneralObject {



	public static readonly float f_dmgInterval = 0.2f;



	public static readonly int i_dmgAmount = 8;



	private HashSet<GameObject> inside = new HashSet<GameObject>();



	void OnTriggerEnter(Collider c){
		if(!inside.Contains(c.gameObject))
			inside.Add(c.gameObject);
	}



	void OnTriggerExit(Collider c){
		if(inside.Contains(c.gameObject))
			inside.Remove(c.gameObject);
	}



	protected override void Start(){
		base.Start();
		dmg();
	}



	public override bool HandleMessage(Telegram msg){
		switch(msg.message){
			case "dmgNow":
				dmg();
				return true;
			default:
				return false;
		}
	}



	private void dmg(){
		//Objekte die bereits zerstört wurden
		HashSet<GameObject> remove = new HashSet<GameObject>();

		//Alle die sich im Feuer befinden
		foreach(GameObject e in inside)
			if(e == null)
				remove.Add(e);
			else
				//Schaden verursachen
				DoDamage(e, i_dmgAmount);

		foreach(GameObject e in remove)
			inside.Remove(e);

		//In x sekunden erneut
		MessageDispatcher.I.Dispatch(this, "dmgNow", f_dmgInterval);
	}



}

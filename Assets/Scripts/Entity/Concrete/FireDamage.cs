using UnityEngine;
using System.Collections;

public class FireDamage : GeneralObject {

	
	void OnTriggerEnter(Collider c){ OnTrigger(c); }
	void OnTriggerStay(Collider c){ OnTrigger(c); }
	void OnTrigger(Collider c){
		Entity e = c.gameObject.GetComponent<Entity>();
		if(e != null){
			DoDamage(e, 1);
		}
	}
	
}

using UnityEngine;
using System.Collections;

public class PlayerHumanTrigger : MonoBehaviour {

	private MovableEntity<Entity> self;

	void Start(){
		self = transform.parent.parent.GetComponent<MovableEntity<Entity>>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Human"){
			Debug.Log("Human fleeing");
			//abhauen
			Human h = other.GetComponent<Human>();
			if(self != null && h != null) 
				h.Flee(self);
		}
	}
}

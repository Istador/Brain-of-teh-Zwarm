using UnityEngine;
using System.Collections;

public class PlayerHumanTrigger : MonoBehaviour {

	private MovableEntity self;
	private Zombie z;

	void Start(){
		self = transform.parent.parent.GetComponent<MovableEntity>();
		z = transform.parent.parent.GetComponent<Zombie>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Human"){
			Human h = other.GetComponent<Human>();
			if(self != null && h != null){
				//Mensch soll abhauen
				h.Flee(self);

				//wenn wir ein Zombie sind
				if(z != null)
					z.ChaseHuman(h); //Jage den Menschen
			}
		}
	}
}

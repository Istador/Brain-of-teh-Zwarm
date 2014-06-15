using UnityEngine;
using System.Collections;

/// 
/// Der PlayerTrigger wird ausgelöst, um Zombies einzusammeln oder
/// Menschen aufzufressen
/// 
public class PlayerTrigger : MonoBehaviour {

	private GeneralObject self;
	private Zombie z;
	private PlayerObject p;

	void Start(){
		z = transform.parent.GetComponent<Zombie>();
		p = transform.parent.GetComponent<PlayerObject>();
		if(z != null) self = z;
		else if(p != null) self = p;
	}

	private bool IsPlayerControlled { get {
			return p != null || (z != null && z.IsPlayerControled);
		}
	}

	void OnTriggerEnter(Collider other) {
		//wenn ein Zombie, und wir sind ein Schwarmzombie
		if(other.tag == "Zombie" && IsPlayerControlled){
			//zur Gruppe hinzufuegen
			Zombie z = other.GetComponent<Zombie>();
			if(z != null) PlayerObject.I.AddToGroup(z);

		} else if (other.tag == "Human"){
			//vernichten
			Human h = other.GetComponent<Human>();
			if(h != null){
				//nur dann Punkte dem Spieler gutschreiben, wenn dieser auch 
				if(IsPlayerControlled){
					PlayerObject.I.Brains++;
					GameOver.Brains++;
				}

				//Essgeräusch abspielen
				if(self != null) self.PlaySound("RCL/omnomnom");

				//aber auf jedenfall sterben
				h.Death();
			}
		}
	}
}

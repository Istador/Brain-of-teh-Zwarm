using UnityEngine;
using System.Collections;

/// 
/// Der PlayerTrigger wird ausgelöst, um Zombies einzusammeln oder
/// Menschen aufzufressen
/// 
public class PlayerTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Zombie"){
			//zur Gruppe hinzufuegen
			Zombie z = other.GetComponent<Zombie>();
			if(z != null) PlayerObject.I.AddToGroup(z);

		} else if (other.tag == "Human"){
			//vernichten
			Human h = other.GetComponent<Human>();
			if(h != null) PlayerObject.I.Eat(h);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Horizont : MonoBehaviour {

	private static bool b_showHorizont = true;
	
	void Start(){
		Inputs.I.Register("Horizont", ()=>umschalten());
	}
	
	void umschalten(){
		b_showHorizont = ! b_showHorizont;
		transform.GetChild(0).gameObject.SetActive(b_showHorizont);
	}

}

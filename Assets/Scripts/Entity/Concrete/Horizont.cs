using UnityEngine;
using System.Collections;

public class Horizont : MonoBehaviour {

	private static bool b_showHorizont = true;

	private bool but_status = false;

	// Update is called once per frame
	void Update () {
		bool new_status = Input.GetButton("Horizont");

		if(but_status && !new_status){
			but_status = false;
		}
		else if(!but_status && new_status){
			but_status = true;
			umschalten();
		}
	}

	void umschalten(){
		b_showHorizont = ! b_showHorizont;
		transform.GetChild(0).gameObject.SetActive(b_showHorizont);
	}

}

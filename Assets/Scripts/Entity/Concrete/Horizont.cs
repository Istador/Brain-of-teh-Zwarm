using UnityEngine;
using System.Collections;

public class Horizont : MonoBehaviour {

	private static bool b_showHorizont = true;
	
	void Start(){
		Inputs.I.Register("Horizont", ()=>umschalten());

		//bei zu breiter Aspektratio ausschalten
		float asp = (float)Screen.width / (float)Screen.height;

		if(Input.touchCount > 0 || asp > 1.44f)
			transform.GetChild(0).gameObject.SetActive(false);
	}
	
	void umschalten(){
		b_showHorizont = ! b_showHorizont;
		transform.GetChild(0).gameObject.SetActive(b_showHorizont);
	}

}

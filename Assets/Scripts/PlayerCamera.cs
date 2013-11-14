using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(PlayerObject.I != null && PlayerObject.I.Health > 0){
			Vector3 pos = transform.position;
			pos.x = PlayerObject.I.transform.position.x;
			this.transform.position = pos;
		}
	}
}

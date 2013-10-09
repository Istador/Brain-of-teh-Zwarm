using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	
	GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Zombie_Hat");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x = player.transform.position.x;
		this.transform.position = pos;
	}
}

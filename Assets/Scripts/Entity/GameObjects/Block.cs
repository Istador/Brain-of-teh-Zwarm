﻿using UnityEngine;
using System.Collections.Generic;

public class Block : GeneralObject {
	
	private static Transform entityParent;
	
	public Vector3[] spawnPoints;
	
	
	public bool PlayerInside { get; private set;}
	public bool PlayerLeaved { get; private set;}
	
	//Objekte die sich zurzeit in diesem Block befinden
	private HashSet<GameObject> inside = new HashSet<GameObject>();
	
	
	protected override void Start(){
		base.Start();
		
		if(entityParent == null)
			entityParent = GameObject.Find("Entities").transform;
		
		PlayerInside = false;
		PlayerLeaved = false;
		
		//maximale Anzahl Mobs die gespawnt werden können
		int maxMobs = System.Convert.ToInt32(System.Math.Sqrt((double)spawnPoints.Length))+1;
		Utility.MinMax(ref maxMobs, 0, spawnPoints.Length);

		//wieviele tatsächlich gespawnt werden sollen (mind. 1)
		int spawnN = rnd.Next(maxMobs+1)+1;
		Utility.MinMax(ref spawnN, 0, spawnPoints.Length);

		//Debug.Log("Spawn "+spawnN+" of max "+maxMobs);
		for(;spawnN>0; spawnN--){
			GameObject o = Instantiate(LevelScript.I.RandomEntity, RandomSpawnPoint);
			o.transform.parent = entityParent;
		}
		
	}
	
	
	private HashSet<int> spawned = new HashSet<int>();
	
	// gib einen zufälligen Spawnpunkt aus
	private Vector3 RandomSpawnPoint{get{
			if(spawned.Count >= spawnPoints.Length){
				Debug.LogError("No free SpawnPoints");
				return Vector3.zero;
			} else {
				int index = -1;
				do{
					index = rnd.Next(spawnPoints.Length);
					if(spawned.Contains(index)) index=-1;
					else spawned.Add(index);
				}
				while(index == -1);
				return transform.position + spawnPoints[index];
			}
		}}
	
	
	void OnTriggerEnter(Collider other) {
		//Kollision mit Spieler
		if(!PlayerInside && !PlayerLeaved && other.name == "Zombie_Hat"){
			//Debug.Log("nextBlock reached");
			
			PlayerInside = true;
			
			//Level mitteilen das Spieler ein Block weiter ist
			MessageDispatcher.I.Dispatch(this, LevelScript.I, "nextBlock");
		}
		
		inside.Add(other.gameObject);
	}
	
	
	
	void OnTriggerExit(Collider other) {
		//Kollision mit Spieler
		if(other.name == "Zombie_Hat"){
			PlayerInside = false;
			PlayerLeaved = true;
		}
		
		inside.Remove(other.gameObject);
	}
	
	
	
	//Entfernt diesen Block und alle Objekte in ihm
	public void Remove(){
		//entferne Objekte die sich im Block befinden
		foreach(GameObject o in inside) if(o != null) {
			o.SetActive(false);
			Destroy(o);
		}
		
		gameObject.SetActive(false);
		Destroy(gameObject);
	}
}

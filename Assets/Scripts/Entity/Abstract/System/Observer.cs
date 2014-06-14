using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//Die Beobachter des Wertes
public interface IObserver {
	//msg: das "Thema", value: der Wert der sich ändert
	void ObserveUpdate(string msg, object value);
}

//Klasse in der die Beobachter gespeichert werden
public class Observer {

	//die beobachter
	private Dictionary<string, HashSet<IObserver>> map = new Dictionary<string, HashSet<IObserver>>();

	//letzter gemerkter Wert
	private Dictionary<string, object> cache = new Dictionary<string, object>();

	public object Add(string msg, IObserver obj){

		//set bekommen
		HashSet<IObserver> set;
		map.TryGetValue(msg, out set);
		if(set == null){
			set = new HashSet<IObserver>();
			map[msg] = set;
		}

		//in set einfügen
		set.Add(obj);

		//sofern vorhanden den letzt bekannten Wert zurückgeben
		if(cache.ContainsKey(msg))
			return cache[msg];
		return null;
	}

	public void Remove(string msg, IObserver obj){
		if(map.ContainsKey(msg))
			map[msg].Remove(obj);
	}

	public void Update(string msg, object value){
		//Wert merken
		cache[msg] = value;

		//Debug.Log("update "+msg);
		//foreach(var k in map.Keys) Debug.Log("key: "+k);

		//Wert mitteilen
		if(map.ContainsKey(msg)){
			//Debug.Log("update "+msg+" "+map[msg].Count);
			foreach(var obj in map[msg])
				obj.ObserveUpdate(msg, value);
		}
	}


	/**
	 * Singleton
	*/
	private static Observer instance;
	private Observer(){}
	public static Observer Instance{get{
			if(instance==null) instance = new Observer();
			return instance;
		}}
	public static Observer I{get{return Instance;}}
}
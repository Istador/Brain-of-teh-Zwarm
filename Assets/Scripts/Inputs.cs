﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Inputs : MonoBehaviour {

	public interface IInputs {
		void Unregister(string name);
		void Register(string name, Action<bool> action);
		void Register(string name, Action action);
	}

	private class STInputs : IInputs {

		private Dictionary<string, bool> states = new Dictionary<string, bool>();
		private Dictionary<string, Action<bool>> actions = new Dictionary<string, Action<bool>>();
		
		public void Unregister(string name){
			actions.Remove(name);
			states.Remove(name);
		}

		public void Register(string name, Action action){
			Register(name, (bool b)=>{if(b)action();});
		}

		public void Register(string name, Action<bool> action){
			actions[name] = action;
			states[name] = false;
		}
		
		public void Clear(){
			states.Clear();
			actions.Clear();
		}

		public void Update(){
			//für alle überwachten Inputs
			foreach(string name in actions.Keys){
				bool oldstate = states[name];
				bool newstate = Input.GetButton(name);
				//bei einer Änderung
				if(oldstate ^ newstate){
					states[name] = newstate; //change state
					actions[name](newstate); //call method
				}
			}
		}


		/**
	 	* Singleton
		*/
		private static STInputs instance;
		private STInputs(){}
		public static STInputs Instance{get{
				if(instance==null) instance = new STInputs();
				return instance;
			}}
		public static STInputs I{get{return Instance;}}

	}
	
	private void Update(){
		STInputs.I.Update();
	}

	//Destruktor
	~Inputs(){
		//registrierte buttons leeren
		STInputs.I.Clear();
	}

	public static IInputs I{get{return STInputs.I;}}
}

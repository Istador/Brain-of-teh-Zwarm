﻿using UnityEngine;

public class SHumanFleeSlow : State<MovableEntity> {

	public override void Enter(MovableEntity owner){
		//langsam bewegen
		owner.MaxSpeed = Human.f_slowSpeed;
		owner.MaxForce = Human.f_slowSpeed;

		//Weglaufen
		owner.Steering.Fleeing = true;

		//nur bis zum Zeitpunkt x langsam weglaufen
		MessageDispatcher.I.Dispatch(owner, "timeout", Human.f_slowTime);
	}

	public override void Exit(MovableEntity owner){
		owner.Steering.Fleeing = false;
	}

	public override bool OnMessage(MovableEntity owner, Telegram msg){
		switch(msg.message){
		case "flee":
			return true;
		case "timeout":
			//Weglaufen beenden
			owner.MoveFSM.ChangeState(SHumanWander.I);
			return true;
		case "reloaded":
			SHumanAim.CheckDistance(owner);
			return true;
		default:
			return false;
		}
	}


	/**
	 * Singleton
	*/
	private static SHumanFleeSlow instance;
	private SHumanFleeSlow(){}
	public static SHumanFleeSlow Instance{get{
			if(instance==null) instance = new SHumanFleeSlow();
			return instance;
		}}
	public static SHumanFleeSlow I{get{return Instance;}}
}
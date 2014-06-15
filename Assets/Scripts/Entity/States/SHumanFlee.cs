using UnityEngine;

public class SHumanFlee : State<MovableEntity> {

	public override void Enter(MovableEntity owner){
		//Schneller bewegen
		owner.MaxSpeed = Human.f_runSpeed;
		owner.MaxForce = Human.f_runSpeed;

		//Weglaufen
		owner.Steering.Fleeing = true;

		//nur bis zum Zeitpunkt x schnell sein
		MessageDispatcher.I.Dispatch(owner, "timeout", Human.f_runningTime);
	}
	
	public override void Exit(MovableEntity owner){
		owner.Steering.Fleeing = false;
	}

	public override bool OnMessage(MovableEntity owner, Telegram msg){
		switch(msg.message){
		case "flee":
			return true;
		case "timeout":
			owner.MoveFSM.ChangeState(SHumanFleeSlow.I);
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
	private static SHumanFlee instance;
	private SHumanFlee(){}
	public static SHumanFlee Instance{get{
			if(instance==null) instance = new SHumanFlee();
			return instance;
		}}
	public static SHumanFlee I{get{return Instance;}}
}
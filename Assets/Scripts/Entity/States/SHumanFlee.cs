using UnityEngine;

public class SHumanFlee : State<MovableEntity<Human>> {

	public override void Enter(MovableEntity<Human> owner){
		//Schneller bewegen
		owner.MaxSpeed = Human.f_runSpeed;
		owner.MaxForce = Human.f_runSpeed;

		//Weglaufen
		owner.Steering.Fleeing = true;
		owner.Steering.WallAvoiding = true;

		//nur bis zum Zeitpunkt x schnell sein
		MessageDispatcher.I.Dispatch(owner, "timeout", Human.f_runningTime);
	}

	public override bool OnMessage(MovableEntity<Human> owner, Telegram msg){
		switch(msg.message){
		case "flee":
			return true;
		case "timeout":
			owner.MoveFSM.ChangeState(SHumanFleeSlow.I);
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
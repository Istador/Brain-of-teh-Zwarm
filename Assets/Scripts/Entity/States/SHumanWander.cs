using UnityEngine;

public class SHumanWander : State<MovableEntity> {

	public override void Enter(MovableEntity owner){
		owner.Steering.Wandering = true;

		//normal bewegen
		owner.MaxSpeed = Human.f_normalSpeed;
		owner.MaxForce = Human.f_normalSpeed;
	}

	public override void Exit(MovableEntity owner){
		owner.Steering.Wandering = false;
	}

	public override bool OnMessage(MovableEntity owner, Telegram msg){
		switch(msg.message){
		case "flee":
			//wenn er keine Waffe hat -> abhauen
			if( ((Human)owner).Weapon == Weapons.None)
				owner.MoveFSM.ChangeState(SHumanFlee.I);
			//ansonsten erst schießen
			else
				owner.MoveFSM.ChangeState(SHumanAim.I);
			return true;
		case "timeout":
			return true;
		case "reloaded":
			return true;
		default:
			return false;
		}
	}


	/**
	 * Singleton
	*/
	private static SHumanWander instance;
	private SHumanWander(){}
	public static SHumanWander Instance{get{
			if(instance==null) instance = new SHumanWander();
			return instance;
		}}
	public static SHumanWander I{get{return Instance;}}
}
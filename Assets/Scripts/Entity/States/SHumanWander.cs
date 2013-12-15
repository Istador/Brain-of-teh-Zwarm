using UnityEngine;

public class SHumanWander : State<MovableEntity<Human>> {

	public override void Enter(MovableEntity<Human> owner){
		owner.Steering.Wandering = true;
	}

	public override void Exit(MovableEntity<Human> owner){
		owner.Steering.Wandering = false;
	}

	public override bool OnMessage(MovableEntity<Human> owner, Telegram msg){
		switch(msg.message){
		case "flee":
			owner.MoveFSM.ChangeState(SHumanFlee.I);
			return true;
		case "timeout":
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
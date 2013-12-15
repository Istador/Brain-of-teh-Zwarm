using UnityEngine;

public class SHumanFleeSlow : State<MovableEntity<Human>> {

	public override void Enter(MovableEntity<Human> owner){
		//langsam bewegen
		owner.MaxSpeed = Human.f_slowSpeed;
		owner.MaxForce = Human.f_slowSpeed;
	}

	public override bool OnMessage(MovableEntity<Human> owner, Telegram msg){
		switch(msg.message){
		case "flee":
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
	private static SHumanFleeSlow instance;
	private SHumanFleeSlow(){}
	public static SHumanFleeSlow Instance{get{
			if(instance==null) instance = new SHumanFleeSlow();
			return instance;
		}}
	public static SHumanFleeSlow I{get{return Instance;}}
}
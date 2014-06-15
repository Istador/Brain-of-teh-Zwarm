using UnityEngine;

public class SHumanAim : State<MovableEntity> {

	public static void CheckDistance(MovableEntity owner){
		//Ziel ist bereits tot
		if(owner.Steering.Target == null || owner.Steering.Target.IsDead)
			//Herumwandern
			owner.MoveFSM.ChangeState(SHumanWander.I);
		//Ziel zu weit weg
		else if(owner.DistanceTo(owner.Steering.Target) > ((Human)owner).Weapon.Range)
			//gemütlich weggehen
			owner.MoveFSM.ChangeState(SHumanFleeArmed.I);
		//Ziel ist nah genug
		else
			owner.MoveFSM.ChangeState(SHumanAim.I);
	}



	public override void Enter(MovableEntity owner){
		//anhalten
		owner.StopMoving();

		//nur bis zum Zeitpunkt x zielen
		MessageDispatcher.I.Dispatch(owner, "timeout", Human.f_aimTime);
	}

	public override void Execute(MovableEntity owner){
		Entity t = owner.Steering.Target;

		if(t != null && !t.IsDead){
			if(owner.Pos.x <= t.Pos.x)
				owner.Moving = Vector3.left;
			else
				owner.Moving = Vector3.right;
		}
	}

	public override void Exit(MovableEntity owner){
		//wieder einschalten, weil es vom StopMoving ausgeschaltet wurde
		owner.Steering.WallAvoiding = true;

		owner.Moving = Vector3.zero;
	}


	public override bool OnMessage(MovableEntity owner, Telegram msg){
		switch(msg.message){
		case "flee":
			return true;
		case "timeout":
			//ziel bereits tot
			if(owner.Steering.Target == null || owner.Steering.Target.IsDead)
				owner.MoveFSM.ChangeState(SHumanWander.I);
			//schieße auf Ziel
			else {
				IWeapon w = ((Human)owner).Weapon;
				//schieße
				w.Shot(owner, owner.Steering.Target.Pos - owner.Pos);
				//Nachricht an selbst zum nachladen
				MessageDispatcher.I.Dispatch(owner, "reloaded", w.ReloadTime);
				//weglaufen zum nachladen
				owner.MoveFSM.ChangeState(SHumanFlee.I);
			}
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
	private static SHumanAim instance;
	private SHumanAim(){}
	public static SHumanAim Instance{get{
			if(instance==null) instance = new SHumanAim();
			return instance;
		}}
	public static SHumanAim I{get{return Instance;}}
}
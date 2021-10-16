using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_NPC_Idle : NPC_State
{

	public State_NPC_Idle(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
	}

	public override void UpdateState()
	{

		if (SM.myInputs.MoveInput != Vector2.zero)
		{
			SM.SwitchState(nameof(State_NPC_Move));
		}
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{

	}
}

public class State_NPC_Move : NPC_State
{
	public State_NPC_Move(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
	}

	public override void UpdateState()
	{

		if (SM.myInputs.MoveInput == Vector2.zero)
		{
			SM.SwitchState(nameof(State_NPC_Idle));
		}
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{

	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_NPC_Idle : NPC_State
{
	public State_NPC_Idle(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Idle));
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Move)))
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
		SM.myInputs.ResetInput(nameof(State_NPC_Idle));
	}
}

public class State_NPC_Move : NPC_State
{
	Vector2 currentTarget;
	float distanceLimit;

	public State_NPC_Move(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Move));

		currentTarget = TestNavmeshThings.GetRandomPOI(2);

		NPC_SM.agent.SetDestination(currentTarget);
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Idle)))
		{
			SM.SwitchState(nameof(State_NPC_Idle));
		}
		else if ((!NPC_SM.agent.pathPending && !NPC_SM.agent.hasPath) || (NPC_SM.agent.remainingDistance <= 0.05f))
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
		SM.myInputs.ResetInput(nameof(State_NPC_Move));
		NPC_SM.agent.SetDestination(NPC_SM.transform.position);
	}
}
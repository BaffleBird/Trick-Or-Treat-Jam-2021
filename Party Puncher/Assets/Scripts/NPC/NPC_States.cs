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
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myInputs.GetInput(nameof(State_NPC_Move)))
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
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myInputs.GetInput(nameof(State_NPC_Idle)))
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

public class State_NPC_Knockdown : NPC_State
{
	Vector2 currentMotion;

	public State_NPC_Knockdown(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Knockdown));
		currentMotion = SM.myInputs.MoveInput;
		SM.gameObject.layer = LayerMask.NameToLayer("NPCdown");
		NPC_SM.agent.enabled = false;
	}

	public override void UpdateState()
	{
		if(currentMotion.sqrMagnitude < 0.01f)
			SM.SwitchState(nameof(State_NPC_Idle));
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion = Vector2.Lerp(currentMotion, Vector2.zero, 0.1f);
		return currentMotion;
	}

	public override void EndState()
	{
		SM.gameObject.layer = LayerMask.NameToLayer("NPC");
		NPC_SM.agent.enabled = true;
	}
}
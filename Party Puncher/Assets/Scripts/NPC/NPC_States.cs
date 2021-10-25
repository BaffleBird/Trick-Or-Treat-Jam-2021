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
		else if (SM.myInputs.GetInput(nameof(State_NPC_GoForCandy)))
		{
			SM.SwitchState(nameof(State_NPC_GoForCandy));
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
		else if (SM.myInputs.GetInput(nameof(State_NPC_GoForCandy)))
		{
			SM.SwitchState(nameof(State_NPC_GoForCandy));
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
	float downTime = 2;
	float counter = 0;

	public State_NPC_Knockdown(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Knockdown));
		currentMotion = SM.myInputs.MoveInput;
		SM.gameObject.layer = LayerMask.NameToLayer("NPCdown");
		NPC_SM.agent.updatePosition = false;

		counter = downTime;
	}

	public override void UpdateState()
	{
		counter -= Time.deltaTime;

		if(currentMotion.sqrMagnitude < 0.01f && counter <= 0)
			SM.SwitchState(nameof(State_NPC_GetUp));
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion = Vector2.Lerp(currentMotion, Vector2.zero, 0.1f);
		return currentMotion;
	}

	public override void EndState()
	{
	}
}

public class State_NPC_GetUp : NPC_State
{
	public State_NPC_GetUp(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.gameObject.layer = LayerMask.NameToLayer("NPCdown");
	}

	public override void UpdateState()
	{
		SM.SwitchState(nameof(State_NPC_Idle));
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
		SM.gameObject.layer = LayerMask.NameToLayer("NPC");
		NPC_SM.agent.nextPosition = SM.transform.position;
		NPC_SM.agent.SetDestination(SM.transform.position);
		NPC_SM.agent.updatePosition = true;
	}
}

public class State_NPC_GoForCandy : NPC_State
{
	public State_NPC_GoForCandy(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		NPC_SM.agent.SetDestination(SM.myInputs.MoveInput);
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if ((!NPC_SM.agent.pathPending && !NPC_SM.agent.hasPath) || (NPC_SM.agent.remainingDistance <= 0.05f))
			SM.SwitchState(nameof(State_NPC_GrabCandy));
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
	}
}

public class State_NPC_GrabCandy : NPC_State
{
	public State_NPC_GrabCandy(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		NPC_SM.agent.SetDestination(SM.myInputs.MoveInput);
		NPC_SM.agent.updatePosition = false;
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myStatus.GetCooldownReady("GrabbinCandy"))
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
		SM.myInputs.ResetInput(nameof(State_NPC_GoForCandy));
		NPC_SM.agent.nextPosition = SM.transform.position;
		NPC_SM.agent.SetDestination(SM.transform.position);
		NPC_SM.agent.updatePosition = true;
	}
}
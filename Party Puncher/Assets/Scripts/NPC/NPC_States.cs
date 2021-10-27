using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_NPC_Idle : NPC_State
{
	public State_NPC_Idle(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Idle));
		SM.myAnimator.Play("Idle", 0, Random.value);
	}

	public override void UpdateState()
	{
		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Idle));
	}

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myInputs.GetInput(nameof(State_NPC_Leave)))
		{
			SM.SwitchState(nameof(State_NPC_Leave));
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

		SM.myAnimator.Play("Move");
	}

	public override void UpdateState()
	{
		SM.mySprite.flipX = NPC_SM.agent.desiredVelocity.x > 0 ? false : true;

		Transition();
		
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

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myInputs.GetInput(nameof(State_NPC_Leave)))
		{
			SM.SwitchState(nameof(State_NPC_Leave));
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
}

public class State_NPC_Leave : NPC_State
{
	public State_NPC_Leave(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Move));
		NPC_SM.agent.SetDestination(MobSpawner.instance.transform.position);
		NPC_SM.agent.speed = 6;

		SM.gameObject.layer = LayerMask.NameToLayer("NPCdown");

		SM.myAnimator.Play("Move");
	}

	public override void UpdateState()
	{
		SM.mySprite.flipX = NPC_SM.agent.desiredVelocity.x > 0 ? false : true;

		if ((!NPC_SM.agent.pathPending && !NPC_SM.agent.hasPath) || (NPC_SM.agent.remainingDistance <= 0.05f))
		{
			SM.gameObject.layer = LayerMask.NameToLayer("NPC");
			GameManager.instance.dataSystem.npcCount--;
			NPC_SM.gameObject.SetActive(false);
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

	public override void Transition()
	{

	}
}

public class State_NPC_Knockdown : NPC_State
{
	protected Vector2 currentMotion;
	protected float downTime = 2;
	protected float counter = 0;

	public State_NPC_Knockdown(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		if (Random.value < 0.35f)
		{
			GameObject dropCandy = GameObject.Instantiate(NPC_SM.candyDrop);
			dropCandy.transform.position = SM.transform.position;
		}

		SM.myInputs.ResetInput(nameof(State_NPC_Knockdown));
		currentMotion = SM.myInputs.MoveInput;
		SM.gameObject.layer = LayerMask.NameToLayer("NPCdown");
		NPC_SM.agent.updatePosition = false;
		SM.myAnimator.Play("Hit");
		SM.mySprite.flipX = currentMotion.x > 0 ? true : false;

		counter = downTime;
	}

	public override void UpdateState()
	{
		counter -= Time.deltaTime;

		if (!SM.myInputs.GetInput("AnimationStart"))
		{
			SM.myAnimator.Play("Down");
		}

		Transition();
		
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion = Vector2.Lerp(currentMotion, Vector2.zero, 0.1f);
		return currentMotion;
	}

	public override void EndState() {}

	public override void Transition()
	{
		if (currentMotion.sqrMagnitude < 0.01f && counter <= 0)
			SM.SwitchState(nameof(State_NPC_GetUp));
	}
}

public class State_NPC_GetUp : NPC_State
{
	public State_NPC_GetUp(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myAnimator.Play("Getup");
		SM.gameObject.layer = LayerMask.NameToLayer("NPCdown");
	}

	public override void UpdateState()
	{
		if (!SM.myInputs.GetInput("AnimationStart"))
		{
			Transition();
		}
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

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Leave)))
			SM.SwitchState(nameof(State_NPC_Leave));
		else
			SM.SwitchState(nameof(State_NPC_Idle));
	}
}

public class State_NPC_GoForCandy : NPC_State
{
	public State_NPC_GoForCandy(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		NPC_SM.agent.SetDestination(SM.myInputs.MoveInput);
		SM.myAnimator.Play("Move");
	}

	public override void UpdateState()
	{
		SM.mySprite.flipX = NPC_SM.agent.desiredVelocity.x > 0 ? false : true;

		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState() {}

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
			SM.SwitchState(nameof(State_NPC_Knockdown));
		else if (SM.myInputs.GetInput(nameof(State_NPC_Leave)))
			SM.SwitchState(nameof(State_NPC_Leave));
		else if ((!NPC_SM.agent.pathPending && !NPC_SM.agent.hasPath) || (NPC_SM.agent.remainingDistance <= 0.05f))
			SM.SwitchState(nameof(State_NPC_GrabCandy));
	}
}

public class State_NPC_GrabCandy : NPC_State
{
	public State_NPC_GrabCandy(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		NPC_SM.agent.SetDestination(SM.myInputs.MoveInput);
		NPC_SM.agent.updatePosition = false;
		SM.myAnimator.Play("Idle");
	}

	public override void UpdateState()
	{
		Transition();
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

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myInputs.GetInput(nameof(State_NPC_Leave)))
		{
			SM.SwitchState(nameof(State_NPC_Leave));
		}
		else if (SM.myStatus.GetCooldownReady("GrabbinCandy"))
		{
			SM.SwitchState(nameof(State_NPC_Idle));
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Enemy_Idle : State_NPC_Idle
{
	public State_Enemy_Idle(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myInputs.GetInput(nameof(State_Enemy_Scare)))
		{
			SM.SwitchState(nameof(State_Enemy_Scare));
		}
		else if (SM.myInputs.GetInput(nameof(State_NPC_Move)))
		{
			SM.SwitchState(nameof(State_NPC_Move));
		}
	}
}

public class State_Enemy_Move : State_NPC_Move
{
	public State_Enemy_Move(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if (SM.myInputs.GetInput(nameof(State_Enemy_Scare)))
		{
			SM.SwitchState(nameof(State_Enemy_Scare));
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

public class State_Enemy_Scare : NPC_State
{
	float scareRadius = 6f;
	ContactFilter2D contactFilter = new ContactFilter2D();
	Collider2D[] collisions = new Collider2D[16];
	public State_Enemy_Scare(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myAnimator.Play("Scare");
		contactFilter.SetLayerMask(LayerMask.GetMask("NPC"));
	}

	public override void UpdateState()
	{
		//Spawn an OverlapCircle that does something
		if (SM.myInputs.GetInput("AnimationHit"))
		{
			Physics2D.OverlapCircle(SM.transform.position, scareRadius, contactFilter, collisions);
			for (int i = 0; i < collisions.Length; i++)
			{
				if (collisions[i] != null)
				{
					NPC_StateMachine npc = collisions[i].GetComponent<NPC_StateMachine>();
					if (npc)
						npc.Flee();
					collisions[i] = null;
				}
			}
		}

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
		SM.myInputs.ResetInput(nameof(State_Enemy_Scare));
	}

	public override void Transition()
	{
		SM.SwitchState(nameof(State_NPC_Idle));
	}

	public override void TestUpdate()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
			Gizmos.DrawWireSphere(SM.transform.position, scareRadius);
	}
}

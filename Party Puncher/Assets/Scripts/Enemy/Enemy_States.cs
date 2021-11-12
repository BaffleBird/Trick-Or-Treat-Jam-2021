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
		else if (SM.myInputs.GetInput(nameof(State_Enemy_Sus)))
		{
			SM.SwitchState(nameof(State_Enemy_Sus));
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

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Move));

		currentTarget = GameObject.FindWithTag("NPC").transform.position;
		
		NPC_SM.agent.SetDestination(currentTarget);

		SM.myAnimator.Play("Move");

		timer = 15;
	}

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

public class State_Enemy_Knockdown: State_NPC_Knockdown
{
	public State_Enemy_Knockdown(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) {}

	public override void Transition()
	{
		if (currentMotion.sqrMagnitude < 0.01f && counter <= 0)
		{
			if (SM.myInputs.GetInput(nameof(State_Enemy_Die)))
				SM.SwitchState(nameof(State_Enemy_Die));
			else
				SM.SwitchState(nameof(State_NPC_GetUp));
		}
	}
}

public class State_Enemy_Leave : State_NPC_Leave
{
	float calmCounter = 5;
	float calmTime = 5;

	LayerMask mask;
	Transform player;

	public State_Enemy_Leave(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myInputs.ResetInput(nameof(State_NPC_Move));

		NPC_SM.agent.SetDestination(TestNavmeshThings.GetRandomPOI(2));
		NPC_SM.agent.speed = 9f;

		SM.gameObject.layer = LayerMask.NameToLayer("Enemy");

		SM.myAnimator.Play("Move");

		calmCounter = calmTime;
		mask = LayerMask.GetMask("Wall");
		player = GameObject.FindWithTag("Player").transform;
	}

	public override void UpdateState()
	{
		SM.mySprite.flipX = NPC_SM.agent.desiredVelocity.x > 0 ? false : true;

		if (Physics2D.Linecast(SM.transform.position, player.transform.position, mask))
			calmCounter -= Time.deltaTime;
		else
			calmCounter = calmTime;

		if ((!NPC_SM.agent.pathPending && !NPC_SM.agent.hasPath) || (NPC_SM.agent.remainingDistance <= 0.5f) && calmCounter > 0)
		{
			Vector2 nextPosition = TestNavmeshThings.GetFleeablePOI(5, SM.transform.position, player.position);
			NPC_SM.agent.SetDestination(nextPosition);
		}

		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
		NPC_SM.agent.SetDestination(NPC_SM.transform.position);
	}

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else if ((!NPC_SM.agent.pathPending && !NPC_SM.agent.hasPath) || (NPC_SM.agent.remainingDistance <= 0.05f) || calmCounter <= 0)
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
		AudioManager.Instance.PlayAtPoint("Roar", SM.transform.position);
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
					if (npc) npc.Flee();
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

public class State_Enemy_Die: NPC_State
{
	Color c;
	public State_Enemy_Die(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.gameObject.layer = LayerMask.NameToLayer("NPCdown");
		c = new Color(0, 0, 0, 0);
	}

	public override void UpdateState()
	{
		SM.mySprite.color = Color.Lerp(SM.mySprite.color, c, 0.02f);
		NPC_SM.myShadow.color = Color.Lerp(SM.mySprite.color, c, 0.02f);
		if (SM.mySprite.color.a <= 0.01f)
		{
			SM.gameObject.layer = LayerMask.NameToLayer("NPC");
			GameManager.instance.dataSystem.enemyCount--;
			GameManager.instance.dataSystem.enemiesDefeated++;
			GameManager.instance.dataSystem.score += 50;
			NPC_SM.gameObject.SetActive(false);
		}
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
	}

	public override void Transition()
	{	
	}
}

public class State_Enemy_Sus: NPC_State
{
	public State_Enemy_Sus(string name, NPC_StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myAnimator.Play("Sus");
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
		SM.myInputs.ResetInput(nameof(State_Enemy_Sus));
	}

	public override void Transition()
	{
		if (SM.myInputs.GetInput(nameof(State_NPC_Knockdown)))
		{
			SM.SwitchState(nameof(State_NPC_Knockdown));
		}
		else
		{
			SM.SwitchState(nameof(State_NPC_Idle));
		}
	}
}
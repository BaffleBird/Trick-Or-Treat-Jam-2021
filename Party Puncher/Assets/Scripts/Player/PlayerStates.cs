using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Player_Idle : Player_State
{
	Vector2 currentMotion;

	public State_Player_Idle(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		currentMotion = SM.myStatus.currentMovement;
		currentMotion.y = 0;
		SM.myAnimator.Play("Idle");

		Color c = PSM.aimSprite.color;
		c.a = 0;
		PSM.aimSprite.color = c;
	}

	public override void UpdateState()
	{
		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion.x = Mathf.Lerp(currentMotion.x, 0, 0.2f);
		currentMotion.y = Mathf.Lerp(currentMotion.y, 0, 0.2f);
		return currentMotion;
	}

	public override void EndState() {}

	public override void Transition()
	{
		if (SM.myInputs.GetInput("Attack"))
		{
			SM.SwitchState(nameof(State_Player_Shove));
		}
		else if (SM.myInputs.GetInput("AttackHold"))
		{
			SM.SwitchState(nameof(State_Player_Windup));
		}
		else if (SM.myInputs.GetInput("ThrowCandy") && GameManager.instance.dataSystem.candyCount > 0)
		{
			SM.SwitchState(nameof(State_Player_ReadyCandy));
		}
		else if (SM.myInputs.MoveInput != Vector2.zero)
		{
			SM.SwitchState(nameof(State_Player_Move));
		}
	}
}

public class State_Player_Move : Player_State
{
	float moveSpeed = 6f;
	Vector3 currentMotion;

	public State_Player_Move(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		currentMotion = SM.myStatus.currentMovement;
		SM.myAnimator.Play("Move");
	}

	public override void UpdateState()
	{
		SM.myAnimator.SetFloat("x_input", SM.myInputs.MoveInput.x);
		SM.myAnimator.SetFloat("y_input", SM.myInputs.MoveInput.y);

		if (SM.myInputs.MoveInput.x != 0)
			SM.mySprite.flipX = SM.myInputs.MoveInput.x < 0 ? true : false;

		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion = Vector2.Lerp(currentMotion, SM.myInputs.MoveInput * moveSpeed, 0.25f);
		return currentMotion;
	}

	public override void EndState() {}

	public override void Transition()
	{
		if (SM.myInputs.GetInput("Attack"))
		{
			SM.SwitchState(nameof(State_Player_Shove));
		}
		else if (SM.myInputs.GetInput("ThrowCandy") && GameManager.instance.dataSystem.candyCount > 0)
		{
			SM.SwitchState(nameof(State_Player_ReadyCandy));
		}
		else if (SM.myInputs.GetInput("Sprint") && SM.myInputs.MoveInput != Vector2.zero)
		{
			SM.SwitchState(nameof(State_Player_Sprint));
		}
		else if (SM.myInputs.MoveInput == Vector2.zero)
		{
			SM.SwitchState(nameof(State_Player_Idle));
		}
	}
}

public class State_Player_Sprint : Player_State
{
	float power = 12;
	float sprintSpeed = 10f;
	Vector3 currentMotion;
	LayerMask layerMask;
	ContactFilter2D contactFilter = new ContactFilter2D();
	Collider2D[] collisions = new Collider2D[8];

	public State_Player_Sprint(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		currentMotion = SM.myStatus.currentMovement;
		SM.myAnimator.Play("Sprint");
		layerMask = LayerMask.GetMask("NPC");
		layerMask |= 1 << LayerMask.NameToLayer("Enemy");
		contactFilter.SetLayerMask(layerMask);
	}

	public override void UpdateState()
	{
		SM.myAnimator.SetFloat("x_input", SM.myInputs.MoveInput.x);
		SM.myAnimator.SetFloat("y_input", SM.myInputs.MoveInput.y);

		if (SM.myInputs.MoveInput.x != 0)
			SM.mySprite.flipX = SM.myInputs.MoveInput.x < 0 ? true : false;

		Transition();
	}

	public override void FixedUpdateState()
	{
		Physics2D.OverlapCircle((Vector2)SM.transform.position + Vector2.up * 0.8f, SM.myCollider.bounds.size.x * 0.5f, contactFilter, collisions);
		for (int i = 0; i < collisions.Length; i++)
		{
			if (collisions[i] != null)
			{
				NPC_StateMachine npc = collisions[i].GetComponent<NPC_StateMachine>();
				if (npc)
				{
					Vector2 impactDirection = (Vector2)collisions[i].transform.position - (Vector2)SM.transform.position;
					bool fearChance = (Random.value < 0.3f);
					npc.Knockdown(impactDirection, power, fearChance);
				}
				collisions[i] = null;
			}
		}
		
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion = Vector2.Lerp(currentMotion, SM.myInputs.MoveInput * sprintSpeed, 0.25f);

		return currentMotion;
	}

	public override void EndState() {}

	public override void Transition()
	{
		if (SM.myInputs.GetInput("Attack") || SM.myInputs.GetInput("AttackHold"))
			SM.SwitchState(nameof(State_Player_Kick));
		else if (!SM.myInputs.GetInput("Sprint") && SM.myInputs.MoveInput != Vector2.zero)
			SM.SwitchState(nameof(State_Player_Move));
		else if (SM.myInputs.MoveInput == Vector2.zero)
			SM.SwitchState(nameof(State_Player_Idle));
	}

	//public override void TestUpdate()
	//{
	//	Gizmos.DrawWireSphere((Vector2)SM.transform.position + Vector2.up * 0.8f, SM.myCollider.bounds.size.x * 0.5f);
	//}
}

public class State_Player_ReadyCandy : Player_State
{
	public State_Player_ReadyCandy(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		Color c = PSM.aimSprite.color;
		c.a = 1;
		PSM.aimSprite.color = c;
		SM.myAnimator.Play("ThrowReady");
	}

	public override void UpdateState()
	{
		if (SM.myInputs.MoveInput != Vector2.zero)
		{
			SM.mySprite.flipX = SM.myInputs.MoveInput.x < 0 ? true : false;
			float angle = Mathf.Atan2(SM.myInputs.MoveInput.y, SM.myInputs.MoveInput.x) * Mathf.Rad2Deg;
			PSM.aimSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			PSM.candyParticles.transform.rotation = Quaternion.LookRotation(SM.myInputs.MoveInput);
		}

		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
		Color c = PSM.aimSprite.color;
		c.a = 0;
		PSM.aimSprite.color = c;
	}

	public override void Transition()
	{
		if (SM.myInputs.GetInput("Attack"))
		{
			SM.SwitchState(nameof(State_Player_Idle));
		}
		else if (!SM.myInputs.GetInput("ThrowCandy"))
		{
			SM.SwitchState(nameof(State_Player_ThrowCandy));
		}
	}
}

public class State_Player_ThrowCandy : Player_State
{
	public State_Player_ThrowCandy(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		float flip = SM.mySprite.flipX ? -1 : 1;

		SM.myAnimator.Play("Throw");
	}

	public override void UpdateState()
	{
		if(SM.myInputs.GetInput("AnimationHit"))
		{
			Vector3 throwPosition = SM.transform.position + (PSM.candyParticles.transform.rotation * Vector3.forward * 3.6f);

			PSM.candyParticles.Emit(8);

			RaycastHit2D hit = Physics2D.Raycast(SM.transform.position, PSM.candyParticles.transform.rotation * Vector3.forward, 3.6f, LayerMask.GetMask("Wall"));
			if (hit.collider != null)
			{
				throwPosition = hit.point;
				throwPosition = SM.transform.position + ((Vector3)hit.point - SM.transform.position * 0.8f);
			}

			GameObject candyThrow = GameObject.Instantiate(PSM.candyPrefab);
			candyThrow.transform.position = throwPosition;
			SM.myInputs.ResetInput("AnimationHit");
			GameManager.instance.dataSystem.candyCount--;
		}

		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState() {}

	public override void Transition()
	{
		if (!SM.myInputs.GetInput("AnimationStart"))
		{
			SM.SwitchState(nameof(State_Player_Idle));
		}
	}
}

public class State_Player_Shove : Player_State
{
	float power = 15;
	float pushRadius = 0.6f;
	Vector2 offset = new Vector2(0.8f, 0.8f);
	LayerMask layerMask;
	ContactFilter2D contactFilter = new ContactFilter2D();
	Collider2D[] collisions = new Collider2D[8];

	public State_Player_Shove(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		offset.x = Mathf.Abs(offset.x);
		offset.x *= SM.mySprite.flipX ? -1 : 1;
		SM.myAnimator.SetFloat("x_input", SM.mySprite.flipX ? -1 : 1);
		SM.myAnimator.Play("Shove");
		layerMask = LayerMask.GetMask("NPC");
		layerMask |= 1 << LayerMask.NameToLayer("Enemy");
		contactFilter.SetLayerMask(layerMask);
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
		{
			Physics2D.OverlapCircle((Vector2)SM.transform.position + offset, pushRadius, contactFilter, collisions);
			for (int i = 0; i < collisions.Length; i++)
			{
				if (collisions[i] != null)
				{
					NPC_StateMachine npc = collisions[i].GetComponent<NPC_StateMachine>();
					if (npc)
					{
						Vector2 impactDirection = (Vector2)collisions[i].transform.position - (Vector2)SM.transform.position;
						bool fearChance = (Random.value < 0.5f);
						npc.Knockdown(impactDirection, power, fearChance);
					}
					collisions[i] = null;
				}
			}
		}
		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
		SM.myInputs.ResetInput("Attack");
	}

	public override void Transition()
	{
		if (!SM.myInputs.GetInput("AnimationStart"))
		{
			SM.SwitchState(nameof(State_Player_Idle));
		}
	}

	public override void TestUpdate()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
			Gizmos.DrawWireSphere((Vector2)SM.transform.position + offset, pushRadius);
	}
}

public class State_Player_Windup : Player_State
{
	public State_Player_Windup(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		SM.myAnimator.Play("PunchStart");
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
		SM.myInputs.ResetInput("AttackHold");
		SM.myInputs.ResetInput("Attack");
		SM.myInputs.ResetInput("ThrowCandy");
	}

	public override void Transition()
	{
		if (!SM.myInputs.GetInput("AttackHold"))
		{
			SM.SwitchState(nameof(State_Player_Punch));
		}
		else if (SM.myInputs.GetInput("ThrowCandy"))
		{
			SM.SwitchState(nameof(State_Player_Idle));
		}
	}

}

public class State_Player_Punch : Player_State
{
	float power = 20;
	float pushRadius = 0.8f;
	Vector2 offset = new Vector2(1.6f, 0.8f);
	LayerMask layerMask;
	ContactFilter2D contactFilter = new ContactFilter2D();
	Collider2D[] collisions = new Collider2D[8];

	public State_Player_Punch(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		offset.x = Mathf.Abs(offset.x);
		offset.x *= SM.mySprite.flipX ? -1 : 1;
		SM.myAnimator.SetFloat("x_input", SM.mySprite.flipX ? -1 : 1);
		SM.myAnimator.Play("Punch");
		layerMask = LayerMask.GetMask("NPC");
		layerMask |= 1 << LayerMask.NameToLayer("Enemy");
		contactFilter.SetLayerMask(layerMask);
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
		{
			Physics2D.OverlapCircle((Vector2)SM.transform.position + offset, pushRadius, contactFilter, collisions);
			for (int i = 0; i < collisions.Length; i++)
			{
				if (collisions[i] != null)
				{
					NPC_StateMachine npc = collisions[i].GetComponent<NPC_StateMachine>();
					if (npc)
					{
						Vector2 impactDirection = (Vector2)collisions[i].transform.position - (Vector2)SM.transform.position;
						npc.Knockdown(impactDirection, power, true);
					}
					collisions[i] = null;
				}
			}
		}
		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		return Vector2.zero;
	}

	public override void EndState()
	{
		SM.myInputs.ResetInput("AttackHold");
		SM.myInputs.ResetInput("Attack");
	}

	public override void Transition()
	{
		if (!SM.myInputs.GetInput("AnimationStart"))
		{
			SM.SwitchState(nameof(State_Player_Idle));
		}
	}

	public override void TestUpdate()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
			Gizmos.DrawWireSphere((Vector2)SM.transform.position + offset, pushRadius);
	}
}

public class State_Player_Kick : Player_State
{
	float power = 25;
	float pushRadius = 0.8f;
	float kickSpeed = 25;
	Vector2 offset = new Vector2(0f, 0.8f);
	Vector2 currentMotion;
	LayerMask layerMask;
	ContactFilter2D contactFilter = new ContactFilter2D();
	Collider2D[] collisions = new Collider2D[8];

	public State_Player_Kick(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		offset.x = Mathf.Abs(offset.x);
		offset.x *= SM.mySprite.flipX ? -1 : 1;
		currentMotion = SM.myStatus.currentMovement.normalized;
		SM.myAnimator.SetFloat("x_input", currentMotion.x);
		SM.myAnimator.SetFloat("y_input", currentMotion.y);
		SM.myAnimator.Play("Kick");
		layerMask = LayerMask.GetMask("NPC");
		layerMask |= 1 << LayerMask.NameToLayer("Enemy");
		contactFilter.SetLayerMask(layerMask);
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
		{
			Physics2D.OverlapCircle((Vector2)SM.transform.position + offset + (currentMotion.normalized * 1.6f), pushRadius, contactFilter, collisions);
			for (int i = 0; i < collisions.Length; i++)
			{
				if (collisions[i] != null)
				{
					NPC_StateMachine npc = collisions[i].GetComponent<NPC_StateMachine>();
					if (npc)
					{
						Vector2 impactDirection = (Vector2)collisions[i].transform.position - (Vector2)SM.transform.position;
						npc.Knockdown(impactDirection, power, true);
					}
					collisions[i] = null;
				}
			}
		}
		Transition();
	}

	public override Vector2 MotionUpdate()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
			currentMotion = Vector2.Lerp(currentMotion, currentMotion.normalized * kickSpeed, 0.25f);
		return currentMotion;
	}

	public override void EndState()
	{
		SM.myInputs.ResetInput("AttackHold");
		SM.myInputs.ResetInput("Attack");
	}

	public override void Transition()
	{
		if (!SM.myInputs.GetInput("AnimationStart"))
		{
			SM.SwitchState(nameof(State_Player_Idle));
		}
	}

	public override void TestUpdate()
	{
		if (SM.myInputs.GetInput("AnimationHit"))
		{
			Gizmos.DrawWireSphere((Vector2)SM.transform.position + offset + (currentMotion.normalized * 1.6f), pushRadius);
		}
			
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_StateMachine : StateMachine
{
	protected NavMeshAgent _agent;
	public NavMeshAgent agent => _agent;

	[SerializeField] SpriteRenderer _myShadow;
	public SpriteRenderer myShadow => _myShadow;

	[SerializeField] GameObject _candyDrop;
	public GameObject candyDrop => _candyDrop;

	void Awake()
	{
		States.Add(nameof(State_NPC_Idle), new State_NPC_Idle(nameof(State_NPC_Idle), this));
		States.Add(nameof(State_NPC_Move), new State_NPC_Move(nameof(State_NPC_Move), this));
		States.Add(nameof(State_NPC_Knockdown), new State_NPC_Knockdown(nameof(State_NPC_Knockdown), this));
		States.Add(nameof(State_NPC_GetUp), new State_NPC_GetUp(nameof(State_NPC_GetUp), this));
		States.Add(nameof(State_NPC_Leave), new State_NPC_Leave(nameof(State_NPC_Leave), this));
		States.Add(nameof(State_NPC_GoForCandy), new State_NPC_GoForCandy(nameof(State_NPC_GoForCandy), this));
		States.Add(nameof(State_NPC_GrabCandy), new State_NPC_GrabCandy(nameof(State_NPC_GrabCandy), this));

		_myStatus = new EntityStatus();

		currentState = States[nameof(State_NPC_Idle)];
		_previousState = currentState.StateName;
		currentState.StartState();

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
		_agent.speed = Random.Range(2, 3.5f);
		_agent.SetDestination(transform.position);
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void SwitchState(State newState)
	{
		base.SwitchState(newState);
		myStatus.currentState = currentState.StateName;
	}

	public virtual void ResetNPC()
	{
		myInputs.ResetAllInputs();
		_agent.speed = Random.Range(2, 3.5f);
		_agent.nextPosition = transform.position;
		_agent.updatePosition = true;
	}

	private void OnEnable()
	{
		SwitchState(States[nameof(State_NPC_Idle)]);
		agent.speed = Random.Range(2, 3.5f);
	}

	public virtual void Knockdown(Vector2 direction, float power, bool fear) //Feed it a direction to be pushed, how powerful it is, and whether or not it causes fear
	{
		myInputs.ForceMove(direction * power);
		myInputs.ForceInput(nameof(State_NPC_Knockdown));
		if (fear) Flee();
	}

	public virtual void CandyPull(float pullTime, Vector2 targetPosition) //Feed it the amount of time to be distracted, and where it should go be distracted
	{
		if (myStatus.GetCooldownReady("GrabbinCandy"))
			myInputs.ForceMove(targetPosition);
		myStatus.SetCooldown("GrabbinCandy", pullTime);
		myInputs.ForceInput(nameof(State_NPC_GoForCandy));
	}

	public void Flee()
	{
		myInputs.ForceInput(nameof(State_NPC_Leave));
	}
}

public abstract class NPC_State : State
{
	protected NPC_StateMachine NPC_SM;

	public NPC_State(string name, NPC_StateMachine stateMachine) : base(name, stateMachine)
	{
		_stateName = name;
		NPC_SM = stateMachine;
	}
}

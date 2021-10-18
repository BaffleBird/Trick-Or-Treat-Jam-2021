using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	#region MajorComponents
	[SerializeField] protected EntityStatus _myStatus = null;
	public EntityStatus myStatus => _myStatus;

	[SerializeField] protected InputHandler _myInputs = null;
	public InputHandler myInputs => _myInputs;

	[SerializeField] protected Rigidbody2D _myRigidbody = null;
	public Rigidbody2D myRigidbody => _myRigidbody;

	[SerializeField] protected Collider2D _myCollider = null;
	public Collider2D myCollider => _myCollider;

	[SerializeField] protected Animator _myAnimator = null;
	public Animator myAnimator => _myAnimator;
	#endregion

	#region StateVariables
	protected Dictionary<string, State> States = new Dictionary<string, State>();
	protected State currentState = null;
	protected string _previousState = "";
	public string previousState => _previousState;
	#endregion

	protected virtual void Update()
	{
		if (currentState != null)
			currentState.UpdateState();
		myStatus.UpdateCooldowns();
	}

	protected virtual void FixedUpdate()
	{
		currentState.FixedUpdateState();
		myRigidbody.MovePosition(myRigidbody.position + currentState.MotionUpdate() * Time.fixedDeltaTime);
		myStatus.currentMovement = currentState.MotionUpdate();
	}

	protected virtual void LateUpdate()
	{
		currentState.LateUpdateState();
	}

	//STATE MANAGEMENT
	protected virtual void SwitchState(State newState)
	{
		currentState.EndState();
		_previousState = currentState.StateName;
		currentState = newState;
		currentState.StartState();
	}

	public void SwitchState(string stateName)
	{
		if (myStatus.GetCooldown(stateName))
		{
			SwitchState(States[stateName]);
		}
	}
}

public abstract class State
{
	protected StateMachine SM;

	protected string _stateName = null;
	public string StateName { get { return _stateName; } }

	public State(string name, StateMachine stateMachine)
	{
		_stateName = name;
		SM = stateMachine;
	}

	public abstract void StartState();
	public abstract void UpdateState();
	public virtual void FixedUpdateState() { }
	public virtual void LateUpdateState() { }
	public abstract Vector2 MotionUpdate();
	public abstract void EndState();
}

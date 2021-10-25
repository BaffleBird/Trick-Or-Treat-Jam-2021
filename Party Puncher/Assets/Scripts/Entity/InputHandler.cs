using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class InputHandler : MonoBehaviour
{
	protected Vector2 moveInput = new Vector2();
	public Vector2 MoveInput => moveInput;

	protected Vector2 pointerInput = new Vector2();
	public Vector2 PointerInput => pointerInput;
	public void ResetPointerInput() { pointerInput = Vector2.zero; }

	protected Dictionary<string, bool> inputs = new Dictionary<string, bool>();

	public bool GetInput(string s) => inputs[s];

	public void ResetInput(string inputName)
	{
		inputs[inputName] = false;
	}

	public void ResetAllInputs()
	{
		foreach (var key in inputs.Keys.ToList())
		{
			inputs[key] = false;
		}
	}

	public void ForceInput(string inputName)
	{
		inputs[inputName] = true;
	}

	public void ForceMove(Vector2 forceMove)
	{
		moveInput = forceMove;
	}

	public void AddInput(string inputName)
	{
		if (!inputs.ContainsKey(inputName))
			inputs.Add(inputName, false);
	}
}

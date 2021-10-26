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

	//Get a particular Input
	public bool GetInput(string s)
	{
		if (inputs.ContainsKey(s))
		{
			return inputs[s];
		}
		else
		{
			Debug.Log("Input: " + s + " Not Found");
			return false;
		}
		
	}

	//Reset an Input
	public void ResetInput(string inputName)
	{
		inputs[inputName] = false;
	}

	//Reset all the Inputs in the dictionary to false
	public void ResetAllInputs()
	{
		foreach (var key in inputs.Keys.ToList())
		{
			inputs[key] = false;
		}
	}

	//Force set an Input
	public void ForceInput(string inputName)
	{
		inputs[inputName] = true;
	}

	//Force set the Move Input
	public void ForceMove(Vector2 forceMove)
	{
		moveInput = forceMove;
	}

	//Add input with it's name as a key
	public void AddInput(string inputName)
	{
		if (!inputs.ContainsKey(inputName))
			inputs.Add(inputName, false);
	}

	//public vo
}

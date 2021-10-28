using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRadio : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler.AddInput("AnimationStart");
        inputHandler.AddInput("AnimationHit");
    }

    public void SignalOn(string inputName)
	{
        inputHandler.ForceInput(inputName);
	}

    public void SignalOff(string inputName)
	{
        inputHandler.ResetInput(inputName);
    }
}

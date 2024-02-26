using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    private Stack<State> states;

    private void Awake() => states = new Stack<State>();

    private void Update() { if (Time.timeScale != 0) GetCurrentState()?.Execute(); }

    public void PushState(Action active, Action onEnter, Action onExit)
    {
        GetCurrentState()?.OnExit();

        State state = new(active, onEnter, onExit);
        states.Push(state);

        GetCurrentState().OnEnter();
    }

    public void PopState()
    {
        GetCurrentState()?.OnExit();

        GetCurrentState().ActiveAction = null;
        states.Pop();

        GetCurrentState().OnEnter();
    }

    private State GetCurrentState() => states.Count > 0 ? states.Peek() : null;
    public Stack<State> States { get => states; set => states = value; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class StateTrasition
{
    public Func<bool> Condition { get; set; }
    public float TransitionDelay { get; set; }
}

public interface IState
{
    Dictionary<Type, StateTrasition> States { get; set; }
    void Init();
    void Execute();

}


public class BehaviourController : ITickable
{
    private readonly List<IState> states;
    private IState currentState;
    private float trasitionDelay;

    public BehaviourController(List<IState> states)
    {
        this.states = states;

        if (states == null || states.Count == 0)
            return;

        currentState = this.states.First();
        currentState.Init();
        trasitionDelay = 0.0f;
    }

    public void Tick()
    {
        if (states == null || states.Count == 0)
            return;

        currentState.Execute();

        if (currentState.States == null || currentState.States.Count == 0)
            return;


        foreach (var state in currentState.States)
        {
            if (state.Value.Condition.Invoke())
            {
                trasitionDelay += Time.deltaTime;
        
                if (trasitionDelay < state.Value.TransitionDelay)
                    break;

                var nextState = FindIState(state.Key);
                if (nextState == null)
                    break;

                currentState = nextState;
                currentState.Init();
                trasitionDelay = 0.0f;

                break;
            }

            trasitionDelay = 0.0f;
        }
    }

    public IState FindIState(Type type)
    {
        return states.Find(x => x.GetType() == type);
    }
}


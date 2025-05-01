using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFactory : IConcreteFactory
{
    public object CreateItem()
    {
        Dictionary<Type, IState> states = new Dictionary<Type, IState>();

        StateMachine fsm = new StateMachine();
        
        states.Add(typeof(StartState), new StartState(fsm));
        states.Add(typeof(IdleState), new IdleState(fsm));
        states.Add(typeof(EncounterState), new EncounterState(fsm));
        states.Add(typeof(GameOverState), new GameOverState(fsm));
        
        fsm.Constructor(states, typeof(StartState));
        
        return fsm;
    }
}
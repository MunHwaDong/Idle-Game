using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory : IConcreteFactory
{
    public object CreateItem()
    {
        Dictionary<Type, IState> states = new Dictionary<Type, IState>();

        StateMachine fsm = new StateMachine();
        
        states.Add(typeof(PlayerIdleState), new PlayerIdleState(fsm));
        states.Add(typeof(PlayerEncounterState), new PlayerEncounterState(fsm));
        
        fsm.Constructor(states, typeof(PlayerIdleState));
        
        return fsm;
    }
}

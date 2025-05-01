using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterStateFactory : IConcreteFactory
{
    public object CreateItem()
    {
        Dictionary<Type, IState> states = new Dictionary<Type, IState>();

        StateMachine fsm = new StateMachine();
        
        states.Add(typeof(NMonsterIdleState), new NMonsterIdleState(fsm));
        states.Add(typeof(NMonsterEncounterState), new NMonsterEncounterState(fsm));
        states.Add(typeof(NMonsterDeathState), new NMonsterDeathState(fsm));
        
        fsm.Constructor(states, typeof(NMonsterIdleState));
        
        return fsm;
    }
}


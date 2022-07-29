using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Attack : EntityState, IState
{
    private readonly SignalBus signalBus;
    private float attackDelay;

    public Dictionary<Type, StateTrasition> States { get; set; }

    public Attack(EntityObject entityObject, RigidbodyController rigidbodyController, SignalBus signalBus) : base(entityObject, rigidbodyController)
    {
        States = new Dictionary<Type, StateTrasition>();
        this.signalBus = signalBus;
    }

    public void Execute()
    {
        if (attackDelay > 0.0f)
        {
            attackDelay -= Time.deltaTime;
            return;
        }

        attackDelay = EntityObject.Data.attackDelay;
        var hitCollider = EntityObject.HitCollider;

        if (hitCollider == null)
            return;

        signalBus.Fire<AttackSignal>();
        var entityFacade = hitCollider.gameObject.GetComponent<EntityFacade>();

        if (entityFacade == null)
            return;

        entityFacade.OnHit(1);
    }

    public void Init()
    {

    }

}

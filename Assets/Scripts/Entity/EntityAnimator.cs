using System;
using UnityEngine;
using Zenject;

public class AttackSignal { }

public class EntityAnimator : IInitializable, IDisposable
{
    private readonly EntityObject entity;
    private readonly EntityFacade entityFacade;
    private readonly Animator animator;
    private readonly SignalBus signalBus;

    public EntityAnimator(EntityObject entity, EntityFacade entityFacade, Animator aniamtor, SignalBus signalBus)
    {
        this.entity = entity;
        this.entityFacade = entityFacade;
        this.animator = aniamtor;
        this.signalBus = signalBus;
    }

    public void Initialize()
    {
        signalBus.Subscribe<RigidbodySingal>(SetMoveParameter);
        signalBus.Subscribe<AttackSignal>(Attack);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<RigidbodySingal>(SetMoveParameter);
        signalBus.Unsubscribe<AttackSignal>(Attack);
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    private void SetMoveParameter(RigidbodySingal rbData)
    {
        animator.SetFloat("X", rbData.grounded ? rbData.x : 0);
        animator.SetFloat("Y", rbData.y);
    }
}


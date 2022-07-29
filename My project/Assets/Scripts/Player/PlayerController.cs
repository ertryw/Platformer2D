using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : IState
{
    private readonly InputController inputController;
    private readonly EntityObject player;
    private readonly SignalBus signalBus;
    private readonly RigidbodyController rigidbodyController;
    public Dictionary<Type, StateTrasition> States { get ; set ; }

    public PlayerController(InputController inputController, EntityObject entity, SignalBus signalBus, RigidbodyController rigidbodyController)
    {
        this.inputController = inputController;
        this.player = entity;
        this.signalBus = signalBus;
        this.rigidbodyController = rigidbodyController;
        rigidbodyController.VelocitySmooth = 0.01f;
    }

    public void Move()
    {
        Vector2 moveInput = inputController.Player.Move.ReadValue<Vector2>();
        Vector3 targetDirection = new Vector2(moveInput.x * player.Data.moveSpeed, moveInput.y);
        rigidbodyController.TargetVelocity = targetDirection;
    }

    public void Attack()
    {
        if (inputController.Player.Attack.triggered == false)
            return;

        signalBus.Fire<AttackSignal>();

        var hitCollider = player.HitCollider;

        if (hitCollider == null)
            return;

        var entityFacade = hitCollider.gameObject.GetComponent<EntityFacade>();

        if (entityFacade == null)
            return;

        entityFacade.OnHit(1);
    }

    public void Execute()
    {
        Move();
        Attack();
    }

    public void Init()
    {
       
    }
}

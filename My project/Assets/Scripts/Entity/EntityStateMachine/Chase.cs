using System;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EntityState, IState
{
    public Dictionary<Type, StateTrasition> States
    {
        get; set;
    }

    private EntityFacade chaseEntity;
    private EntityFacade prevChaseEntity;

    public Chase(EntityObject entityObject, RigidbodyController rigidbodyController) : base (entityObject, rigidbodyController)
    {
        States = new Dictionary<Type, StateTrasition>();
    }

    public void Init()
    {
        var hitCollider = EntityObject.HitCollider;

        if (hitCollider != null)
            chaseEntity = hitCollider.GetComponent<EntityFacade>();

        if (chaseEntity != null)
            prevChaseEntity = chaseEntity;

        RigidbodyController.VelocitySmooth = 0.1f;
    }

    public void Execute()
    {
        if (chaseEntity == null)
        {
            chaseEntity = prevChaseEntity;
        }

        if (chaseEntity == null)
            return;

        var distanceX = Mathf.Abs(chaseEntity.Position.x - EntityObject.Position.x);
        var direction = Mathf.Sign((chaseEntity.Position - EntityObject.Position).normalized.x);

        if (distanceX  > EntityObject.Data.chaseDistance)
            RigidbodyController.TargetVelocity = Vector3.right * (direction * EntityObject.Data.moveSpeed);
        else
            RigidbodyController.TargetVelocity = Vector3.zero;
    }


}

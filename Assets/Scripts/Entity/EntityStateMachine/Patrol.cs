using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public class EntityWayPoints
{
    public Transform[] transforms;
}

public class Patrol : EntityState, IState
{
    private readonly EntityWayPoints entityWayPoints;
    private int currentWaypointIndex = 0;

    public Dictionary<Type, StateTrasition> States  
    { 
        get; set; 
    } 

    public Patrol(EntityWayPoints entityWayPoints, EntityObject entityObject, RigidbodyController rigidbodyController) 
                : base(entityObject, rigidbodyController)
    {
        States = new Dictionary<Type, StateTrasition>();

        this.entityWayPoints = entityWayPoints;
    }

    public void Execute()
    {

        if (entityWayPoints.transforms.Length == 0)
            return;

        Transform wp = entityWayPoints.transforms[currentWaypointIndex];


        if (wp == null)
            return;

        Vector3 newWpPos = new Vector3(wp.position.x, EntityObject.Position.y, EntityObject.Position.z);
        float distanceToNext = Vector3.Distance(EntityObject.Transform.position, newWpPos);

        if (distanceToNext < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % entityWayPoints.transforms.Length;
        }
        else
        {

            if (EntityObject.IsGrounded == false)
                return;

            var direction = Mathf.Sign((newWpPos - EntityObject.Position).normalized.x);
            RigidbodyController.TargetVelocity = Vector3.right * (direction * EntityObject.Data.moveSpeed);
        }
    }

    public void Init()
    {
        RigidbodyController.VelocitySmooth = 0.01f;
    }

}


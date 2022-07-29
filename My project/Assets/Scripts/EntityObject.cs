using System;
using UnityEngine;

public class EntityObject 
{
    private readonly LayerMask groundLayerMask;
    private readonly LayerMask enemiesLayerMask;

    public Transform Transform
    {
        get; set;
    }

    public Vector3 Position
    {
        get => Transform.position;
    }

    public Collider2D Collider 
    {
        get; set;
    }

    public EntityData Data
    { 
        get; set;
    }

    public EntityObject(Collider2D boxCollider2D, EntityData entityData, LayerMask groundLayerMask, LayerMask enemiesLayerMask)
    {
        Collider = boxCollider2D;
        Data = entityData;

        Transform = Collider.transform;
        this.groundLayerMask = groundLayerMask;
        this.enemiesLayerMask = enemiesLayerMask;
    }

    public bool IsGrounded
    {
        get
        {
            float boundsOffset = 0.1f;
            var raycastHit = Physics2D.Raycast(Collider.bounds.center, Vector2.down, Collider.bounds.extents.y + boundsOffset, groundLayerMask);
            return raycastHit.collider != null;
        }
    }

    public Collider2D HitCollider
    {
        get
        {
            float boundsOffset = 1f;
            var raycastHit = Physics2D.Raycast(Collider.bounds.center, Transform.right, Collider.bounds.extents.x + boundsOffset, enemiesLayerMask);
            Debug.DrawRay(Collider.bounds.center, Transform.right * (Collider.bounds.extents.x + boundsOffset));
            return raycastHit.collider;
        }
    }

    public void FlipByVelocity(RigidbodySingal velocity)
    {
        if (velocity.x != 0.0f)
            Transform.rotation = Quaternion.Euler(new Vector3(0, Convert.ToInt32(velocity.x < 0.0f) * 180, 0));
    }

}

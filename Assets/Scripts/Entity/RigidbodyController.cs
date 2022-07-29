using UnityEngine;
using Zenject;

public class RigidbodySingal
{
    public bool grounded;
    public float x;
    public float y;
}

public class RigidbodyController : IFixedTickable
{
    public Rigidbody2D Rigidbody
    {
        get; set;
    }

    private readonly EntityObject entityObject;
    private readonly SignalBus signalBus;

    public Vector3 currentVelocity;

    public Vector3 TargetVelocity
    { 
        get; set; 
    }

    public float VelocitySmooth
    {
        get; set;
    }

    public float StunnedTime
    {
        get; set;
    }

    public RigidbodyController(Rigidbody2D rigidbody, EntityObject entityObject, SignalBus signalBus)
    {
        this.Rigidbody = rigidbody;
        this.entityObject = entityObject;
        this.signalBus = signalBus;
    }

    public void FixedTick()
    {
        if (StunnedTime > 0.0f)
        {
            StunnedTime -= Time.fixedDeltaTime;
            return;
        }

        Vector3 targetVelocity = new Vector3(TargetVelocity.x, Rigidbody.velocity.y, 0);
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, targetVelocity, ref currentVelocity, VelocitySmooth);

        if (entityObject.IsGrounded)
        {
            if (TargetVelocity.y > 0.0f && Rigidbody.velocity.y <= 0.0f)
            {
                AddForce(new Vector2(0f, entityObject.Data.jumpPower), 0.0f);
                return;
            }
        }

        var playerVelocitySignal = new RigidbodySingal()
        {
            x = TargetVelocity.x,
            y = Rigidbody.velocity.y,
            grounded = entityObject.IsGrounded
        };

        signalBus.Fire(playerVelocitySignal);
    }

    public void AddForce(Vector2 force, float stunnedTime)
    {
        Rigidbody.AddForce(force);
        StunnedTime = stunnedTime;
    }


}


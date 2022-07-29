using UnityEngine;
using Zenject;

public class MushroomCollision 
{
    public Transform mushroom;
    public float impactForce;
}

public class MushroomTrigger : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionLayer;

    [SerializeField]
    private float impactForce;

    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var isCorrectLayer = collisionLayer == (collisionLayer | (1 << collision.gameObject.layer));

        if (isCorrectLayer == false)
            return;

        var entityFacade = collision.gameObject.GetComponent<EntityFacade>();

        if (entityFacade == null)
            return;

        Vector2 force = -(transform.position - entityFacade.Position).normalized * impactForce;
        entityFacade.Impact(force);
        entityFacade.OnHit(1);
    }

}

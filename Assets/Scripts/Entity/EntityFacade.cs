using UnityEngine;
using Zenject;

public class EntityFacade : MonoBehaviour
{
    private RigidbodyController rigidbodyController;
    private EntityAnimator entityAnimator;
    private SignalBus signalBus;

    [Inject]
    public void Construct(
        RigidbodyController rigidbodyController,
        EntityAnimator entityAnimator,
        EntityStats entityStats,
        SignalBus signalBus)
    {
        this.rigidbodyController = rigidbodyController;
        this.entityAnimator = entityAnimator;
        this.signalBus = signalBus;

        Stats = entityStats;
    }

    public Vector3 Position => transform.position;

    public EntityStats Stats
    {
        get; set;
    }

    public void OnEnable()
    {
        signalBus.Subscribe<AddCoin>(AddCoint);
    }

    public void OnDisable()
    {
        signalBus.Unsubscribe<AddCoin>(AddCoint);
    }

    public void AddCoint() => Stats.AddCoint();
    public int Damaged(int value) => Stats.Damaged(GetInstanceID(), value);   

    public void OnHit(int value)
    {
        entityAnimator.Hit();
        int health = Damaged(value);

        if (health <= 0)
        {
            entityAnimator.Die();
            Destroy(gameObject, 0.5f);
        }
    }

    public void Impact(Vector2 force)
    {
        rigidbodyController.AddForce(force, 0.2f);
    }



}


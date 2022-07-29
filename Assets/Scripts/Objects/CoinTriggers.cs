using UnityEngine;
using Zenject;

public class CoinTriggers : MonoBehaviour
{
    [SerializeField]
    private LayerMask canPickLayer;

    [SerializeField]
    private int value;

    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isCorrectLayer = canPickLayer == (canPickLayer | (1 << collision.gameObject.layer));

        if (isCorrectLayer == false)
            return;

        for (int i = 0; i < value; i++)
        {
            signalBus.Fire<AddCoin>();
        }

        Destroy(gameObject);
    }
}

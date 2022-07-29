using TMPro;
using UnityEngine;
using Zenject;

public class UICoins : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cointsValueText;

    private SignalBus signalBus;
    private EntityFacade entityFacade;

    [Inject]
    public void Construct(SignalBus signalBus, EntityFacade entityFacade)
    {
        this.signalBus = signalBus;
        this.entityFacade = entityFacade;
        cointsValueText.text = entityFacade.Stats.data.Coints.ToString();
    }

    public void OnEnable()
    {
        signalBus.Subscribe<AddCoin>(AddCoin);
    }

    public void OnDisable()
    {
        signalBus.Unsubscribe<AddCoin>(AddCoin);
    }

    public void AddCoin()
    {
        cointsValueText.text = entityFacade.Stats.data.Coints.ToString();
    }
}


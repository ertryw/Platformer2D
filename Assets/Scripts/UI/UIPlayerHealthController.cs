using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private UIHeart uIHeart;

    [SerializeField]
    private Transform heartsPanel;

    private SignalBus signalBus;
    private List<UIHeart> hearts = new List<UIHeart>();
    private EntityFacade entityFacade;

    [Inject]
    public void Construct(SignalBus signalBus, EntityFacade entityFacade)
    {
        this.entityFacade = entityFacade;
        this.signalBus = signalBus;

        for (int i = 0; i < entityFacade.Stats.data.Health; i++)
        {
            var heartObject = Instantiate(uIHeart, heartsPanel);
            hearts.Add(heartObject);
        }
    }

    public void OnEnable()
    {
        signalBus.SubscribeId<LoseHeart>(entityFacade.GetInstanceID(), LoseHeart);
    }

    public void OnDisable()
    {
        signalBus.UnsubscribeId<LoseHeart>(entityFacade.GetInstanceID(), LoseHeart);
    }

    public void LoseHeart(LoseHeart loseHeart)
    {
        int last = hearts.Count - 1;

        if (last < 0)
            return;

        hearts[last].HeartLose();
        hearts.RemoveAt(last);
    }



}

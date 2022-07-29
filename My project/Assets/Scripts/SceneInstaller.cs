using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private EntityFacade playerFacade;

    public override void InstallBindings()
    {
        Debug.Log("Scene install");
        Container.BindInstance(playerFacade);

        Container.DeclareSignal<LoseHeart>().WithId(playerFacade.GetInstanceID());
        Container.DeclareSignal<AddCoin>();

        Container.DeclareSignal<MushroomCollision>();

        Container.Bind<InputController>()
            .AsSingle()
            .OnInstantiated(OnInputControllerNew)
            .IfNotBound();

        SignalBusInstaller.Install(Container);

        Debug.Log("Scene Installs");
    }

    private void OnInputControllerNew(InjectContext context, object inputController)
    {
        var ip = (InputController)inputController;
        ip.Enable();
    }


}

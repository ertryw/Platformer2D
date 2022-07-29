using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PlayerController>()
            .AsSingle()
            .NonLazy();

    }

}
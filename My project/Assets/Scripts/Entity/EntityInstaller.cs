using System;
using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private LayerMask enemyLayerMask;

    public override void InstallBindings()
    {
        Container.BindInstance(_settings.entityData);

        Container.Bind<EntityObject>()
            .AsSingle()
            .WithArguments(_settings.collider, groundLayerMask, enemyLayerMask);

        Container.Bind<EntityStats>()
            .AsSingle()
            .WithArguments(_settings.entityData);

        Container.BindInterfacesAndSelfTo<RigidbodyController>()
            .AsSingle()
            .WithArguments(_settings.rigidbody);

        Container.BindInterfacesTo<BehaviourController>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<EntityAnimator>()
            .AsSingle()
            .WithArguments(_settings.animator);

        Container.DeclareSignal<RigidbodySingal>();
        Container.DeclareSignal<AttackSignal>();

        Container.BindSignal<RigidbodySingal>()
           .ToMethod<EntityObject>(x => x.FlipByVelocity)
           .FromResolve();
    }

    [Serializable]
    public class Settings
    {
        public EntityData entityData;
        public Rigidbody2D rigidbody;
        public Collider2D collider;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
    }
}


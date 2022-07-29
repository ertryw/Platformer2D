using System;
using UnityEditor;
using UnityEngine;
using Zenject;


[Flags]
public enum EnemyBehaviours
{
    None =   0,
    Patrol = 1,   
    Chase =  2,    
    Attack = 4,    
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnemyBehaviours))]
public class EnemyBehavioursDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        property.intValue = (int)(EnemyBehaviours)EditorGUI.EnumFlagsField(position, label, (EnemyBehaviours)property.intValue);
        EditorGUI.EndProperty();
    }
}
#endif

public class EnemyInstaller : MonoInstaller
{
    [SerializeField]
    private  EnemyBehaviours Behaviours;

    [SerializeField]
    private EntityWayPoints entityWaypoints;

    public override void InstallBindings()
    {
        Container.BindInstance(entityWaypoints);

        if (Behaviours.HasFlag(EnemyBehaviours.Patrol))
        {
            Container.BindInterfacesTo<Patrol>()
                .AsTransient()
                .OnInstantiated<Patrol>(OnPatrolInstatiate);
        }

        if (Behaviours.HasFlag(EnemyBehaviours.Chase))
        {
            Container.BindInterfacesTo<Chase>()
            .AsTransient()
            .OnInstantiated<Chase>(OnChaseInstatiate);
        }

        if (Behaviours.HasFlag(EnemyBehaviours.Attack))
        {
            Container.BindInterfacesTo<Attack>()
            .AsTransient()
            .OnInstantiated<Attack>(OnAttackInstatiate);
        }
    }

    private void OnAttackInstatiate(InjectContext arg1, Attack attack)
    {
        attack.States.Add(typeof(Chase), new StateTrasition()
        {
            Condition = () => attack.EntityObject.HitCollider == null,
            TransitionDelay = 0.0f
        });
    }

    private void OnChaseInstatiate(InjectContext arg1, Chase chase)
    {
        chase.States.Add(typeof(Patrol), new StateTrasition()
        {
            Condition = () => chase.EntityObject.HitCollider == null,
            TransitionDelay = 3.0f
        });

        chase.States.Add(typeof(Attack), new StateTrasition()
        {
            Condition = () => chase.RigidbodyController.Rigidbody.velocity == Vector2.zero,
            TransitionDelay = 0.0f
        });
    }

    private void OnPatrolInstatiate(InjectContext arg1, Patrol patrol)
    {
        patrol.States.Add(typeof(Chase), new StateTrasition()
        {
            Condition = () => patrol.EntityObject.HitCollider != null,
            TransitionDelay = 0.0f
        });
    }



}


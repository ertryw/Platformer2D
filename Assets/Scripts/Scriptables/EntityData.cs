using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityScriptableObject", order = 1)]
public class EntityData : ScriptableObject
{
    public int health;
    public int coints;
    public int moveSpeed;
    public int jumpPower;
    [Space(10)]
    public float chaseDistance;
    public float attackDelay;
}

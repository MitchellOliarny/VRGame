using UnityEngine;

[CreateAssetMenu]
public class EnemyModifierSettings : ScriptableObject
{
    [Header("MODIFIERS")]
    public bool Reinforced;
    public bool Camo;
    public bool Rock;
    public bool Speed;
    public bool MagicRes;
    public bool ExploRes;
    public bool Flying;
}

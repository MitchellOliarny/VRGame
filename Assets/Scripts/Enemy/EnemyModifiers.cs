using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifiers : MonoBehaviour
{
    [Header("SCRIPTS")]
    [SerializeField] private EnemyModifierSettings mod;


    [Header("MODIFIERS")]
    [SerializeField] private bool Reinforced;
    [SerializeField] private bool Camo;
    [SerializeField] private bool Rock;
    [SerializeField] private bool Speed;
    [SerializeField] private bool MagicRes;
    [SerializeField] private bool ExploRes;


    private void Start()
    {
        Reinforced = mod.Reinforced;
        Camo = mod.Camo;
        Rock = mod.Rock;
        Speed = mod.Speed;
        MagicRes = mod.MagicRes;
        ExploRes = mod.ExploRes;
    }

    //-------------------------------------
    // GETTERS
    public bool GetReinforced => Reinforced;
    public bool GetCamo => Camo;
    public bool GetRock => Rock;
    public bool GetSpeed => Speed;
    public bool GetMagicRes => MagicRes;
    public bool GetExploRes => ExploRes;
    //-------------------------------------

    public void SetModifiers(EnemyModifierSettings mods)
    {
        mod = mods;
    }

    public void SetModifiers(bool b)
    {
        Reinforced = false;
        Camo = false;
        Rock = false;
        Speed = false;
        MagicRes = false;
        ExploRes = false;
    }


    //-- SLIME IN AIR MODIFIER --\\
    //DO NOT TOUCH
    [SerializeField] private bool inAir;
    public void Air() => inAir = true; // Set inAir True

    public void Ground() => inAir = false; // Set inAir False

    public bool GetInAir() => inAir; // Return inAir

}

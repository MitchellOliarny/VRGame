using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
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
    [SerializeField] private bool Flying;

    [Header("Objects")]
    [SerializeField] private GameObject flyingOBJ;
    [SerializeField] private Animator anim;
    private void Start()
    {
        Reinforced = mod.Reinforced;
        Camo = mod.Camo;
        Rock = mod.Rock;
        Speed = mod.Speed;
        MagicRes = mod.MagicRes;
        ExploRes = mod.ExploRes;
        Flying = mod.Flying;
    }

    //-------------------------------------
    // GETTERS
    public bool GetReinforced => Reinforced;
    public bool GetCamo => Camo;
    public bool GetRock => Rock;
    public bool GetSpeed => Speed;
    public bool GetMagicRes => MagicRes;
    public bool GetExploRes => ExploRes;
    public bool GetFlying => Flying;
    //-------------------------------------

    public void SetModifiers(EnemyModifierSettings mods)
    {
        mod = mods;
        GetComponentInParent<PathFollower>().UpdateModifier(mods);
        if (mods.Flying)
        {
            flyingOBJ.SetActive(true);
            anim.SetBool("Flying", true);
        }
        else
        {
            flyingOBJ.SetActive(false);
            anim.SetBool("Flying", false);
        }
    }


    //-- SLIME IN AIR MODIFIER --\\
    [SerializeField] private bool inAir;
    //DO NOT TOUCH
    public void Air() => inAir = true; // Set inAir True

    public void Ground() => inAir = false; // Set inAir False

    public bool GetInAir() => inAir; // Return inAir

}

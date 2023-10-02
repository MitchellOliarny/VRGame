using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Spinner : MonoBehaviour
{
    [Header("SCRIPTS")]
    [Tooltip("FOR SPINNER TYPE TOWER SET TARGET TO CLOSE")]
    [SerializeField] private TargetingScript target; // Targeting Script
    [SerializeField] private CallSeverScript callSever; // Severing Script

    [Header("INTS")]
    [SerializeField] private float damage; // Damage of Blades
    [SerializeField] private int InstanceID;

    [Header("GAME VALUES")]
    [SerializeField] private ParticleSystem hitEffect; // Hit effect
    [SerializeField] private BoxCollider col; // Collider
    [SerializeField] private float distance; // Range Distance

    [SerializeField] Animator anim; // Animator

    private void OnEnable()
    {
        EventManagerScript.OnBladeHit += OnBladeHit; // Subscribes to the OnBladeHit Event
    }

    private void OnDisable()
    {
        EventManagerScript.OnBladeHit -= OnBladeHit; // Unsubscribes from the OnBladeHit Event
    }

    private void Start()
    {
        // Sets Targeting Script to parent Targeting Script
        target = GetComponentInParent<TargetingScript>();
        InstanceID = gameObject.GetInstanceID(); // Gets the Instance ID from the Instantiated Game Object
    }

    void FixedUpdate()
    {
        // If targeting script is returning an enemy
        if (target.GetEnemyArrayLength() > 0)
            anim.SetBool("spin", true); // Spin the blades
        else
            anim.SetBool("spin", false);
    }

    //-- ON TRIGGER ENTER --\\
    private void OnBladeHit(int ID, int SpinID)
    {
        if (SpinID == InstanceID)
        {
            EventManagerScript.EnemyHit(ID, damage);
        }
    }

    //-- ON TRIGGER EXIT --\\
    private void OnTriggerExit(Collider other)
    {

    }

    public void SetDamage(float i) => damage *= i;
}

using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public virtual void DamageHealth(float damage, int pierce)
    {

    }

    public virtual void CheckDeath(int pierce)
    {
    }

    public virtual void PreSplit(Plane[] planes)
    {


    }

    public virtual void PostSplit(GameObject[] childEnemy)
    {
        

    }

    public virtual IEnumerator DespawnEnemy(GameObject g, float timer)
    {
        return null;
    }

    public virtual IEnumerator DespawnEnemy(GameObject g)
    {
        return null;
    }


    public virtual bool Killed()
    {
        return false;
    }
    public virtual float GetCurrentHealth()
    {
        return 0;
    }
    public virtual void SetCurrentHealth(float f)
    {

    }
    public virtual int GetDamageToPlayer => 0;
}

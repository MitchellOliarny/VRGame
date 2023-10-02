using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutManager : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private void Start()
    {
        StartCoroutine(LifeTimeCountdown(LifeTime));
    }

    public IEnumerator LifeTimeCountdown(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public float LifeTime
    {
        get { return lifeTime; }
        set { lifeTime = value; }
    } 
}

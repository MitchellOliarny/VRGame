using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererModifier : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position.Set(parentTransform.position.x, transform.position.y, transform.position.z);
    }
}

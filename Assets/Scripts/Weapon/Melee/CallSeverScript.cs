using UnityEngine;

public class CallSeverScript : MonoBehaviour
{
    [Header("Normalized Axis")]
    [Space]
    [Tooltip("USES BLUE AXIS TO NORMALIZE CUTTING PLANE")]
    [SerializeField] private bool FORWARD;
    [Tooltip("USES GREEN AXIS TO NORMALIZE CUTTING PLANE")]
    [SerializeField] private bool UP;
    [Tooltip("USES RED AXIS TO NORMALIZE CUTTING PLANE")]
    [SerializeField] private bool RIGHT;

    private Vector3 NormalizeAxis;
    private void Start()
    {
        if (FORWARD) NormalizeAxis = transform.forward; // Sets NormalizeAxis to the Forward of attached object
        else if (UP) NormalizeAxis = transform.up; // Sets NormalizeAxis to the Up of attached object
        else NormalizeAxis = transform.right; // Sets NormalizeAxis to the Right of attached object
    }

    //-- Sends Message to hit object to cut in half --//
    // Collider other is hit object
    // Vector3 hitPoint is where the player hit the target
    // NormalizeAxis is the normal of the plane
    public void CallSliceMethod(Collider other, Vector3 hitPoint)
    {

        Plane severingPlane;

        if (FORWARD) { severingPlane = new Plane(transform.forward, hitPoint); } // Sets NormalizeAxis to the Forward of attached object
        else if (UP) { severingPlane = new Plane(transform.up, hitPoint); } // Sets NormalizeAxis to the Up of attached object
        else { severingPlane = new Plane(transform.right, hitPoint); } // Sets NormalizeAxis to the Right of attached object

        other.SendMessage("Sever", new Plane[] { severingPlane }, SendMessageOptions.DontRequireReceiver);
    }
}

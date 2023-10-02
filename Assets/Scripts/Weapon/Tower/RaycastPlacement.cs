using UnityEngine;

public class RaycastPlacement : MonoBehaviour
{
    private RaycastHit hit; // Ray Hit Info
    [SerializeField] private Transform parentTransform; // Object's Parent Transform
    [SerializeField] private Transform raycastTransform; // Transform where Raycast will come out of

    private void FixedUpdate()
    {
        // Creates a raycast at raycastTransform which emits downwards, outputs the hit info
        if (Physics.Raycast(raycastTransform.position, raycastTransform.TransformDirection(Vector3.down), out hit))
        {
            // If the Raycast collides with the ground tag
            if (hit.collider.tag == "ground")
            {
                // Sets the Game Object to the position the raycast hit
                parentTransform.position = hit.point; 

                // If the Game Object is in the position the raycast hit, destroy this script to remove any unneeded code running
                if (parentTransform.position == hit.point) { Destroy(this); }
            }

        }
    }
}

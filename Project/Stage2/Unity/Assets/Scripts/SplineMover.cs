using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;  // Add this if using float3

public class SplineMover : MonoBehaviour
{
    public SplineContainer splineContainer; // Assign the Spline Container here
    public Transform objectToMove;          // The object to move along the spline
    public float moveSpeed = 1f;            // Speed of movement
       
    private float t = 0f;                   // Normalized time (0 to 1)

    void Update()
    {
        if (splineContainer != null && objectToMove != null)
        {
            // Move the object along the spline
            t += Time.deltaTime * moveSpeed / splineContainer.CalculateLength();
            t = Mathf.Clamp01(t); // Keep t within [0, 1]

            // Get the position on the spline at normalized time t
            Vector3 position = splineContainer.EvaluatePosition(t);
            objectToMove.position = position;

            // Optional: Rotate the object to face the direction of travel
            if (t < 1f)
            {
                // Fixing the tangent direction
                Vector3 tangent = splineContainer.EvaluateTangent(t); 
                objectToMove.rotation = Quaternion.LookRotation(tangent, Vector3.up);
            }
        }
    }
}

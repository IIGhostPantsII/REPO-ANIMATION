using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SplineMover : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float moveSpeed = 1f;
       
    private float t = 0f;

    void OnEnable()
    {
        t = 0f;
    }

    void Update()
    {
        if(splineContainer != null && objectToMove != null)
        {
            t += Time.deltaTime * moveSpeed / splineContainer.CalculateLength();
            t = Mathf.Clamp01(t);

            Vector3 position = splineContainer.EvaluatePosition(t);
            objectToMove.position = position;

            if(t < 1f)
            {
                Vector3 tangent = splineContainer.EvaluateTangent(t); 
                Quaternion lookRotation = Quaternion.LookRotation(tangent, Vector3.down);
                Quaternion sillyRotation = Quaternion.Euler(90f, 0f, 180f);
                objectToMove.rotation = lookRotation * sillyRotation;
            }
        }
    }
}

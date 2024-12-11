using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField] private GameObject _ripple;

    private GameObject rippleInstantiate;
    private ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        // Check if it's the plane (or any specific object) you're targeting
        if (other.CompareTag("WaterPlane")) // Replace with the actual tag of your plane
        {
            // Get the particle system's collision events
            ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[particleSystem.particleCount];
            int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

            for(int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 hitPosition = collisionEvents[i].intersection; // Get the intersection point (particle position on collision)
                rippleInstantiate = Instantiate(_ripple, hitPosition, Quaternion.identity);
                StartCoroutine(WaitForDestroy(rippleInstantiate));
            }
        }
    }

    public IEnumerator WaitForDestroy(GameObject ripple)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ripple);
    }
}

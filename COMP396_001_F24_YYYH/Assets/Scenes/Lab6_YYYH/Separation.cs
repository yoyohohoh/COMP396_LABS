using Unity.VisualScripting;
using UnityEngine;

public class Separation : FlockBehavior
{
    public float separationStrength = 1.0f;

    public override Vector3 GetForce()
    {
        // Check for nearby entities and calculate a force vector away from them
        float minDistance = 0.1f;
        Vector3 separationForce = new Vector3(0f, 0f, 0f);
        foreach (Vector3 neighbor in neighbors)

            if (neighbor != Vector3.zero && Vector3.Distance(transform.position, neighbor) <
            minDistance)

                separationForce += (transform.position - neighbor).normalized *
                separationStrength;

        return separationForce;
    }

    public override void Update(Vector3 position)
    {
        // Find nearby entities within a certain radius
        float radius = 10f;

        int count = 0;
        for (int i = 0; i < maxNeighbors; i++) {

            Vector3 neighborPosition = position + Random.onUnitSphere * radius;

            Flock entity = GameObject.Find("FlockEntity").GetComponent<Flock>();

            if (entity != null && Vector3.Distance(position, neighborPosition) <= radius)

                neighbors[count++] = neighborPosition;

            if (count == maxNeighbors) { }
                break;
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Alignment : FlockBehavior
{
    public float alignmentStrength = 1.0f;

    public override Vector3 GetForce()
    {
        // Calculate the average velocity of all neighbors
        Vector3 sum = Vector3.zero;

        for (int i = 0; i < maxNeighbors; i++)
        {
            if (neighbors[i] != Vector3.zero)
            {

                Flock entity = GameObject.Find("FlockEntity").GetComponent<Flock>();
                Vector3 velocity = entity.GetComponent<Rigidbody>().velocity;
                sum += velocity;
            }
        }
        //float averageX = sumX / maxNeighbors;
        //float averageY = sumY / maxNeighbors;
        //float averageZ = sumZ / maxNeighbors;
        //Vector3 averageVelocity = new Vector3(averageX, averageY, averageZ);
        Vector3 averageVelocity = sum / maxNeighbors; //new Vector3(averageX, averageY,averageZ);

        // Return a force vector pointing in the direction of the average velocity
        return (averageVelocity - transform.position).normalized * alignmentStrength;
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

if (count == maxNeighbors) break;
            

}
}}

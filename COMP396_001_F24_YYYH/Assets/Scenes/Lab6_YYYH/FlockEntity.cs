using UnityEngine;

public class FlockEntity : MonoBehaviour
{
    public int maxNeighbors = 5;
    public float cohesionStrength = 1.0f;
    public float separationStrength = 100.0f;
    public float alignmentStrength = 1.0f;

    private FlockBehavior cohesion;
    private FlockBehavior separation;
    private FlockBehavior alignment;

    void Start()
    {
        cohesion = new Cohesion();
        separation = new Separation();
        alignment = new Alignment();

        // Initialize the flock's position and velocity
        transform.position = Random.insideUnitSphere * 10f;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        // Update each flock behavior
        cohesion.Updates(transform.position);
        separation.Updates(transform.position);
        alignment.Updates(transform.position);

        // Combine the forces to get a new velocity
        Vector3 newVelocity = cohesion.GetForce() + separation.GetForce() + alignment.GetForce();
        newVelocity.Normalize();

        // Apply the force to the entity's Rigidbody
        GetComponent<Rigidbody>().AddForce(newVelocity * 10f);
    }

    void Update()
    {
        // Keep the flock within a certain radius
        if (Vector3.Distance(transform.position, transform.parent.position) > 20f)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 1f);
        }
    }
}

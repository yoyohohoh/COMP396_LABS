using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float distance = 5f; // Total distance to move
    public float speed = 3f;   // Movement speed

    private Vector3 pointA; // Calculated point A
    private Vector3 pointB; // Calculated point B
    private Vector3 targetPoint; // Current target point

    void Start()
    {
        // Dynamically calculate PointA and PointB based on the object's initial position
        pointA = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - distance / 2);
        pointB = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + distance / 2);

        // Set the initial target point
        targetPoint = pointB;
    }

    void Update()
    {
        // Move the object towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        // Switch the target point when the current one is reached
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }
}

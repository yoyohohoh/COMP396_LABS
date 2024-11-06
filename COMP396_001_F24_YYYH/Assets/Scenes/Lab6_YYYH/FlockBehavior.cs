using UnityEngine;

public abstract class FlockBehavior : MonoBehaviour
{
    protected int maxNeighbors;
    protected Vector3[] neighbors;

    public void Initialize(int maxNeighbors)
    {
        this.maxNeighbors = maxNeighbors;
        neighbors = new Vector3[maxNeighbors];
    }

    public abstract Vector3 GetForce(); // Returns a vector representing the force of this behavior
    public abstract void Update(Vector3 position); // Updates the neighbors array and calculates the force for this behavior
}

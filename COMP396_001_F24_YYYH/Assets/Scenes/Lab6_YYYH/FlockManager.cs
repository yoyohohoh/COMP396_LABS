using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject flockEntityPrefab;
    public int numberOfFlockEntities = 20;

    void Start()
    {
        for (int i = 0; i < numberOfFlockEntities; i++)
        {
            Instantiate(flockEntityPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}

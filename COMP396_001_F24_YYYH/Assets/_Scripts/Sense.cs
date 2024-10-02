using UnityEngine;
public class Sense : MonoBehaviour
{
    public bool bDebug = true;
    public Aspect.Affiliation targetAffiliation = Aspect.Affiliation.Enemy;
    public float detectionRate = 1.0f;
    protected float elapsedTime = 0.0f;
    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }
    void Start()
    {
        Initialize();
    }
    void Update()
    {
        UpdateSense();

    }
    
}
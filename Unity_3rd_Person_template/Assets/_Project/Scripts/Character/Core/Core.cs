using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public CollisionSystem CollisionSystem { get; private set; }
    public AbilitySystem AbilitySystem { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSystem = GetComponentInChildren<CollisionSystem>();
        AbilitySystem = GetComponentInChildren<AbilitySystem>();

        Animator = GetComponentInParent<Animator>();

        if(!Movement || !CollisionSystem || !AbilitySystem)
        {
            Debug.LogError("Missing Core Components");
        }

        if (!Animator)
        {
            Debug.LogError("Missing Animator Component");
        }
    }

    public void StartMethod()
    {

    }

    public void LogicUpdate()
    {

    }
}

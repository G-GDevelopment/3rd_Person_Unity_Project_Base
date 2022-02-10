using UnityEngine;

public class CoreComponents : MonoBehaviour
{
    protected Core core;

    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();

        if(core == null) { Debug.LogError("Missing Core on Parent Object! I miss my daddy"); }
    }
}

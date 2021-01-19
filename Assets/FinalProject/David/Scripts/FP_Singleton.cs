using UnityEngine;

public abstract class FP_Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    static T instance = default(T);

    [SerializeField, Header("Visual Debug Height"), Range(-100, 100)] float visualDebugHeight = 0;
    [SerializeField, Header("Visual Debug Size"), Range(0, 100)] float visualDebugSize = 0.3f;
    [SerializeField, Header("Visual Debug Color")] Color validColor = Color.green;
    public static T Instance => instance;

    public virtual bool IsValid => instance;
    public virtual void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this as T;
        instance.name += $"[{instance.GetType().Name} MANAGER]";
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = IsValid ? validColor : Color.gray;
        Vector3 _upPosition = transform.position + Vector3.up * visualDebugHeight;
        Gizmos.DrawLine(transform.position, _upPosition);
        Gizmos.DrawSphere(_upPosition, visualDebugSize);
    }
}

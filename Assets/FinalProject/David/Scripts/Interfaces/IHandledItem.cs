using UnityEngine;

public interface IHandledItem<Tkey>
{
    Tkey ID { get; }

    void Enable();
    void Disable();
    void InitHandledItem();
    void RemoveHandledItem();
}

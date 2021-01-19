using UnityEngine;

public interface IEffects 
{
    float DurationFx { get; }
    GameObject ShootFX { get; }
    GameObject ShootHitFX { get; }
    void InstantiateFX(GameObject _fx, Vector3 _position, float _duration);
    void InstantiateFX(GameObject _fx, Vector3 _position, string _audioressources, float _duration);
}

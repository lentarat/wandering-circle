using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject _movePositionsGiveableGameObject;
    [SerializeField] private ParticleSystem _bubblesParticleSystem;

    private IMovePositionGiveable _movePositionGiveable;

    private void Awake()
    {
        _movePositionGiveable = _movePositionsGiveableGameObject.GetComponent<IMovePositionGiveable>();
        _movePositionGiveable.OnMovePositionGiven += HandlePositionGiven;          
    }

    private void HandlePositionGiven(Vector2 movePosition)
    {
        Vector3 movePositionVector3 = movePosition; 
        Instantiate(_bubblesParticleSystem, movePositionVector3, Quaternion.identity, transform);
    }

    private void OnDestroy()
    {
        _movePositionGiveable.OnMovePositionGiven -= HandlePositionGiven;
    }
}

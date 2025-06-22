using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionGiveableListener : MonoBehaviour
{
    [SerializeField] private GameObject _movePositionsGiveableGameObject;

    private IMovePositionGiveable _movePositionGiveable;

    private void Awake()
    {
        _movePositionGiveable = _movePositionsGiveableGameObject.GetComponent<IMovePositionGiveable>();
        _movePositionGiveable.OnMovePositionGiven += HandleMovePositionGiven;
    }

    private void HandleMovePositionGiven(Vector2 movePosition)
    {
        SFXManager.instance.PlayBubbles();
    }

    private void OnDestroy()
    {
        _movePositionGiveable.OnMovePositionGiven -= HandleMovePositionGiven;
    }
}

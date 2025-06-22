using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleMovement : MonoBehaviour
{
    [SerializeField] private GameObject _movePositionsGiveableGameObject;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _speedPower;

    private IMovePositionGiveable _movePositionGiveable;
    private CommandManager _commandManager = new ();
    private float _currentSpeedValue;
    public float CurrentSpeedValue => _currentSpeedValue;

    public Action<float>OnSpeedValueChanged;

    private void Awake()
    {
        _movePositionGiveable = _movePositionsGiveableGameObject.GetComponent<IMovePositionGiveable>();
        _movePositionGiveable.OnMovePositionGiven += HandleMovePositionGiven;

        SpeedSlider.OnValueChanged += HandleSpeedSliderValueChanged;
    }

    private void HandleSpeedSliderValueChanged(float newSpeed)
    {
        _currentSpeedValue = Mathf.Pow(newSpeed * _speedMultiplier, _speedPower);
        OnSpeedValueChanged?.Invoke(_currentSpeedValue);
    }

    private void OnDestroy()
    {
        _movePositionGiveable.OnMovePositionGiven -= HandleMovePositionGiven;
    }

    private void HandleMovePositionGiven(Vector2 movePosition)
    {
        ICommand command = new MoveCircleCommand(this, movePosition);
        _commandManager.EnqueueCommand(command);
    }
}

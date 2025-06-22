using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircleCommand : ICommand
{
    private float _lastSpeed;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Tween _currentTween;
    private CircleMovement _circleMovement;

    public MoveCircleCommand(CircleMovement circleMovement, Vector3 endPosition)
    {
        _startPosition = circleMovement.transform.position;
        _endPosition = endPosition;
        _circleMovement = circleMovement;
    }

    void ICommand.Execute(Action onEnd)
    {
        Move(onEnd);
        MonitorSpeed();
    }
    
    private void Move(Action onEnd)
    {
        _lastSpeed = _circleMovement.CurrentSpeedValue;
        float time = Vector3.Distance(_startPosition, _endPosition) / _lastSpeed;
        _currentTween = _circleMovement.transform.DOMove(_endPosition, time)
            .OnComplete(() =>
        {
            onEnd?.Invoke();
            _circleMovement.OnSpeedValueChanged -= ChangeSpeed;
        });
    }

    private void MonitorSpeed()
    {
        _circleMovement.OnSpeedValueChanged += ChangeSpeed;
    }

    private void ChangeSpeed(float newSpeed)
    {
        _currentTween.timeScale *= newSpeed / _lastSpeed;
        _lastSpeed = newSpeed;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchesReader : MonoBehaviour, IMovePositionGiveable                                                     
{
    private Dictionary<int, Vector2> _touchIdToPositionDictionary = new ();
    private event Action<Vector2> _onMovePositionGiven;

    event Action<Vector2> IMovePositionGiveable.OnMovePositionGiven
    {
        add
        {
            _onMovePositionGiven += value;
        }

        remove
        {
            _onMovePositionGiven -= value;
        }
    }

    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject() == false)
            CheckForTouches();
    }

    private void CheckForTouches()
    {
        AddPressedTouchesToDictionaryIfExist();
        Vector2[] releasedScreenPositions = GetReleasedScreenTouchesPositionsIfExist();
        if (releasedScreenPositions.Length > 0)
        {
            Vector2[] releasedWorldPositions = GetTransformedPositionsToWorldSpace(releasedScreenPositions);
            SendPositions(releasedWorldPositions);
        }
    }

    private void AddPressedTouchesToDictionaryIfExist()
    {
        foreach (var touch in Touchscreen.current.touches)
        {
            int id = touch.touchId.ReadValue();
            if (touch.isInProgress && _touchIdToPositionDictionary.ContainsKey(id) == false)
            {
                _touchIdToPositionDictionary[id] = touch.position.ReadValue();
            }
        }
    }

    private Vector2[] GetReleasedScreenTouchesPositionsIfExist()
    {
        List<int> releaseTouchesIdsList = new();
        List<Vector2> releaseTouchesPositionsList = new();
        foreach (int id in _touchIdToPositionDictionary.Keys)
        {
            var isTouchFound = Touchscreen.current.touches.FirstOrDefault(touch =>
             touch.touchId.ReadValue() == id);

            if (isTouchFound != null && isTouchFound.isInProgress == false)
            {
                releaseTouchesIdsList.Add(id);
            }
        }

        for (int i = 0; i < releaseTouchesIdsList.Count; i++)
        {
            int releaseId = releaseTouchesIdsList[i];
            Vector2 releasePosition = _touchIdToPositionDictionary[releaseId];
            releaseTouchesPositionsList.Add(releasePosition);
            _touchIdToPositionDictionary.Remove(releaseId);
        }

        return releaseTouchesPositionsList.ToArray();
    }

    private Vector2[] GetTransformedPositionsToWorldSpace(Vector2[] screenPositions)
    {
        Vector2[] worldPositions = new Vector2[screenPositions.Length];
        for(int i = 0; i < screenPositions.Length; i++)
        {
            worldPositions[i] = Camera.main.ScreenToWorldPoint(screenPositions[i]);
        }

        return worldPositions;
    }

    private void SendPositions(Vector2[] positions)
    {
        foreach (Vector2 position in positions)
        {
            _onMovePositionGiven?.Invoke(position);
        }
    }
}

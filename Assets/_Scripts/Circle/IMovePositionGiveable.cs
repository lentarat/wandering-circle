using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovePositionGiveable
{
    event Action<Vector2> OnMovePositionGiven;
}

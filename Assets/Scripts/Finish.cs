using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finish : SnakeCollision
{
    protected override void Collision(Snake snake)
    {
        Events.OnGameWin.Invoke();
    }
}

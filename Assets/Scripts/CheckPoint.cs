using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : SnakeCollision
{
    [SerializeField] private ColorId colorId;

    protected override void Collision(Snake snake)
    {
        snake.SetNewColor(colorId);
    }
}

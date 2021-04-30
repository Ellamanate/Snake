using System.Collections;
using UnityEngine;


public abstract class SnakeCollision : MonoBehaviour
{
    protected virtual void Collision(Snake snake) { }

    private void OnTriggerEnter(Collider other)
    {
        Snake snake = other.GetComponent<Snake>();

        if (snake != null)
        {
            Collision(snake);
        }
    }
}
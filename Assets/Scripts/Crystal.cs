using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : SnakeCollision, IRestartable
{
    protected override void Collision(Snake snake)
    {
        Events.OnCollectCrystal.Invoke();

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        Restarter.Instance.Register(this);
    }

    public void Restart()
    {
        gameObject.SetActive(true);
    }
}

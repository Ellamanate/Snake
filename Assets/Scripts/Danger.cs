using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour, IRestartable
{
    private void Awake()
    {
        Restarter.Instance.Register(this);
    }

    public void Restart()
    {
        gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Food : MonoBehaviour, IRestartable
{
    private ObjectColor objectColor;
    private new Renderer renderer;
    private Vector3 startPosition;

    public ObjectColor ObjectColor => objectColor;

    private void Awake()
    {
        Restarter.Instance.Register(this);

        startPosition = transform.position;
    }

    public void Restart()
    {
        gameObject.SetActive(true);

        StopAllCoroutines();

        transform.position = startPosition;
    }

    public void Colorize(ObjectColor objectColor)
    {
        renderer = GetComponent<Renderer>();

        this.objectColor = objectColor;
        renderer.material.color = objectColor.Color;
    }
}

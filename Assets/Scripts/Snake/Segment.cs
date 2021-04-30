using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Segment : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float startSpeed = 8;
    [SerializeField] private int pointsLimit = 2;
    private Vector3 point;
    private float speed;
    private Vector3 startPosition;
    private Transform segmentAhead;
    private new Renderer renderer;

    public void Restart()
    {
        speed = startSpeed;
        transform.position = startPosition;
    }

    public void Colorize(Color color)
    {
        renderer.material.color = color;
    }

    public void UpdateSpeed(float speedCoef)
    {
        if (speedCoef >= 0)
            speed = startSpeed * speedCoef;
    }

    public void SetSegmentAhead(Transform segment)
    {
        segmentAhead = segment;
    }

    public void SetPoint(Vector3 point)
    {
        if (point.z >= transform.position.z)
        {
            this.point = point;
        }
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();

        startPosition = transform.position;
        speed = startSpeed;
    }

    private void Update()
    {
        float distance = Mathf.Pow(Vector3.Distance(transform.position, point), 2);

        transform.position = Vector3.MoveTowards(transform.position, point, distance * speed * Time.deltaTime);

        CheckEndPoint();
    }

    private void CheckEndPoint()
    {
        if (transform.position.z > point.z)
        {
            point = segmentAhead.position;
        }
    }
}

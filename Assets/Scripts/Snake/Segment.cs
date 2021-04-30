using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Segment : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float startSpeed = 8;
    [SerializeField] private int pointsLimit = 2;
    [SerializeField] private float minDistance = 1;
    [SerializeField] private float minStep = 0.1f;
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    [SerializeField] private float speed;
    private Vector3 startPosition;
    private Transform segmentAhead;
    private new Renderer renderer;

    public void Restart()
    {
        points.Clear();

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

    public void AddPoint(Vector3 point)
    {
        if (point.z >= transform.position.z)
        {
            points.Add(point);
        }

        if (points.Count > pointsLimit)
        {
            points.RemoveAt(0);
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
        if (points.Count > 0)
        {
            float distance = Mathf.Pow(Vector3.Distance(transform.position, points.First()), 3);

            if(Vector3.Distance(transform.position, segmentAhead.position) > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, points.First(), distance * speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, points.First().ChangeZ(transform.position.z + minStep), distance * speed * Time.deltaTime);
            }

            CheckEndPoint();
        }
    }

    private void CheckEndPoint()
    {
        if (transform.position == points.First())
        {
            points.RemoveAt(0);
        }
    }
}

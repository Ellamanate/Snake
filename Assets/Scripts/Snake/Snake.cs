using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(SnakeMove))]
[RequireComponent(typeof(SnakeMouth))]
public class Snake : MonoBehaviour, IRestartable
{
    [SerializeField] private Segment segmentPrefab;
    [SerializeField] [Range(1, 100)] private int pointsToGrow = 10;
    [SerializeField] private ColorId startColorId;
    [SerializeField] private ColorScheme colorScheme;
    [SerializeField] private List<Segment> segments = new List<Segment>();
    private new Renderer renderer;
    private SnakeMove snakeMove;
    private SnakeMouth snakeSensors;
    private Segment[] startSegments;
    private ObjectColor objectColor;
    private int points;

    public IReadOnlyCollection<Segment> Segments => segments.AsReadOnly();
    public ObjectColor ObjectColor => objectColor;

    public void Restart()
    {
        points = 0;

        DropSegments();

        snakeMove.StartMove();

        foreach (Segment segment in segments)
        {
            segment.Restart();
        }

        objectColor = colorScheme.GetObjectColorsById(new ColorId[1] { startColorId })[0];
        Colorize();
    }

    public void StartFever()
    {
        snakeMove.StartFever();
        snakeSensors.IsFever = true;
    }

    public void StopFever()
    {
        snakeMove.StopFever();
        snakeSensors.IsFever = false;
    }

    public void SetNewColor(ColorId newColorId)
    {
        objectColor = colorScheme.GetObjectColorsById(new ColorId[1] { newColorId })[0];

        Colorize();
    }

    public void ControlSegmentsSpeed(float speedCoef)
    {
        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].UpdateSpeed(speedCoef);
        }
    }

    public void AddPointsToSegments()
    {
        segments.First().AddPoint(transform.position);

        for (int i = 1; i < segments.Count; i++)
        {
            segments[i].AddPoint(segments[i - 1].transform.position);
        }
    }

    public void EndGame()
    {
        snakeMove.StopMove();

        Events.OnEndGame.Invoke();
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        snakeMove = GetComponent<SnakeMove>();
        snakeSensors = GetComponent<SnakeMouth>();

        Restarter.Instance.Register(this);

        objectColor = colorScheme.GetObjectColorsById(new ColorId[1] { startColorId })[0];

        segments.First().SetSegmentAhead(transform);

        for (int i = 1; i < segments.Count; i++)
        {
            segments[i].SetSegmentAhead(segments[i - 1].transform);
        }

        startSegments = segments.ToArray();
    }

    private void Start()
    {
        Colorize();
    }

    private void OnEnable()
    {
        Events.OnEat.AddListener(TryGrow);
    }

    private void OnDisable()
    {
        Events.OnEat.RemoveListener(TryGrow);
    }

    private void TryGrow()
    {
        points++;

        if (points % pointsToGrow == 0)
        {
            points -= pointsToGrow;

            Segment segment = Instantiate(segmentPrefab, transform.position.ChangeZ(-segments.Count - 1), Quaternion.identity, transform.parent);

            segment.SetSegmentAhead(segments[segments.Count - 1].transform);
            segment.Colorize(objectColor.Color);

            segments.Add(segment);
        }
    }

    private void Colorize()
    {
        renderer.material.color = objectColor.Color;

        foreach (Segment segment in segments)
        {
            segment.Colorize(objectColor.Color);
        }
    }

    private void DropSegments()
    { 
        foreach (Segment segment in segments)
        {
            if (!startSegments.Contains(segment))
            {
                Destroy(segment.gameObject);
            }
        }

        segments = startSegments.ToList();
    }
}
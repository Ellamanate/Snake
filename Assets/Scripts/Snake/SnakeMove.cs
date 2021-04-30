using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Snake))]
[RequireComponent(typeof(CharacterController))]
public class SnakeMove : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] private float speed = 5;
    [SerializeField] [Range(1, 10)] private float feverSpeedUp = 3;
    [SerializeField] [Range(0.01f, 1)] private float trackDelay = 0.01f;
    [SerializeField] private Vector2 RatioXZ = new Vector2(3, 1);
    private CharacterController body;
    private Snake snake;
    private Vector3 force;
    private bool isGame = true;
    private bool Isfever = false;

    public void StartFever()
    {
        Isfever = true;
        SpeedControl(feverSpeedUp);
    }

    public void StopFever()
    {
        Isfever = false;
        SpeedControl(1);
    }

    public void StopMove()
    {
        isGame = false;
        body.enabled = false;
    }

    public void StartMove()
    {
        isGame = true;
        body.enabled = true;
    }

    private void Awake()
    {
        snake = GetComponent<Snake>();
        body = GetComponent<CharacterController>();

        StartCoroutine(Tracker());
    }

    private void Update()
    {
        if (isGame)
        {
            if (!Isfever) 
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                force = new Vector3(horizontalInput * RatioXZ.x, 0, RatioXZ.y);

                SpeedControl(force.magnitude / 2);

                body.Move(force * speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position.ChangeX(0) + new Vector3(0, 0, RatioXZ.y), speed * feverSpeedUp * Time.deltaTime);
            }
        }
    }

    private void SpeedControl(float speedCoef)
    {
        snake.ControlSegmentsSpeed(speedCoef);
    }

    private IEnumerator Tracker()
    {
        while (true)
        {
            if (isGame)
            {
                snake.AddPointsToSegments();
            }

            yield return new WaitForSeconds(trackDelay);
        }
    }
}

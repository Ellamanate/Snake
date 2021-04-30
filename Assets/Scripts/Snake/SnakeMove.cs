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
        SpeedControl(feverSpeedUp / 2);
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
    }

    private void Update()
    {
        if (isGame)
        {
            snake.SetPointsToSegments();

            if (!Isfever) 
            {
                float horizontalInput = 0;

                if (Input.GetMouseButton(0))
                {
                    horizontalInput = Input.mousePosition.x > Screen.width / 2 ? 1 : Input.mousePosition.x < Screen.width / 2 ? -1 : 0;
                }

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
}

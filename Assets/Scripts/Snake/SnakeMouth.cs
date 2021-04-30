using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Snake))]
public class SnakeMouth : MonoBehaviour
{
    [HideInInspector] public bool IsFever = false;
    private Snake snake;

    private void Awake()
    {
        snake = GetComponent<Snake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Danger danger = other.GetComponent<Danger>();

        if (danger != null)
        {
            if (!IsFever)
            {
                snake.EndGame();

                return;
            }
            else
            {
                Eat(danger.transform);
            }
        }

        Food food = other.GetComponent<Food>();

        if (food != null && food.gameObject.activeSelf)
        {
            if (food.ObjectColor.ColorId == snake.ObjectColor.ColorId || IsFever)
            {
                Eat(food.transform);
            }
            else if (!IsFever)
            {
                snake.EndGame();
            }
        }
    }

    private void Eat(Transform food)
    {
        StartCoroutine(StartAttraction(food.transform));
    }

    private IEnumerator StartAttraction(Transform food)
    {
        while (food.position.z > transform.position.z)
        {
            food.position = Vector3.MoveTowards(food.position, transform.position, 10 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        Events.OnEat.Invoke();

        food.gameObject.SetActive(false);
    }
}
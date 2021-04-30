using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] UserInterface userInterface;
    [SerializeField] Snake snake;
    [SerializeField] private float crystalTargetTime = 2;
    [SerializeField] private float feverTime = 5;
    private int foodCounter;
    private int crystalCounter;
    private float crystalTimer;
    private int crystalSequence;
    private bool isFever = false;

    public void Restart()
    {
        Events.OnRestart.Invoke();

        foodCounter = crystalCounter = 0;
        crystalTimer = crystalSequence = 0;

        userInterface.ClosePausePanel();

        StopAllCoroutines();
        snake.StopFever();

        Awake();
    }

    private void Awake()
    {
        DropUI();

        StartCoroutine(CrystalTimer());
    }

    private void OnEnable()
    {
        Events.OnEat.AddListener(AddFood);
        Events.OnCollectCrystal.AddListener(AddCrystal);
        Events.OnEndGame.AddListener(OnEndGame);
        Events.OnGameWin.AddListener(OnGameWin);
    }

    private void OnDisable()
    {
        Events.OnEat.RemoveListener(AddFood);
        Events.OnCollectCrystal.RemoveListener(AddCrystal);
        Events.OnEndGame.RemoveListener(OnEndGame);
        Events.OnGameWin.RemoveListener(OnGameWin);
    }

    private void DropUI()
    {
        userInterface.UpdateFood(foodCounter.ToString());
        userInterface.UpdateCrystal(crystalCounter.ToString());
        userInterface.UpdateTitle("GAME OVER!");
    }

    private void AddFood()
    {
        foodCounter++;
        userInterface.UpdateFood(foodCounter.ToString());
    }

    private void AddCrystal()
    {
        crystalCounter++;
        userInterface.UpdateCrystal(crystalCounter.ToString());

        if (!isFever)
        {
            crystalTimer = 0;
            crystalSequence++;
        }
    }

    private void Fever()
    {
        StartCoroutine(StartFever());
    }

    private IEnumerator StartFever()
    {
        isFever = true;
        snake.StartFever();

        yield return new WaitForSeconds(feverTime);

        isFever = false;
        snake.StopFever();
    }

    private IEnumerator CrystalTimer()
    {
        while (true)
        {
            crystalTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);

            if (!isFever)
            {
                if (crystalSequence == 3)
                {
                    Fever();

                    crystalSequence = 0;
                    crystalTimer = 0;
                }

                if (crystalTimer >= crystalTargetTime)
                {
                    crystalSequence = 0;
                }
            }
        }
    }

    private void OnGameWin()
    {
        userInterface.UpdateTitle("WIN!");
        userInterface.OpenPausePanel();
    }

    private void OnEndGame()
    {
        userInterface.OpenPausePanel();
    }
}
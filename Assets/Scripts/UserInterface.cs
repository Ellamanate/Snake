using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInterface : MonoBehaviour
{
    [SerializeField] private RectTransform pausePanel;
    [SerializeField] private Text title;
    [SerializeField] private Text crystals;
    [SerializeField] private Text food;

    public void UpdateTitle(string message)
    {
        title.text = message;
    }

    public void UpdateFood(string number)
    {
        food.text = number;
    }

    public void UpdateCrystal(string number)
    {
        crystals.text = number;
    }

    public void OpenPausePanel()
    {
        if (pausePanel != null)
            pausePanel.gameObject.SetActive(true);
    }

    public void ClosePausePanel()
    {
        if (pausePanel != null)
            pausePanel.gameObject.SetActive(false);
    }
}

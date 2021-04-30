using System.Collections;
using UnityEngine;


public class FoodGroup : MonoBehaviour
{
    [SerializeField] private ColorId[] possibleColors;
    [SerializeField] private ColorScheme colorScheme;
    [SerializeField] private Food[] group;

    private void Awake()
    {
        ObjectColor randomColor = colorScheme.GetObjectColorsById(possibleColors)[Random.Range(0, possibleColors.Length)];

        foreach (Food food in group)
        {
            food.Colorize(randomColor);
        }
    }
}
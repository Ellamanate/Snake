using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum ColorId
{
    red,
    green,
    blue,
    yellow,
    white,
    black
}

[System.Serializable]
public struct ObjectColor
{
    public Color Color;
    public ColorId ColorId;
}

[CreateAssetMenu(menuName = "Colors")]
public class ColorScheme : ScriptableObject
{
    [SerializeField] ObjectColor[] colors;

    public ObjectColor[] GetObjectColorsById(ColorId[] id)
    {
        return colors.Where((x) => id.Contains(x.ColorId)).ToArray();
    }
}
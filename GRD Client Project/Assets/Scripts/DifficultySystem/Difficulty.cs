using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulties/New Difficulty")]

public class Difficulty : ScriptableObject
{
    public string name;

    public itemType typeToRecogniseBy;

    public int duration;

    public int pointsGainWhenCorrect;
    public int pointsLossWhenIncorrect;

    public enum itemType
    {
        colour,
        icon,
        code
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulties/New Difficulty")]
public class Difficulty : ScriptableObject
{
    public string name;


    public float duration;

    public int pointsGainWhenCorrect;
    public int pointsLossWhenIncorrect;
}
using UnityEngine;

/// <summary>
/// This class is a serializable object asset, and allows to create new difficulty levels and change their values.
/// - Natalia Pietrzak
/// </summary>
[CreateAssetMenu(menuName = "Difficulties/New Difficulty")]
public class Difficulty : ScriptableObject
{
    public string name;


    public float duration;

    public int pointsGainWhenCorrect;
    public int pointsLossWhenIncorrect;
}
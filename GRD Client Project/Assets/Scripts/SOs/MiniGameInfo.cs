using UnityEngine;

/// <summary>
/// This class is serializable objects class. It allows to create mini-games profile assets.
/// - Natalia Pietrzak
/// </summary>
[CreateAssetMenu(menuName = "MiniGameInfo/New MiniGameInfo")]
public class MiniGameInfo : ScriptableObject
{
    public string title;
    public string sceneTitleToLoad;
    public string description;
}
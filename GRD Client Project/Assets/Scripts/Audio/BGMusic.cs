using UnityEngine;

/// <summary>
/// This class is attached to the game object in the main menu to prevent the background audio game object being destroyed on scene reload.
/// - Natalia Pietrzak
/// </summary>
public class BGMusic : MonoBehaviour
{
    private static BGMusic _bgMusic;

    void Awake()
    {
        if (_bgMusic == null)
        {
            _bgMusic = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
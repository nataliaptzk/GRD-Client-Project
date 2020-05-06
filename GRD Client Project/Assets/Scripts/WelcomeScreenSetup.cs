using TMPro;
using UnityEngine;

/// <summary>
/// This class sets up the welcome panel on each of the mini-games.
/// - Natalia Pietrzak
/// </summary>
public class WelcomeScreenSetup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleField;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _descriptionField;

    [SerializeField] private MiniGameInfo _info;

    private void Awake()
    {
        _titleField.text = _info.title;
        _descriptionField.text = _info.description;
        _nameField.text = "Dear " + SessionManager.Nickname + ",";
    }
}
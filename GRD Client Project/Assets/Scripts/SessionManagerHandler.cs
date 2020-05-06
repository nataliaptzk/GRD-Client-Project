using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class is a handler class to change values of the SessionManager.cs through the UI buttons.
/// - Natalia Pietrzak
/// </summary>
public class SessionManagerHandler : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;


    private void Awake()
    {
        if (SessionManager.CurrentDifficulty == null && SceneManager.GetActiveScene().buildIndex > 2)
        {
            _gameManager = FindObjectOfType<GameManager>();
            SessionManager.CurrentDifficulty = _gameManager.Difficulties[0]; //set easy by deafult
        }
    }

    public void ChooseDifficultyHandler(Difficulty difficulty)
    {
        SessionManager.ChooseDifficulty(difficulty);
    }

    public void AssignNicknamePartA(TMP_InputField nameA)
    {
        SessionManager.nickA = nameA.text;
    }

    public void AssignNicknamePartB(TMP_InputField nameB)
    {
        SessionManager.nickB = nameB.text;
    }

    public void ChooseConsent(Toggle consent)
    {
        SessionManager.Consent = consent.isOn;
        Debug.Log(SessionManager.Consent);
    }

    public void ResetSessionHandler(Difficulty difficulty)
    {
        _gameManager.GetComponent<DataCollection>().WhenRestarted(SceneManager.GetActiveScene().name);

        SessionManager.ResetSession(difficulty);

        SceneManager.LoadScene("01 WelcomeScreen");
    }

    public void CreateSessionHandler()
    {
        SessionManager.CreateSession();
        DataCollectionFileManager.WriteStringNewLine(SessionManager.SessionId, SessionManager.Consent.ToString());
    }
}
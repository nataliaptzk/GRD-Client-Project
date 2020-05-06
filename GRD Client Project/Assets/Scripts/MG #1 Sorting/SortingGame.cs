using UnityEngine;

/// <summary>
/// This class is class responsible for the behaviour of the Sorting game. It calls the rubbish generation and fills in data in data collection file when the player exits the game before finishing.
/// - Natalia Pietrzak
/// </summary>
public class SortingGame : Level
{
    [SerializeField] private GameObject _rubbishSlotsParent;

    private RubbishGenerator _rubbishGenerator;


    private void Awake()
    {
        DisplayTutorialScreen();
        _gameManager = FindObjectOfType<GameManager>();

        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
    }

    private void Start()
    {
        _rubbishGenerator.GeneratePlasticObjects(SessionManager.CurrentDifficulty, _rubbishSlotsParent);
    }

    public void FillInDataCollectionForRemainingObjects()
    {
        //in case the timer runs out of time
        int tempCount = 0;
        tempCount = _rubbishSlotsParent.transform.childCount;

        for (int i = 0; i < tempCount; i++)
        {
            DataCollectionFileManager.WriteStringContinuation("run out of time", true);
            DataCollectionFileManager.WriteStringContinuation("N/A", true);
        }
    }

    public void CheckIfFinished()
    {
        if (_rubbishSlotsParent.transform.childCount == 1)
        {
            Invoke("FinishMiniGame", 1f);
        }
    }
}
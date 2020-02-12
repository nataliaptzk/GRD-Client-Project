using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationGame : MonoBehaviour
{
    [SerializeField] private MiniGameInfo _gameInfo;

    [SerializeField] private GameObject _rubbishSlotsParent;

    [SerializeField] private GameObject _claw;
    [SerializeField] private GameObject _hook;
    [SerializeField] private List<GameObject> _slotsToMoveTheClaw = new List<GameObject>();
    private int _currentSlot; // 0-2 -> 0 left, 1 middle, 2 right

    private GameManager _gameManager;
    private RubbishGenerator _rubbishGenerator;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
        MoveClaw(1); // move claw to middle
    }

    private void Start()
    {
        _rubbishGenerator.GeneratePlasticObjects(SessionManager.CurrentDifficulty, _rubbishSlotsParent);
    }

    public void MoveClaw(int i)
    {
        _currentSlot += i;
        _currentSlot = Mathf.Clamp(_currentSlot, 0, 2);
        //_claw.transform.localPosition = _slotsToMoveTheClaw[_currentSlot].transform.position;
        StartCoroutine(MoveClawEnumerator());
    }

    private IEnumerator MoveClawEnumerator()
    {
        float elapsedTime = 0;
        float waitTime = 2f;
        while (elapsedTime < waitTime)
        {
            _claw.transform.localPosition = Vector3.Lerp(_claw.transform.localPosition, _slotsToMoveTheClaw[_currentSlot].transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = _slotsToMoveTheClaw[_currentSlot].transform.position;
        yield return null;
    }

    public void Release()
    {
        //unparent the plastic
    }

    public void AttachPlastic()
    { // attach the plastic  - child it to the hook
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationGame : MonoBehaviour
{
    [SerializeField] private MiniGameInfo _gameInfo;

    [SerializeField] private GameObject _rubbishSlotsParent;

    [SerializeField] private GameObject _claw;
    [SerializeField] private Transform _hook;
    [SerializeField] private List<GameObject> _slotsToMoveTheClaw = new List<GameObject>();
    private int _currentSlot; // 0-2 -> 0 left, 1 middle, 2 right
    private bool _isClawMoving = false;


    private GameManager _gameManager;
    private RubbishGenerator _rubbishGenerator;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
        //    MoveClaw(1); // move claw to middle
        _currentSlot = 1;
    }

    private void Start()
    {
        _rubbishGenerator.GeneratePlasticObjects(SessionManager.CurrentDifficulty, _rubbishSlotsParent);
    }

    public void MoveClaw(int i)
    {
        if (!_isClawMoving)
        {
            _currentSlot += i;
            _currentSlot = Mathf.Clamp(_currentSlot, 0, 2);
            //_claw.transform.localPosition = _slotsToMoveTheClaw[_currentSlot].transform.position;
            StartCoroutine(MoveClawEnumerator());
        }
    }

    private IEnumerator MoveClawEnumerator()
    {
        _isClawMoving = true;
        float elapsedTime = 0;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            _claw.transform.localPosition = Vector3.Lerp(_claw.transform.localPosition, _slotsToMoveTheClaw[_currentSlot].transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = _slotsToMoveTheClaw[_currentSlot].transform.position;
        _isClawMoving = false;

        yield return null;
    }

    public void Release()
    {
        if (_hook.transform.childCount > 0)
        {
            //todo Make the Claw that will open on release, so no need to switch off RB2
            _hook.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _hook.transform.GetChild(0).SetParent(null);
        }
    }

    public void AttachPlastic()
    {
        if (_rubbishSlotsParent.transform.childCount != 0 && _hook.transform.childCount == 0)
        {
            _rubbishSlotsParent.transform.GetChild(0).parent = _hook;
            _hook.transform.GetChild(0).localPosition = new Vector3(0, -0.12f, 0);
            _hook.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationGame : Level
{
    [SerializeField] private GameObject _rubbishSlotsParent;

    [SerializeField] private GameObject _claw;
    [SerializeField] private Transform _rubbishSlot;
    [SerializeField] private HingeJoint2D _hookHingeLeft;
    [SerializeField] private HingeJoint2D _hookHingeRight;
    [SerializeField] private FixedJoint2D _hookFixedLeft;
    [SerializeField] private FixedJoint2D _hookFixedRight;
    [SerializeField] private List<GameObject> _slotsToMoveTheClaw = new List<GameObject>();
    [SerializeField] private List<GameObject> _slotsToMoveTheClawHardDifficulty = new List<GameObject>();
    private int _currentSlot; // 0-2 -> 0 left, 1 middle, 2 right || hard difficulty 0-4 -> 0 - left, 1 - mid left, 2 - mid, 3 - right mid, 4 - right
    private bool _isClawMoving = false;
    private bool _isReleasing = false;

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
        if (SessionManager.CurrentDifficulty.name == "easy" || SessionManager.CurrentDifficulty.name == "normal")
        {
            _currentSlot = 1;
        }
        else if (SessionManager.CurrentDifficulty.name == "hard")
        {
            _currentSlot = 2;
        }
    }

    public void MoveClaw(int i)
    {
        if (!_isClawMoving && !_isReleasing)
        {
            _currentSlot += i;
            if (SessionManager.CurrentDifficulty.name == "easy" || SessionManager.CurrentDifficulty.name == "normal")
            {
                _currentSlot = Mathf.Clamp(_currentSlot, 0, 2);
                StartCoroutine(MoveClawEnumerator(_slotsToMoveTheClaw));
            }
            else if (SessionManager.CurrentDifficulty.name == "hard")
            {
                _currentSlot = Mathf.Clamp(_currentSlot, 0, 4);
                StartCoroutine(MoveClawEnumerator(_slotsToMoveTheClawHardDifficulty));
            }
        }
    }

    private IEnumerator MoveClawEnumerator(List<GameObject> pointsToMoveAcross)
    {
        _isClawMoving = true;
        float elapsedTime = 0;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            _claw.transform.localPosition = Vector3.Lerp(_claw.transform.localPosition, pointsToMoveAcross[_currentSlot].transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = pointsToMoveAcross[_currentSlot].transform.position;
        _isClawMoving = false;

        yield return null;
    }

    public void ReleaseHandler()
    {
        if (!_isReleasing && !_isClawMoving)
        {
            _hookHingeLeft.enabled = true;
            _hookFixedLeft.enabled = false;
            _hookHingeRight.enabled = true;
            _hookFixedRight.enabled = false;

            StartCoroutine(Release());
        }
    }


    private IEnumerator Release()
    {
        if (_rubbishSlot.transform.childCount != 0)
        {
            _rubbishSlot.GetChild(0).parent = null;
        }

        int speed = 100;
        _isReleasing = true;

        do
        {
            if (_hookHingeLeft.limitState == JointLimitState2D.UpperLimit && speed > 0)
            {
                speed = speed * -1;
            }

            JointMotor2D motor = _hookHingeLeft.motor;
            motor.motorSpeed = speed;
            _hookHingeLeft.motor = motor;

            JointMotor2D motor2 = _hookHingeRight.motor;
            motor2.motorSpeed = speed * -1;
            _hookHingeRight.motor = motor2;

            yield return null;
        } while (_hookHingeLeft.jointAngle > _hookHingeLeft.limits.min);

        _hookFixedLeft.enabled = true;
        _hookHingeLeft.enabled = false;

        _hookFixedRight.enabled = true;
        _hookHingeRight.enabled = false;


        _isReleasing = false;
        if (_rubbishSlotsParent.transform.childCount == 0)
        {
            Invoke("FinishMiniGame", 1f);
        }

        yield return null;
    }

    public void AttachPlastic()
    {
        if (!_isReleasing)
        {
            if (_rubbishSlotsParent.transform.childCount != 0 && _rubbishSlot.transform.childCount == 0)
            {
                _rubbishSlotsParent.transform.GetChild(0).parent = _rubbishSlot;
                _rubbishSlot.transform.GetChild(0).localPosition = new Vector3(0, 0f, 0);
            }
        }
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

        for (int i = 0; i < _rubbishSlot.transform.childCount; i++)
        {
            DataCollectionFileManager.WriteStringContinuation("run out of time", true);
            DataCollectionFileManager.WriteStringContinuation("N/A", true);
        }
        
    }
}
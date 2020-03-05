using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationGame : Level
{
    [SerializeField] private GameObject _rubbishSlotsParent;

    [SerializeField] private GameObject _claw;
    [SerializeField] private Transform _rubbishSlot;
    [SerializeField] private HingeJoint2D _hookHinge;
    [SerializeField] private List<GameObject> _slotsToMoveTheClaw = new List<GameObject>();
    private int _currentSlot; // 0-2 -> 0 left, 1 middle, 2 right
    private bool _isClawMoving = false;

    private RubbishGenerator _rubbishGenerator;

    private void Awake()
    {
        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
        //    MoveClaw(1); // move claw to middle
        _currentSlot = 1;

    }

    private void Start()
    {
        _rubbishGenerator.GeneratePlasticObjects(SessionManager.CurrentDifficulty, _rubbishSlotsParent);
        StartCoroutine(_timer.Countdown(SessionManager.CurrentDifficulty.duration * _miniGameBaseTime));
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

    public void ReleaseHandler()
    {
        StartCoroutine(Release());
    }

    private void Update()
    {
        Debug.Log(_hookHinge.limitState);
    }

    private IEnumerator Release()
    {
        int openingSpeed = 100;
        int closingSpeed = -100;
        int stoppedSpeed = 0;

        bool finishedRelease = false;
        bool opening = true;
        JointMotor2D motor = _hookHinge.motor;

    //    motor.motorSpeed = openingSpeed;
      //  _hookHinge.motor = motor;

        while (!finishedRelease)
        {
            if (_hookHinge.limitState == JointLimitState2D.Inactive && opening)
            {
                motor.motorSpeed = openingSpeed;
                _hookHinge.motor = motor;
                Debug.Log("1");
            }
            else if (_hookHinge.limitState == JointLimitState2D.UpperLimit)
            {
                opening = false;
                motor.motorSpeed = closingSpeed;
                _hookHinge.motor = motor;
                Debug.Log("2");

            }
            else if (_hookHinge.limitState == JointLimitState2D.LowerLimit && !opening)
            {
                motor.motorSpeed = closingSpeed;
                _hookHinge.motor = motor;
                finishedRelease = true;
                Debug.Log("3");

                yield return null;
            }


            /*if (_hook.transform.childCount > 0)
            {
                _hook.transform.GetChild(0).SetParent(null);
            }

            while (_hookHinge.jointAngle < _hookHinge.limits.max && opening)
            {
                motor.motorSpeed = openingSpeed;
                _hookHinge.motor = motor;
            }

            opening = false;

            motor.motorSpeed = closingSpeed;
            _hookHinge.motor = motor;


            while (!opening && _hookHinge.jointAngle > _hookHinge.limits.min)
            {
                motor.motorSpeed = stoppedSpeed;
                _hookHinge.motor = motor;
            }*/
        }


        yield return null;
    }

    public void AttachPlastic()
    {
        if (_rubbishSlotsParent.transform.childCount != 0 && _rubbishSlot.transform.childCount == 0)
        {
            _rubbishSlotsParent.transform.GetChild(0).parent = _rubbishSlot;
            _rubbishSlot.transform.GetChild(0).localPosition = new Vector3(0, 0f, 0);
        }
    }
}
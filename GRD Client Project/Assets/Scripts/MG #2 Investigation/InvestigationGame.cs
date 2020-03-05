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
    private int _currentSlot; // 0-2 -> 0 left, 1 middle, 2 right
    private bool _isClawMoving = false;
    private bool _isReleasing = false;

    private RubbishGenerator _rubbishGenerator;

    private void Awake()
    {
        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
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
        if (!_isReleasing)
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
        AttachPlastic();
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
            else if (_rubbishSlotsParent.transform.childCount == 0)
            {
                FinishMiniGame();
            }
        }
    }
}
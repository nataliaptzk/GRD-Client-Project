using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraInvestigation : MonoBehaviour
{
    [SerializeField] private Transform _itemToFollow;
    [SerializeField] private Vector3 _offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _itemToFollow.position + _offset, 1f);
    }
}
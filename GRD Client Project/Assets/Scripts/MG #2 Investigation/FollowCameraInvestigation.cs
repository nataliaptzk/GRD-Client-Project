using UnityEngine;

/// <summary>
/// This class is attached to the camera that displays a zoomed in image of the machine hook in the investigation game. It lerps the camera's position to follow the hook.
/// - Natalia Pietrzak
/// </summary>
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
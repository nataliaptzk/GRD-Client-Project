using Doozy.Engine.UI;
using UnityEngine;

/// <summary>
/// This class manages the behaviours of UI panels and the animations.
/// - Natalia Pietrzak
/// </summary>
public class PanelUI : MonoBehaviour
{
    [SerializeField] private UIView _uiView;

    private void OnEnable()
    {
        _uiView.Show();
    }

    [ContextMenu("GetMyReference")]
    public void GetMyReference()
    {
        _uiView = gameObject.GetComponent<UIView>();
    }
}
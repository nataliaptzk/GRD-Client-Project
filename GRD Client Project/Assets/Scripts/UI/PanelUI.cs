using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;

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
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpScreenSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _helps = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI _textBox;
    private void Start()
    {
        HelpSetup();
        LoadJsonTips();
    }

    private void HelpSetup()
    {
        if (SessionManager.CurrentDifficulty.name == "easy" || SessionManager.CurrentDifficulty.name == "normal")
        {
            _helps[0].SetActive(true);
            _helps[1].SetActive(false);
        }
        else if (SessionManager.CurrentDifficulty.name == "hard")
        {
            _helps[0].SetActive(false);
            _helps[1].SetActive(true);
        }
    }

    public void LeftButton()
    {
        DisplayTip(-1);
    }

    public void RightButton()
    {
        DisplayTip(1);
    }

    private void DisplayTip(int index)
    {
    }

    private void LoadJsonTips()
    {
    }
}
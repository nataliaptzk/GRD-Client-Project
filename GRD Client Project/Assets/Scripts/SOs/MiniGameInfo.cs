using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniGameInfo/New MiniGameInfo")]
public class MiniGameInfo : ScriptableObject
{
    public string title;
    public List<string> helpTips = new List<string>();
    public string sceneTitleToLoad;
}
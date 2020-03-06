using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public Difficulty difficulty;

    [ContextMenu("test")]
    public void Test()
    {
        SessionManager.CreateSession();
    }
}
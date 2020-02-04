using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManagerHandler : MonoBehaviour
{
    public void ChooseDifficultyHandler(Difficulty difficulty)
    {
        SessionManager.ChooseDifficulty(difficulty);
    }

    public void ResetSessionHandler(Difficulty difficulty)
    {
        SessionManager.ResetSession(difficulty);
    }
}

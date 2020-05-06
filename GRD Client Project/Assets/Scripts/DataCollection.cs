using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for Unity Analytics data collection.
/// - Natalia Pietrzak
/// </summary>\
public class DataCollection : MonoBehaviour
{
    public void SendFinishedLevelInfo(string title, int correctAmount, int incorrectAmount)
    {
        if (SessionManager.Consent)
        {
            Dictionary<string, object> levelData = new Dictionary<string, object>();
            levelData.Add("SessionID", SessionManager.SessionId);
            levelData.Add("GameTitle", title);
            levelData.Add("CorrectAnswers", correctAmount);
            levelData.Add("IncorrectAnswers", incorrectAmount);


            AnalyticsResult ar = Analytics.CustomEvent("LEVEL_FINISHED", levelData);

            Debug.Log("Result = " + ar.ToString());
        }
    }

    public void HelpScreenOpened(string title, int howManyTimesHelpOpened)
    {
        if (SessionManager.Consent)
        {
            Dictionary<string, object> helpScreen = new Dictionary<string, object>();
            helpScreen.Add("SessionID", SessionManager.SessionId);
            helpScreen.Add("GameTitle", title);
            helpScreen.Add("AmountHelpOpened", howManyTimesHelpOpened);


            AnalyticsResult ar = Analytics.CustomEvent("HELP_OPENED", helpScreen);

            Debug.Log("Result = " + ar.ToString());
        }
    }

    public void WhenRestarted(string title)
    {
        if (SessionManager.Consent)
        {
            Dictionary<string, object> restartInfo = new Dictionary<string, object>();
            restartInfo.Add("SessionID", SessionManager.SessionId);
            restartInfo.Add("DuringGameTitle", title);


            AnalyticsResult ar = Analytics.CustomEvent("RESTART_INFO", restartInfo);

            Debug.Log("Result = " + ar.ToString());
        }
    }

    public void DidYouKnowAnalytics(string answer)
    {
        if (SessionManager.Consent)
        {
            Dictionary<string, object> didYouKnowInfo = new Dictionary<string, object>();
            didYouKnowInfo.Add("SessionID", SessionManager.SessionId);
            didYouKnowInfo.Add("CurrentDidYouKnowScene", SceneManager.GetActiveScene().name);
            didYouKnowInfo.Add("Answer", answer);
            didYouKnowInfo.Add("TimeSpentOnThePage", Time.timeSinceLevelLoad);

            // Custom Data Collection
            DataCollectionFileManager.WriteStringContinuation(answer, true);
            DataCollectionFileManager.WriteStringContinuation(Time.timeSinceLevelLoad.ToString(), true);

            AnalyticsResult ar = Analytics.CustomEvent("DYK_INFO", didYouKnowInfo);

            Debug.Log("Result = " + ar.ToString());
        }
    }
}
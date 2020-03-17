﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestExample : MonoBehaviour
{
    // Where to send our request
    const string DEFAULT_URL = "https://jsonplaceholder.typicode.com/todos/1";
    string targetUrl = DEFAULT_URL;
 
    // Keep track of what we got back
    string recentData = "";
 
    void Awake()
    {
        this.StartCoroutine(this.RequestRoutine(this.targetUrl, this.ResponseCallback));
    }
 
    // Web requests are typially done asynchronously, so Unity's web request system
    // returns a yield instruction while it waits for the response.
    //
    private IEnumerator RequestRoutine(string url, Action<string> callback = null)
    {
        // Using the static constructor
        var request = UnityWebRequest.Get(url);
 
        // Wait for the response and then get our data
        yield return request.SendWebRequest();
        var data = request.downloadHandler.text;
 
        // This isn't required, but I prefer to pass in a callback so that I can
        // act on the response data outside of this function
        if (callback != null)
            callback(data);
    }
 
    // Callback to act on our response data
    private void ResponseCallback(string data)
    {
        Debug.Log(data);
        recentData = data;
    }
 
    
    
}
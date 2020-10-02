#region copyright
/*
 * Copyright Mete Ozguz 2019
 *
 * http://www.meteozguz.com
 * ozguz.mete@gmail.com
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 */
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInternetConnection : MonoBehaviour
{
    public bool onlyOnce = true;
    private bool checkedOnce = false;
    public bool continuousCheck = false;
    public int intervalInSec = 40;
    private float elapsedTime = 0.0f;


    IEnumerator Check(Action<bool> action)
    {
        if (onlyOnce == true && checkedOnce == true)
        {
            yield break;
        }
        checkedOnce = true;
        //Debug.Log("Checking internet connection...");
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListen((int)Events.PLAY_BUTTON_CLICKED, TryNow);
    }

    private void OnDisable()
    {
        EventManager.StopListen((int)Events.PLAY_BUTTON_CLICKED, TryNow);
    }

    void Start()
    {
        //StartCoroutine(Check(IsConnected));
    }

    private void TryNow()
    {
        StartCoroutine(Check(IsConnected));
    }

    // Update is called once per frame
    void Update()
    {
        if (continuousCheck)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > intervalInSec)
            {
                elapsedTime = 0.0f;
                StartCoroutine(Check(IsConnected));
            }
        }
    }

    private void IsConnected(bool isConnected)
    {
        if (isConnected)
        {
            EventManager.Trigger((int)Events.CONNECTED_TO_NET, 1);
        }
        else
        {
            EventManager.Trigger((int)Events.CONNECTED_TO_NET, 0);
        }
    }
}

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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{

    public GameObject okButton;
    public PopupMessages popupMessages;

    private TextMeshPro message;

    private State currentState = State.HIDDEN;
    private State lastState = State.HIDDEN;

    public enum State
    {
        HIDDEN = 0,
        NO_CONNECTION
    }

    public enum Action
    {
        HIDE,
        NO_CONNECTION
    }

    // Use this for initialization
    void Start()
    {
        message = transform.Find("Message").GetComponent<TextMeshPro>();

        EventManager.StartListen((int)Events.POPUP_OKBUTTON_CLICKED, OkButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != lastState)
        {
            lastState = currentState;
            if (currentState == State.HIDDEN)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }
    }

    private void Activate()
    {
        DisplayMessage(currentState);
        SetActiveChildren(true);
        Game.GameData.gameState.currentState = GameState.State.IN_POPUP;
    }

    private void Deactivate()
    {
        SetActiveChildren(false);
        Game.GameData.gameState.currentState = GameState.State.IN_MENU;
    }

    private void OkButtonClicked()
    {
        currentState = State.HIDDEN;
        Application.Quit(); // No other Ok Button function for now...
    }


    private void DisplayMessage(PopupController.State messageId)
    {
        if (message != null)
        {
            message.text = popupMessages.GetMessage(messageId);
        }
    }

    private void SetActiveChildren(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    public void TakeAction(Action action)
    {
        switch (action)
        {
            case Action.HIDE:
                currentState = State.HIDDEN;
                break;
            case Action.NO_CONNECTION:
                currentState = State.NO_CONNECTION;
                break;
        }
    }
}

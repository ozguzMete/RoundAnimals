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

public class PopupMessages : MonoBehaviour
{
    private Dictionary<int, string> currentMessages;

    private Dictionary<int, string> englishMessages = new Dictionary<int, string>();

    public void InitPopupMessagesEnglish()
    {
        englishMessages.Add((int)PopupController.State.NO_CONNECTION, "We can't connect to internet.\n\nYou can use this application without internet when you unlock all the animals.\n\nConnect to internet and try again.");
    }

    private Dictionary<int, string> turkishMessages = new Dictionary<int, string>();

    public void InitPopupMessagesTurkish()
    {
        turkishMessages.Add((int)PopupController.State.NO_CONNECTION, "\u0130nternete ba\u011flanam\u0131yoruz.\n\nHayvanlar\u0131n hepsini a\u00e7t\u0131\u011f\u0131n\u0131zda, uygulamay\u0131 internetsiz kullanabilirsiniz.\n\n\u0130nternete ba\u011flan\u0131n ve tekrar deneyin.");
       // turkishMessages.Add((int)PopupController.State.NO_CONNECTION, "İnternete bağlanamıyoruz.\n\nHayvanların hepsini açtığınızda, uygulamayı internetsiz kullanabilirsiniz.\n\nİnternete bağlanın ve tekrar deneyin.");
    }

    // Use this for initialization
    void Start()
    {
        InitPopupMessagesEnglish();
        InitPopupMessagesTurkish();
        InitCurrentMessages();
    }

    private void InitCurrentMessages()
    {
        if (Application.systemLanguage == SystemLanguage.English)
        {
            currentMessages = englishMessages;
        }
        else if (Application.systemLanguage == SystemLanguage.Turkish)
        {
            currentMessages = turkishMessages;
        }
        else
        {
            currentMessages = englishMessages;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetMessage(PopupController.State messageId)
    {
        string result = "";
        if (currentMessages.TryGetValue((int)messageId, out result))
        {
            return result;
        }
        else
        {
            return "";
        }
    }
}

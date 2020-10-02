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
namespace GameUI
{
    public class Exit : MonoBehaviour
    {
        public GameObject ExitButton;

        void Start()
        {
            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_INFO, EnableExitButton);
            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_PAGE, EnableExitButtonWithId);
            EventManager.StartListen((int)Events.ANIMAL_CLICKED, DisableExitButtonWithGameObject);
            EventManager.StartListen((int)Events.EXIT_BUTTON_CLICKED, DisableExitButton);
        }

        private void DisableExitButton()
        {           
            transform.localScale = Vector3.one;
            ExitButton.SetActive(false);
        }

        private void EnableExitButton()
        {
            ExitButton.SetActive(true);
        }

        private void EnableExitButtonWithId(int id)
        {
            ExitButton.SetActive(true);
        }

        private void DisableExitButtonWithGameObject(GameObject go)
        {
            transform.localScale = new Vector3(0.326f, 0.326f);
        }
    }
}
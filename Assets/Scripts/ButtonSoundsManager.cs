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
using MeteOzguz.RoundAnimals;
using UnityEngine;

public class ButtonSoundsManager : MonoBehaviour
{

    public ActiveButtonSounds activeButtonSounds;

    protected AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EventManager.StartListen((int)Events.BUTTON_CLICKED, (Action)PlayButtonClickSound);
        EventManager.StartListen((int)Events.BUTTON_CLICKED, (Action<GameObject>)PlayButtonClickSound);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonClickSound()
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(activeButtonSounds.NextCip());
    }

    /// <summary>
    /// Finds button whether active or inactive
    /// </summary>
    /// <param name="button"></param>
    public void PlayButtonClickSound(GameObject go)
    {
        Button button = go.GetComponent<Button>();
        if (button != null)
        {
            if (button.Interactable)
            {
                audioSource.pitch = 1;
                audioSource.PlayOneShot(activeButtonSounds.NextCip());
            }
            else
            {
                audioSource.pitch = 3;
                audioSource.PlayOneShot(activeButtonSounds.NextCip());
            }
        }
    }
}

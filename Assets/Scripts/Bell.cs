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

public class Bell : MonoBehaviour
{

    public AudioClip[] sounds = new AudioClip[4];
    public int[] soundPlayOrder = { 1, 1, 0, 3, 2, 1, 2, 3, 1, 3, 2, 2 };
    public int[] gapsBetweenSounds = { 1, 2, 3, 1, 1, 1, 2, 1, 1, 1, 3, 3 };
    protected AudioSource audioSource = null;

    private int soundIndex = 0;
    private int gapIndex = 0;
    private float deltaTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        Find.Upward<AudioSource>(transform, out audioSource);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsVisible())
        {
            if (audioSource != null && audioSource.enabled == true)
            {
                deltaTime += Time.deltaTime;

                if (audioSource.isPlaying)
                {
                    return;
                }

                if (deltaTime > gapsBetweenSounds[gapIndex])
                {
                    gapIndex++;
                    gapIndex = gapIndex % gapsBetweenSounds.Length;

                    deltaTime = 0;

                    audioSource.PlayOneShot(sounds[soundPlayOrder[soundIndex]]);
                    soundIndex++;
                    soundIndex = soundIndex % soundPlayOrder.Length;
                }
            }
        }
    }

    private bool IsVisible()
    {
        Vector3 vposition = Camera.main.WorldToViewportPoint(transform.position);
        if (vposition.x >= 0 && vposition.x <= 1 && vposition.y >= 0 && vposition.y <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

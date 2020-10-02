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
using MeteOzguz.CuteLocks;
using MeteOzguz.RoundAnimals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCuteLocksOnActivePage : MonoBehaviour
{
    public CuteLocks cuteLocks;
    public PageWiseLockedAnimals pageWiseLockedAnimals;

    [Tooltip("Animation will be played once in every animationInterval seconds!")]
    public float animationInterval = 5.0f;

    private GameObject cuteLock = null;
    private CuteLockController cuteLockController = null;

    private float time = 0.0f;
    // Use this for initialization
    void Start()
    {
        EventManager.StartListen((int)Events.CAMERA_MOVED_TO_PAGE, AnimateMiddleCuteLockOnActivePage);
    }

    private void AnimateMiddleCuteLockOnActivePage(int pageId)
    {
        GameObject lockedAnimal;
        if (pageWiseLockedAnimals.GetFirstLockedAnimalAtPage(pageId, out lockedAnimal))
        {
            if (cuteLocks != null && cuteLocks.TryGetValue(lockedAnimal.name, out cuteLock))
            {
                cuteLockController = cuteLock.GetComponent<CuteLockController>();
                time = animationInterval;
            }
            else
            {
                cuteLock = null;
                cuteLockController = null;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > animationInterval)
        {
            time = 0.0f;
            if (cuteLock != null && cuteLockController != null)
            {
                cuteLockController.playAnimation = true;
            }
        }
    }

}

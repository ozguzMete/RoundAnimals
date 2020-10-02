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
using MeteOzguz.RoundAnimals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnlockLockedAnimalsWhen : MonoBehaviour
{

    public LockedAnimals lockedAnimals;

    // Use this for initialization
    void Start()
    {
    }

    private void OnEnable()
    {
        EventManager.StartListen((int)Events.AD_FINISHED, Unlock);
        EventManager.StartListen((int)Events.AD_SKIPPED, UnlockWithoutChangingPlayerPrefs);
        EventManager.StartListen((int)Events.AD_FAILED, UnlockWithoutChangingPlayerPrefs);
    }

    private void OnDisable()
    {
        EventManager.StopListen((int)Events.AD_FINISHED, Unlock);
        EventManager.StopListen((int)Events.AD_SKIPPED, UnlockWithoutChangingPlayerPrefs);
        EventManager.StopListen((int)Events.AD_FAILED, UnlockWithoutChangingPlayerPrefs);
    }

    private void Unlock(GameObject animal)
    {
        GameObject lockedAnimal;
        if (lockedAnimals.TryGetValue(animal.name, out lockedAnimal))
        {
            //Debug.Log("Unlocking " + animal.name);
            UnlockLockedAnimal(lockedAnimal);
            Animals.Unlock(lockedAnimal.name);
            lockedAnimals.Remove(animal.name);
        }
    }

    private void UnlockWithoutChangingPlayerPrefs(GameObject animal)
    {
        GameObject lockedAnimal;
        if (lockedAnimals.TryGetValue(animal.name, out lockedAnimal))
        {
            //Debug.Log("Unlocking " + animal.name);
            UnlockLockedAnimal(lockedAnimal);
            lockedAnimals.Remove(animal.name);
        }
    }

    private void UnlockLockedAnimal(GameObject lockedAnimal)
    {
        BoxCollider2D boxCollider2D = lockedAnimal.GetComponent<BoxCollider2D>();
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = true;
        }
        MakeUnavailable makeUnavailable = lockedAnimal.GetComponent<MakeUnavailable>();
        makeUnavailable.enabled = true;
        makeUnavailable.Reset();
    }

    // Update is called once per frame
    void Update()
    {
    }
}

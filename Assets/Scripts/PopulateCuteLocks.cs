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

public class PopulateCuteLocks : MonoBehaviour
{

    public CuteLocks cuteLocks;
    public LockedAnimals lockedAnimals;
    public GameObject cuteLockPrefab;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject lockedAnimal in lockedAnimals.GetAsNewList())
        {
            // Add cuteLock to locked animal
            if (cuteLockPrefab != null)
            {
                GameObject cuteLock = Instantiate<GameObject>(cuteLockPrefab);
                cuteLock.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                cuteLock.transform.parent = transform;
                cuteLock.name = lockedAnimal.name;
                Follow follow = cuteLock.AddComponent<Follow>();
                follow.target = lockedAnimal.transform;
                if (cuteLocks != null)
                {
                    cuteLocks.Add(cuteLock.name, cuteLock);
                }
            }
        }
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

}

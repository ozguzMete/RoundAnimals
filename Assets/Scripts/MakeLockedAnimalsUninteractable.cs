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

public class MakeLockedAnimalsUninteractable : MonoBehaviour {

    public LockedAnimals lockedAnimals;

    // colliders are added dynamically, at the time of first check we might miss them, missed ones added here...
    private Stack<Transform> childrenWithColliders = new Stack<Transform>();

    // Use this for initialization
    void Start()
    {
        foreach(GameObject lockedAnimal in lockedAnimals.GetAsNewList())
        {
            TryToMakeItUniteractable(lockedAnimal.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (childrenWithColliders.Count > 0)
        {
            TryToMakeItUniteractable(childrenWithColliders.Pop());
        }
        else
        {
            enabled = false;
        }
    }

    private void TryToMakeItUniteractable(Transform child)
    {
        BoxCollider2D box = child.GetComponent<BoxCollider2D>();
        if (box != null)
        {
            box.enabled = false;
        }
        else
        {
            childrenWithColliders.Push(child);
        }
    }
}

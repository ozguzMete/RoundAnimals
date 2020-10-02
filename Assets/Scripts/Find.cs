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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Find
{
    public static Transform Upward<T>(Transform startTransform, out T targetComponent)
    {
        targetComponent = default(T);
        Transform currentTransform = startTransform;
        while (currentTransform != null)
        {
            targetComponent = currentTransform.GetComponent<T>();
            if (targetComponent != null && !targetComponent.Equals(default(T)))
            {
                return currentTransform;
            }
            else
            {
                currentTransform = currentTransform.parent;
            }
        }
        return null;
    }

    public static bool Upward(GameObject startObject, string targetName, out GameObject target)
    {
        target = null;
        if (startObject == null)
        {
            return false;
        }
        Transform currentTransform = startObject.transform;
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
            if (currentTransform.gameObject.name.Equals(targetName))
            {
                target = currentTransform.gameObject;
                return true;
            }
        }
        return false;
    }

    public static Transform Downward<T>(Transform startTransform, out T targetComponent)
    {
        targetComponent = default(T);

        Stack<Transform> pool = new Stack<Transform>();
        pool.Push(startTransform);
        while (pool.Peek() != null)
        {
            Transform currentTransform = pool.Pop();

            targetComponent = currentTransform.GetComponent<T>();
            if (targetComponent != null && !targetComponent.Equals(default(T)))
            {
                return currentTransform;
            }
            else
            {
                for (int i = 0; i < currentTransform.childCount; i++)
                {
                    pool.Push(currentTransform.GetChild(i));
                }
            }
        }
        return null;
    }
}

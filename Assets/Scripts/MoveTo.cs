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

public class MoveTo : MonoBehaviour
{

    public bool continous = false;
    public float yPosition = 0.0f;
    public float xPosition = 0.0f;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (continous)
        {
            Move();
        }
    }

    public void Move()
    {
        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(xPosition, yPosition, Camera.main.nearClipPlane));

        // we are making z = 0 for orthographic camera
        // in order to make it clickable
        if (Camera.main.orthographic)
        {
            position.z = 0;
        }

        transform.position = position;
    }

    private void OnEnable()
    {
        Move();
    }
}

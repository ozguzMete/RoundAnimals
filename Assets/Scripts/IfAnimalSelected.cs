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

public class IfAnimalSelected : MonoBehaviour
{
    private SmoothFollow2D smoothFollow2D;
    private GameObject selectedAnimal;
    private bool firstUpdate = true;
    private Vector3 origin;
    // Use this for initialization
    void Start()
    {
        smoothFollow2D = GetComponent<SmoothFollow2D>();
        selectedAnimal = GameObject.Find("/UICameraSystem/CameraPositions/SelectedAnimal");
        EventManager.StartListen((int)Events.EXIT_BUTTON_CLICKED, (Action)MoveToOrigin);
        EventManager.StartListen((int)Events.ANIMAL_CLICKED, MoveToSelectedAnimal);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            origin = transform.position;
        }
    }

    void MoveToSelectedAnimal(GameObject go)
    {
        if (gameObject.Equals(go))
        {
            smoothFollow2D.target = selectedAnimal.transform;
        }
    }

    void MoveToOrigin()
    {
        smoothFollow2D.target = null; // for any case...
        transform.position = origin;
    }

    void MoveToOrigin(int id)
    {
        if (transform.GetSiblingIndex() == id)
        {
            MoveToOrigin();
        }
    }
}

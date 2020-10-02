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

/// <summary>
/// Cloud will move to left until hit the left boundary
/// Clouds will die after passing left boundary and resurrect randomly between resurrection boundaries
/// Clouds will have a random scale between max and min scale
/// Will have random layer number according to target layer
/// </summary>
public class Cloud : MonoBehaviour {

    public GameObject leftBoundary;
    public GameObject resurrectionArea;
    public GameObject objectFromTargetLayer;

    public int maxScale = 10;
    public int minScale = 4;

    // Max. Distance in units per sec.
    public float maxDistance = 0.65f;
    public float minDistance = 0.1f;
    private float distance;

    private int targetSortingOrder;
    // Use this for initialization
    void Start () {
        Distance();
    }

    private void Distance()
    {
        distance = UnityEngine.Random.Range(minDistance, maxDistance);
    }

    private void Scale()
    {
        targetSortingOrder = objectFromTargetLayer.GetComponent<SpriteRenderer>().sortingOrder;
        float scale = UnityEngine.Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    // Update is called once per frame
    void Update ()
    {
        if(leftBoundary.transform.position.x >= transform.position.x)
        {
            // Kill and resurrect this cloud!
            Resurrect();
                return;
        }
        else
        {
            // Keep moving to left
            transform.Translate(Vector2.left * distance * Time.deltaTime);
            return;
        }
    }

    private void Resurrect()
    {
        Vector3 bottomLeft = resurrectionArea.GetComponent<Renderer>().bounds.min;
        Vector3 topRight = resurrectionArea.GetComponent<Renderer>().bounds.max;

        float x = UnityEngine.Random.Range(bottomLeft.x, topRight.x);
        float y = UnityEngine.Random.Range(bottomLeft.y, topRight.y);

        transform.position = new Vector3(x, y, transform.position.z);
        Scale();
        Layer();
        Distance();
    }

    private void Layer()
    {
        int coin = UnityEngine.Random.Range(0, 2);
        int value = 0;
        if(coin == 0)
        {
            value = -1;
        }
        else
        {
            value = +1;       
        }
        GetComponent<SpriteRenderer>().sortingOrder = targetSortingOrder + value;
    }
}

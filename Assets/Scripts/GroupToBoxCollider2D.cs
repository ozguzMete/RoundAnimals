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
using UnityEngine;
using System.Collections;


public class GroupToBoxCollider2D : MonoBehaviour
{

    public bool isTrigger = true;

    private Bounds bounds = new Bounds();

    // Use this for initialization
    void Start()
    {
        Calculate();
    }

    public void Calculate()
    {
        int count = 0;
        foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>())
        {
            if (count == 0)
            {
                bounds = new Bounds(r.transform.position, Vector3.zero);
                count++;
            }
            bounds.Encapsulate(r.bounds);
        }
        if (count == 0) // No children, take itself and make a collider from it!
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            bounds = new Bounds(r.transform.position, Vector3.zero);
            bounds.Encapsulate(r.bounds);
            CreateCollider();
            return;
        }
        CreateCollider();
    }

    private void CreateCollider()
    {
        GameObject newgo = new GameObject(); // Finding true local scale for this group by creating a new gameobject!
        newgo.transform.position = gameObject.transform.position;
        newgo.transform.parent = gameObject.transform;

        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(bounds.size.x * newgo.transform.localScale.x, bounds.size.y * newgo.transform.localScale.y);
        collider.isTrigger = isTrigger;

        GameObject.Destroy(newgo);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

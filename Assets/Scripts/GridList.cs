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

public class GridList : MonoBehaviour
{
    public Pages pages;

    public GameObject theList;
    public GameObject cameraPositions;
    public int xOffsetConstant = 3;

    private int pageCount = 1;

    private Vector3 firstPageStartPosition = new Vector3(0, 0, 0);

    public int PageCount
    {
        get
        {
            return pageCount;
        }
    }

    // Use this for initialization
    void Start()
    {
        CalculatePageCount();

        // Generate all the positions for the list items
        int listSize = theList.transform.childCount;
        Vector3Int[] cellPositions = new Vector3Int[listSize];
        int index = 0;
        int z = 0;
        int xOffset = 0;
        for (int p = 0; p < pageCount; p++)
        {
            xOffset = p * xOffsetConstant;
            for (int x = 0; x < pages.columnsPerPage; x++)
            {
                for (int y = 0; y > -pages.rowsPerPage; y--)
                {
                    if (index >= listSize) // we completed!
                    {
                        break;
                    }
                    cellPositions[index] = new Vector3Int(x + xOffset, y, z);
                    index++;
                }
            }
        }

        // Convert and Assign positions to the list items
        index = 0;

        foreach (Transform child in theList.transform)
        {
            child.transform.position = GetComponent<Grid>().GetCellCenterWorld(cellPositions[index]);
            if (index < pages.ItemCountPerPage)
            {
                firstPageStartPosition += child.transform.position;
            }
            index++;
        }
        firstPageStartPosition /= pages.ItemCountPerPage;

        GeneratePageCenters();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Find page count according to the list
    /// </summary>
    private void CalculatePageCount()
    {
        int listSize = theList.transform.childCount;
        int divider = pages.ItemCountPerPage;
        pageCount = listSize / divider;
        int remainder = pageCount % divider;
        if (remainder > 0)
        {
            pageCount++;
        }
    }

    /// <summary>
    /// Generate all the center positions of the pages as gameobjects under the gameobject PAGES
    /// This process is only needed to ui camera system for now
    /// </summary>
    private void GeneratePageCenters()
    {
        int xCameraOffset = 6;  // This is completely dependent on grid size, this should be calculated to improve/widen the code
        for (int x = 0; x < pageCount; x++)
        {
            Vector2 position = new Vector2();
            if (pages.ItemCountPerPage == 1) // one object per page, we can simply take object center...
            {
                position = theList.transform.GetChild(x).position;
            }
            else
            {
                position.x = firstPageStartPosition.x + x * xCameraOffset;
                position.y = firstPageStartPosition.y;
            }
            GameObject go = new GameObject();
            go.transform.position = position;
            go.name = pages.rootName + x;
            go.transform.parent = cameraPositions.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

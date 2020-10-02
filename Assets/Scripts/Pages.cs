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

public class Pages : MonoBehaviour
{
    public int columnsPerPage = 2;
    public int rowsPerPage = 4;
    public string rootName = "PAGE";

    private Dictionary<int, List<GameObject>> pages = new Dictionary<int, List<GameObject>>();

    private int pageNumber = 0;

    public int ItemCountPerPage
    {
        get
        {
            return columnsPerPage * rowsPerPage;
        }
    }

    public int PageNumber
    {
        get
        {
            return pageNumber;
        }

        set
        {
            if (pageNumber < 0)
            {
                pageNumber = 0;
            }
            if (pageNumber >= pages.Count)
            {
                pageNumber = pages.Count - 1;
            }
            pageNumber = value;
        }
    }

    public List<GameObject> Page(int i)
    {
        List<GameObject> result = null;
        pages.TryGetValue(i, out result);
        return result;
    }

    public List<GameObject> ActivePage()
    {
        return Page(pageNumber);
    }

    public bool TryGetValue(int key, out List<GameObject> value)
    {
        return pages.TryGetValue(key, out value);
    }

    public void Add(int key, List<GameObject> value)
    {
        pages.Add(key, value);
    }

    // Use this for initialization
    void Start()
    {
        EventManager.StartListen((int)Events.CAMERA_MOVED_TO_PAGE, Reset);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset(int id)
    {
        if (id == 0)
        {
            PageNumber = 0;
        }
    }

    public bool MoveToNextPage()
    {
        int tmp = PageNumber;
        PageNumber++;
        if (tmp != PageNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MoveToPreviousPage()
    {
        int tmp = PageNumber;
        PageNumber--;
        if (tmp != PageNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsFirstPage()
    {
        return PageNumber == 0;
    }

    public bool IsLastPage()
    {
        return PageNumber == pages.Count - 1;
    }
}

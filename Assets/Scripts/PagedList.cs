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
namespace GameUI
{
    public class PagedList : MonoBehaviour
    {

        public int ColumnsPerPage = 3;
        public int RowsPerPage = 3;
        public GameObject TheList;

        // Use this for initialization
        void Start()
        {
            InitializePage(0);
            InitializePage(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber">Starts from 0. We simply add this to x axis.</param>
        private void InitializePage(int pageNumber)
        {
            float clipping = 0.1f; // 0 <= clipping <= 1
                                   // In viewport unit
                                   // The bottom-left of the viewport is (0,0); the top-right is (1,1)
            float cellWidth = (1.0f - clipping) / ColumnsPerPage;
            float cellHeight = (1.0f - clipping) / RowsPerPage;

            float xOffset = cellWidth / 2;
            float yOffset = cellHeight / 2;

            for (int x = 0; x < ColumnsPerPage; x++)
            {
                float xValue = x * cellWidth + xOffset;
                for (int y = 0; y < RowsPerPage; y++)
                {
                    float yValue = y * cellHeight + yOffset;
                    GameObject go = new GameObject();
                    go.AddComponent<MoveTo>();
                    go.GetComponent<MoveTo>().xPosition = xValue + clipping + pageNumber;
                    go.GetComponent<MoveTo>().yPosition = yValue + clipping;
                    go.name = x + "," + y;
                    go.transform.parent = transform;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
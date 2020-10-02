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
    public class PagesController : MonoBehaviour
    {

        public GameObject nextPageButton;
        public GameObject previousPageButton;
        public GameObject buttons;
        public GameObject grid;
        public GameObject animals;
        public Pages pages;


        private UICameraSystem uiCameraSystem;
        private GridList gridList;

        // Use this for initialization
        void Start()
        {
            uiCameraSystem = Camera.main.GetComponent<UICameraSystem>();
            gridList = grid.GetComponent<GridList>();

            EventManager.StartListen((int)Events.NEXT_ANIMAL_PAGE_CLICKED, NextPage);
            EventManager.StartListen((int)Events.PREVIOUS_ANIMAL_PAGE_CLICKED, PreviousPage);
            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_PAGE, PageRelatedJobs);
            EventManager.StartListen((int)Events.EXIT_BUTTON_CLICKED, Reset);
            EventManager.StartListen((int)Events.ANIMAL_CLICKED, ResetWith);
        }

        private void PageRelatedJobs(int pageId)
        {
            EnableButtons();
            if (pageId == 0)
            {
                DisablePrevious();
            }
            else if (pageId == gridList.PageCount - 1)
            {
                DisableNext();
            }
        }

        private void Reset()
        {
            buttons.SetActive(false);
        }

        private void ResetWith(GameObject go)
        {
            Reset();
        }

        private void DisablePrevious()
        {
            previousPageButton.SetActive(false);
        }

        private void DisableNext()
        {
            nextPageButton.SetActive(false);
        }

        private void DisableButtons()
        {
            buttons.SetActive(false);
        }

        private void EnableButtons()
        {
            buttons.SetActive(true);
            nextPageButton.SetActive(true);
        }

        private void PreviousPage()
        {
            nextPageButton.SetActive(true);
            if (pages.MoveToPreviousPage())
            {
                if (pages.IsFirstPage())
                {
                    previousPageButton.SetActive(false);
                }
                uiCameraSystem.MoveTo("PAGE" + pages.PageNumber);
            }

        }

        private void NextPage()
        {
            previousPageButton.SetActive(true);
            if (pages.MoveToNextPage())
            {
                if (pages.IsLastPage())
                {
                    nextPageButton.SetActive(false);
                }
                uiCameraSystem.MoveTo("PAGE" + pages.PageNumber);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
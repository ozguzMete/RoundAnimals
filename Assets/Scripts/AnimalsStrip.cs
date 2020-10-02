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
    public class AnimalsStrip : MonoBehaviour
    {
        public GameObject nextPageButton;
        public GameObject previousPageButton;
        public GameObject buttons;
        public GameObject cameraPositions;
        public GameObject animals;

        private GameObject nextAnimal;
        private GameObject previousAnimal;
        private GameObject selectedAnimal;
        private Dictionary<int, List<GameObject>> animalsStrip = new Dictionary<int, List<GameObject>>();

        public static int activeAnimalId = 0;

        // Use this for initialization
        void Start()
        {
            nextAnimal = GameObject.Find("/UICameraSystem/CameraPositions/NextAnimal");
            previousAnimal = GameObject.Find("/UICameraSystem/CameraPositions/PreviousAnimal");
            selectedAnimal = GameObject.Find("/UICameraSystem/CameraPositions/SelectedAnimal");

            EventManager.StartListen((int)Events.NEXT_ANIMAL_CLICKED, NextAnimal);
            EventManager.StartListen((int)Events.PREVIOUS_ANIMAL_CLICKED, PreviousAnimal);

            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_ANIMAL, EnableOrDisable);

            EventManager.StartListen((int)Events.EXIT_BUTTON_CLICKED, Reset);

            EventManager.StartListen((int)Events.ANIMAL_CLICKED, MoveAnimal);
            EventManager.StartListen((int)Events.ANIMAL_SOUND_CLICKED, PlayAnimalAnimation);

            PopulatePages();
        }

        private void PlayAnimalAnimation()
        {
            Animal target;
            if (Find.Downward<Animal>(animals.transform.GetChild(activeAnimalId), out target) != null)
            {
                target.OpenCloseMouth();
            }
        }

        private void MoveAnimal(GameObject animal)
        {
            activeAnimalId = animal.transform.GetSiblingIndex();
            animals.transform.GetChild(activeAnimalId).GetComponent<SmoothFollow2D>().target = selectedAnimal.transform;
        }

        private void PopulatePages()
        {
            int page = 0;
            int count = 1;
            List<GameObject> pageItems = new List<GameObject>();
            foreach (Transform animal in animals.transform)
            {
                pageItems.Add(animal.transform.gameObject);
                if (count % 1 == 0)
                {
                    animalsStrip.Add(page, pageItems);
                    page++;
                    pageItems = new List<GameObject>();
                }
                count++;
            }
            if (pageItems.Count != 0)
            {
                animalsStrip.Add(page, pageItems);
                page++;
            }
        }

        private void EnableAnimalsOnPage0()
        {
            SetAnimalsActiveOnPage(0, true);
        }

        private void SetAnimalsActiveOnPage(int pageId, bool active)
        {
            List<GameObject> pageItems;
            animalsStrip.TryGetValue(pageId, out pageItems);

            foreach (GameObject animal in pageItems)
            {
                animal.SetActive(active);
            }
        }

        private void Reset()
        {
            activeAnimalId = 0;
            buttons.SetActive(false);
        }

        private void DisableButtons()
        {
            buttons.SetActive(false);
        }

        private void EnableOrDisable()
        {
            EnableButtons();
            if (activeAnimalId == animals.transform.childCount - 1)
            {
                nextPageButton.SetActive(false);
            }
            else if (activeAnimalId == 0)
            {
                previousPageButton.SetActive(false);
            }
        }

        private void EnableButtons()
        {
            buttons.SetActive(true);
            nextPageButton.SetActive(true);
            previousPageButton.SetActive(true);
        }

        private void PreviousAnimal()
        {
            nextPageButton.SetActive(true);
            if (activeAnimalId > 0)
            {
                animals.transform.GetChild(activeAnimalId).GetComponent<SmoothFollow2D>().target = nextAnimal.transform;
                activeAnimalId--;
                if (activeAnimalId == 0)
                {
                    previousPageButton.SetActive(false);
                }
                animals.transform.GetChild(activeAnimalId).position = previousAnimal.transform.position;
                animals.transform.GetChild(activeAnimalId).GetComponent<SmoothFollow2D>().target = selectedAnimal.transform;
                EventManager.Trigger((int)Events.TIME_TO_DECIDE_AD_DISPLAY);
            }
        }

        private void NextAnimal()
        {
            previousPageButton.SetActive(true);
            if (activeAnimalId < animals.transform.childCount - 1)
            {
                animals.transform.GetChild(activeAnimalId).GetComponent<SmoothFollow2D>().target = previousAnimal.transform;
                activeAnimalId++;
                if (activeAnimalId == animals.transform.childCount - 1)
                {
                    nextPageButton.SetActive(false);
                }
                animals.transform.GetChild(activeAnimalId).position = nextAnimal.transform.position;
                animals.transform.GetChild(activeAnimalId).GetComponent<SmoothFollow2D>().target = selectedAnimal.transform;
                EventManager.Trigger((int)Events.TIME_TO_DECIDE_AD_DISPLAY);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
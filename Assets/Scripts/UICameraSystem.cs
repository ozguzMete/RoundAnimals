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
using System.Collections.Generic;
using UnityEngine;
namespace GameUI
{
    public class UICameraSystem : MonoBehaviour
    {

        public GameObject parentOfPositions;

        private Dictionary<string, GameObject> positions = new Dictionary<string, GameObject>();
        private GameObject target = null;

        private SmoothFollow2D smoothFollow2D;

        // Use this for initialization
        void Start()
        {
            PopulatePositions(parentOfPositions);
            smoothFollow2D = Camera.main.GetComponent<SmoothFollow2D>();
            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_ANIMAL, DecreaseOrthographicSize);
            EventManager.StartListen((int)Events.EXIT_BUTTON_CLICKED, IncreaseOrthographicSize);
            EventManager.StartListen((int)Events.ANIMAL_CLICKED, MoveToSelectedAnimal);
        }

        public void PopulatePositions(GameObject go)
        {
            // create name-gameobject pairs for each level-1 children
            foreach (Transform child in go.transform)
            {
                positions.Add(child.gameObject.name, child.gameObject);
            }
        }

        public void MoveToSelectedAnimal(GameObject go)
        {
            MoveTo("SelectedAnimal");
        }

        void Update()
        {
            if (target != null)
            {
                Vector3 targetPosition = target.transform.position;
                if (targetPosition.x >= Camera.main.transform.position.x - 1.0 && targetPosition.x <= Camera.main.transform.position.x + 1.0
                    && targetPosition.y >= Camera.main.transform.position.y - 1.0 && targetPosition.y <= Camera.main.transform.position.y + 1.0)
                {
                    // we arrived our target position!                  
                    if (target.name.Contains("PAGE"))
                    {
                        EventManager.Trigger((int)Events.CAMERA_MOVED_TO_PAGE, int.Parse(target.name[target.name.Length - 1].ToString()));
                    }
                    else if (target.name.Equals("Main"))
                    {
                        EventManager.Trigger((int)Events.CAMERA_MOVED_TO_MAIN);
                    }
                    else if (target.name.Equals("Info"))
                    {
                        EventManager.Trigger((int)Events.CAMERA_MOVED_TO_INFO);
                    }
                    else
                    {
                        EventManager.Trigger((int)Events.CAMERA_MOVED_TO_ANIMAL);
                    }
                    target = null;
                }
            }
        }

        public void MoveTo(string positionName)
        {
            if (parentOfPositions.transform.childCount != positions.Count)
            {
                positions = new Dictionary<string, GameObject>();
                PopulatePositions(parentOfPositions);
            }
            positions.TryGetValue(positionName, out target);

            smoothFollow2D.target = target.transform;

        }

        private void DecreaseOrthographicSize()
        {
            Camera.main.orthographicSize = 1.63f;
        }

        private void IncreaseOrthographicSize()
        {
            Camera.main.orthographicSize = 5.0f;
        }
    }
}
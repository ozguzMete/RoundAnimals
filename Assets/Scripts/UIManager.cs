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
using MeteOzguz.RoundAnimals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameUI
{
    public class UIManager : MonoBehaviour
    {

        public GameObject Info;
        public GameObject Pages;
        public GameObject Popup;

        private UICameraSystem uiCameraSystem;
        public static ActiveUI activeUI = ActiveUI.MAIN;
        private PopupController popupController;

        public enum ActiveUI
        {
            MAIN,
            INFO,
            PAGES,
            ANIMAL
        }

        // Use this for initialization
        void Start()
        {
            uiCameraSystem = Camera.main.GetComponent<UICameraSystem>();
            popupController = Popup.GetComponent<PopupController>();

            EventManager.StartListen((int)Events.INFO_BUTTON_CLICKED, InfoButtonClicked);
            EventManager.StartListen((int)Events.PLAY_BUTTON_CLICKED, PlayButtonClicked);
            EventManager.StartListen((int)Events.RATEUS_BUTTON_CLICKED, RateUsButtonClicked);
            EventManager.StartListen((int)Events.FACEBOOK_BUTTON_CLICKED, FacebookButtonClicked);
            EventManager.StartListen((int)Events.TWITTER_BUTTON_CLICKED, TwitterButtonClicked);
            EventManager.StartListen((int)Events.INFO_BUTTON_CLICKED, StartCoroutineOpenInfoCanvasAfterHalfSec);
            EventManager.StartListen((int)Events.EXIT_BUTTON_CLICKED, DecideExitAction);
            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_PAGE, ActiveUItoPages);
            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_ANIMAL, ActiveUItoAnimal);
            EventManager.StartListen((int)Events.CAMERA_MOVED_TO_INFO, OpenInfoCanvas);
            EventManager.StartListen((int)Events.CONNECTED_TO_NET, DecideAccordingConnectionStatus);

            //GOTOs
            EventManager.StartListen((int)Events.GOTO_MAIN, GotoMain);
        }

        private void StartCoroutineOpenInfoCanvasAfterHalfSec()
        {
            StartCoroutine(OpenInfoCanvasAfterHalfSec());
        }
        
        private void RateUsButtonClicked()
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.MeteOzguz.RoundAnimals");
        }

        private void TwitterButtonClicked()
        {
            Application.OpenURL("https://twitter.com/MeteOzguz");
        }

        private void FacebookButtonClicked()
        {
            Application.OpenURL("https://fb.me/roundAnimalsApp");
        }

        private void DecideAccordingConnectionStatus(int connectionStatus)
        {
            if (connectionStatus <= 0) // Connection NOK
            {
                popupController.TakeAction(PopupController.Action.NO_CONNECTION);
            }
        }

        private void ActiveUItoPages(int id)
        {
            activeUI = ActiveUI.PAGES;
        }

        private void ActiveUItoAnimal()
        {
            activeUI = ActiveUI.ANIMAL;
        }

        private void PlayButtonClicked()
        {
            activeUI = ActiveUI.PAGES;
            uiCameraSystem.MoveTo("PAGE0");
        }

        private void DecideExitAction()
        {
            switch (activeUI)
            {
                case ActiveUI.MAIN:
                    break;
                default:
                    GotoMain();
                    break;
            }
        }

        private void GotoMain()
        {
            activeUI = ActiveUI.MAIN;
            uiCameraSystem.MoveTo("Main");
            DisableAllOtherUIs();
        }

        private void DisableAllOtherUIs()
        {
            Info.SetActive(false);
        }

        private IEnumerator OpenInfoCanvasAfterHalfSec()
        {
            yield return new WaitForSeconds(0.5f);
            OpenInfoCanvas();
        }

        private void OpenInfoCanvas()
        {
            Info.SetActive(true);
            activeUI = ActiveUI.INFO;
        }

        private void InfoButtonClicked()
        {
            uiCameraSystem.MoveTo("Info");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
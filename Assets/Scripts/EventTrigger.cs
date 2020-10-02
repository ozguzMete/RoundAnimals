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
namespace MeteOzguz.RoundAnimals
{
    public class EventTrigger : MonoBehaviour
    {
        [Tooltip("DISABLING enable/disable mechanism through shadowing & const")]
        public new const bool enabled = true; // DO NOT USE THIS VARIABLE ANYWHERE!

        public bool addSelf = false;
        public GameObject gameObjectToPass = null;

        [Tooltip("You can't disable this component if it is checked!")]
        public bool alwaysOn = false;

        public Events[] events = new Events[0];
        private bool isPopupMember = false;

        public bool Enabled
        {
            get
            {
                return base.enabled;
            }

            set
            {
                if (alwaysOn)
                {
                    base.enabled = true;
                }
                else
                {
                    base.enabled = value;
                }
            }
        }

        private void Awake()
        {
            if (base.enabled == false)
            {
                Enabled = false;
            }
        }

        // Use this for initialization
        public virtual void Start()
        {
            GameObject devNull;
            if (Find.Upward(gameObject, "Popup", out devNull))
            {
                isPopupMember = true;
            }
            if (addSelf && gameObjectToPass == null)
            {
                gameObjectToPass = gameObject;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void OnMouseDown()
        {
            if (!Enabled)
            {
                return;
            }
            if (Game.GameData.gameState.currentState == GameState.State.IN_POPUP)
            {
                if (isPopupMember == false)
                {
                    //Debug.Log("Not a popup member: " + gameObject.name);
                    return;
                }
            }
            foreach (int eventName in events)
            {
                //print(eventName);
                if (gameObjectToPass != null)
                {
                    EventManager.Trigger(eventName, gameObjectToPass);
                }
                else
                {
                    EventManager.Trigger(eventName);
                }
            }
        }
    }
}
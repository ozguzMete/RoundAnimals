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

public class EventManager : MonoBehaviour {

    private static Dictionary<int, List<Action>> listeners = new Dictionary<int, List<Action>>();
    private static Dictionary<int, List<Action<int>>> listenersWithInt = new Dictionary<int, List<Action<int>>>();
    private static Dictionary<int, List<Action<GameObject>>> listenersWithGameObject = new Dictionary<int, List<Action<GameObject>>>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void StartListen(int eventName, Action action)
    {
        List<Action> actions = null;
        if (listeners.TryGetValue(eventName, out actions))
        {
            actions.Add(action);
        }
        else
        {
            actions = new List<Action>();
            actions.Add(action);
            listeners.Add(eventName, actions);
        }
    }

    public static void StartListen(int eventName, Action<int> action)
    {
        List<Action<int>> actions = null;
        if (listenersWithInt.TryGetValue(eventName, out actions))
        {
            actions.Add(action);
        }
        else
        {
            actions = new List<Action<int>>();
            actions.Add(action);
            listenersWithInt.Add(eventName, actions);
        }
    }

    public static void StartListen(int eventName, Action<GameObject> action)
    {
        List<Action<GameObject>> actions = null;
        if (listenersWithGameObject.TryGetValue(eventName, out actions))
        {
            actions.Add(action);
        }
        else
        {
            actions = new List<Action<GameObject>>();
            actions.Add(action);
            listenersWithGameObject.Add(eventName, actions);
        }
    }

    public static void StopListen(int eventName, Action action)
    {
        List<Action> actions = null;
        if (listeners.TryGetValue(eventName, out actions))
        {
            actions.Remove(action);
        }
    }

    public static void StopListen(int eventName, Action<int> action)
    {
        List<Action<int>> actions = null;
        if (listenersWithInt.TryGetValue(eventName, out actions))
        {
            actions.Remove(action);
        }
    }

    public static void StopListen(int eventName, Action<GameObject> action)
    {
        List<Action<GameObject>> actions = null;
        if (listenersWithGameObject.TryGetValue(eventName, out actions))
        {
            actions.Remove(action);
        }
    }

    public static void Trigger(int eventName)
    {
        //Debug.Log("Triggering event, name: " + eventName);
        List<Action> actions = null;
        if (listeners.TryGetValue(eventName, out actions))
        {
            foreach (Action action in actions)
            {
                action();
            }
        }
    }

    public static void Trigger(int eventName, int arg)
    {
        //Debug.Log("Triggering event, name: " + eventName + " int: " + arg);
        List<Action<int>> actions = null;
        if (listenersWithInt.TryGetValue(eventName, out actions))
        {
            foreach (Action<int> action in actions)
            {
                action(arg);
            }
        }
    }

    public static void Trigger(int eventName, GameObject arg)
    {
        //Debug.Log("Triggering event, name: " + eventName + " gameobject: " + arg.name);
        List<Action<GameObject>> actions = null;
        if (listenersWithGameObject.TryGetValue(eventName, out actions))
        {
            foreach (Action<GameObject> action in actions)
            {
                action(arg);
            }
        }
    }
}

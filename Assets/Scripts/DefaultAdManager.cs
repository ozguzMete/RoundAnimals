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
using GameUI;
using MeteOzguz.RoundAnimals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Rewarded videos are first class citizens, 
/// videos are second class citizens ALWAYS
/// 
/// Add game specific events/behaviours only to concrete classes like this one!
/// 
/// </summary>
public class DefaultAdManager : MonoBehaviour
{
    public LockedAnimals lockedAnimals;
    public GameObject animals;

    private int clicksPerAd;
    public int showAdPerAnimalsCounter = 0;
    private GameObject targetAnimal;
    private ShowOptions options;
    private void Awake()
    {
        // Advertisement.Initialize("2850768");
    }

    // Use this for initialization
    void Start()
    {
        clicksPerAd = lockedAnimals.Count() * 2;
        options = new ShowOptions { resultCallback = HandleShowResult };
    }

    private void OnEnable()
    {
        EventManager.StartListen((int)Events.CUTELOCK_CLICKED, cutelockClicked);
        EventManager.StartListen((int)Events.TIME_TO_DECIDE_AD_DISPLAY, DecideAdDisplay);
    }

    private void OnDisable()
    {
        EventManager.StopListen((int)Events.CUTELOCK_CLICKED, cutelockClicked);
        EventManager.StopListen((int)Events.TIME_TO_DECIDE_AD_DISPLAY, DecideAdDisplay);
    }

    private void DecideAdDisplay()
    {
        showAdPerAnimalsCounter++;
        Transform animal = animals.transform.GetChild(AnimalsStrip.activeAnimalId);
        if (animal != null)
        {
            if (showAdPerAnimalsCounter >= clicksPerAd)
            {
                showAdPerAnimalsCounter = 0;
                ShowAd(animal.gameObject);
            }
            else
            {
                EventManager.Trigger((int)Events.AD_SKIPPED, animal.gameObject);
            }
        }
    }

    private void cutelockClicked(GameObject go)
    {
        showAdPerAnimalsCounter = 0;
        ShowAd(go);
    }

    private void ShowAd(GameObject go)
    {
        targetAnimal = go;
        if (Advertisement.isInitialized)
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video", options);
            }
            else if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo", options);
            }
            else
            {
                EventManager.Trigger((int)Events.AD_FAILED, targetAnimal);
            }
        }
        else
        {
            //Debug.Log("Advertisement is not initialized!");
            EventManager.Trigger((int)Events.AD_FAILED, targetAnimal);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                // Debug.Log("Video completed. Offer a reward to the player.");
                EventManager.Trigger((int)Events.AD_FINISHED, targetAnimal);
                break;
            case ShowResult.Skipped:
                // Debug.LogWarning("Video was skipped.");
                EventManager.Trigger((int)Events.AD_SKIPPED, targetAnimal);
                break;
            case ShowResult.Failed:
                // Debug.LogError("Video failed to show.");
                EventManager.Trigger((int)Events.AD_FAILED, targetAnimal);
                break;
        }
    }
}

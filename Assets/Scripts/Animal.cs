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

public class Animal : MonoBehaviour
{

    public AudioClip[] sounds = new AudioClip[3];
    public string mouthAnimation = "OpenCloseMouth";
    private int soundId = 0;
    protected AudioSource audioSource = null;
    protected Animator animator;
    private Animation anim;
    private float[] mouthAnimationSpeedMultipliersPerSound;

    public void Blink()
    {
        animator.SetTrigger("Blink");
    }

    // Use this for initialization
    public virtual void Start()
    {
        mouthAnimationSpeedMultipliersPerSound = new float[sounds.Length];
        Find.Upward<AudioSource>(transform, out audioSource);
        animator = GetComponent<Animator>();

        SyncAnimationLengthWithAudioLength();
    }

    private void SyncAnimationLengthWithAudioLength()
    {
        float animationLength = 1.0f;
        foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
        {
            if (animationClip.name.Equals(mouthAnimation))
            {
                animationLength = animationClip.length;
                break;
            }
        }
        int index = 0;
        float soundLength = 1.0f;
        foreach (AudioClip sound in sounds)
        {
            if (sound != null)
            {
                soundLength = sound.length;
            }
            else
            {
                soundLength = 1.0f;
            }
            mouthAnimationSpeedMultipliersPerSound[index] = animationLength / soundLength;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OpenCloseMouth()
    {
        animator.SetTrigger(mouthAnimation);
        if (sounds.Length == 0)
        {
            animator.SetFloat("OpenCloseMouthSpeedMultiplier", 1.0f);
        }
        else
        {
            animator.SetFloat("OpenCloseMouthSpeedMultiplier", mouthAnimationSpeedMultipliersPerSound[soundId]);
        }
    }

    public void PlayOpenCloseMouthSound()
    {
        if (audioSource != null && audioSource.enabled == true && sounds.Length != 0)
        {
            audioSource.PlayOneShot(sounds[soundId]);
            soundId++;
            soundId = soundId % sounds.Length;
        }
    }

    public void EatingFinished()
    {
        animator.SetTrigger("FinishEating");
    }

    public void EatingStarted()
    {
        animator.SetTrigger("StartEating");
    }

    private void OnMouseDown()
    {
        //Debug.Log("Animal.OnMOuseDown");
        if (GameUI.UIManager.activeUI == GameUI.UIManager.ActiveUI.ANIMAL)
        {
            OpenCloseMouth();
        }
    }
}

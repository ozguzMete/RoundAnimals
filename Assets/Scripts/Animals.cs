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

public static class Animals
{
    public static string NameToString(string name)
    {
        string result = "";
        int token = 0;
        foreach (char c in name)
        {
            token = c;
            result += token + "0";
        }
        //Debug.Log("NameToString( " + name + " ) = " + result);
        return result;
    }

    public static int NameToInt(string name)
    {
        int result = 0;
        int multiplier = 1;
        foreach (char c in name)
        {
            result += c * multiplier;
            multiplier++;
        }
        //Debug.Log("NameToInt( " + name + " ) = " + result);
        return result;
    }

    public static bool IsLocked(string name)
    {
        int animalVal = PlayerPrefs.GetInt(NameToString(name), 0);
        if (animalVal != NameToInt(name)) // Locked
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public static void Unlock(string name)
    {
        PlayerPrefs.Save();
        //Debug.Log("Saving Playerfabs: " + NameToString(name) + " / " + NameToInt(name));
        PlayerPrefs.SetInt(NameToString(name), NameToInt(name));
    }


}


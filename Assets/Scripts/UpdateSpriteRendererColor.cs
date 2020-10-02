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

[ExecuteInEditMode]
public class UpdateSpriteRendererColor : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float red = 0.0f;

    [Range(0.0f, 1.0f)]
    public float green = 0.0f;

    [Range(0.0f, 1.0f)]
    public float blue = 0.0f;

    [Range(0.0f, 1.0f)]
    public float alpha = 1.0f;

    private bool valueChanged = false;

    private SpriteRenderer[] spriteRenderers;
    private Color[] spriteRenderersColors;

    public float Alpha
    {
        get
        {
            return alpha;
        }

        set
        {
            valueChanged = true;
            alpha = value;
        }
    }

    public float Blue
    {
        get
        {
            return blue;
        }

        set
        {
            valueChanged = true;
            blue = value;
        }
    }

    public float Green
    {
        get
        {
            return green;
        }

        set
        {
            valueChanged = true;
            green = value;
        }
    }

    public float Red
    {
        get
        {
            return red;
        }

        set
        {
            valueChanged = true;
            red = value;
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        if (valueChanged)
        {
            Make();
            valueChanged = false;
        }
    }

    public void Make(Color color)
    {
        Red = color.r;
        Green = color.g;
        Blue = color.b;
        Alpha = color.a;
        Make();
    }

    public void Make()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spriteRenderersColors = new Color[spriteRenderers.Length];
        Color color = new Color(Red, Green, Blue, Alpha);
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderersColors[i] = spriteRenderers[i].color;
            spriteRenderers[i].color = color;
        }
    }

    public void Reset()
    {
        //Make(Color.white);
        Unmake();
    }

    private void Unmake()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = spriteRenderersColors[i];
        }
    }
}

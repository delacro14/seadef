﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBtn : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip ClickFx;

    public void ClickSound()
    {
        myFx.PlayOneShot(ClickFx);
    }
}

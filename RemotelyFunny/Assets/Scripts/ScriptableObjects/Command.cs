﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scriptable object for the commands
/// </summary>
[CreateAssetMenu(menuName = "Command")]
public class Command : ScriptableObject
{
    public enum Commands
    {
        // TV Commands
        VolUp,
        VolDown,
        ChangeCh1,
        ChangeCh2,
        ChangeCh3,
        ChangeChUpOne,
        ChangeChDownOne,
        mute,

        // DVR Commands
        Pause,
        REC,
        Play,
        FF,
        Rewind,

        // Blender Commands
        SlowTomato,
        MediumTomato,
        FastTomato,
        SlowFishHead,
        MediumFishHead,
        FastFishHead,
        SlowIce,
        MediumIce,
        FastIce,
        SlowStrawberry,
        MediumStrawberry,
        FastStrawberry,
        SlowBanana,
        MediumBanana,
        FastBanana
    }

    public string commandDisplay; // How the command will be displayed in game
    public Commands command; // Command enum that lets us know what to look for
}

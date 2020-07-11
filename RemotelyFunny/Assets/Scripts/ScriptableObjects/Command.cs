using System.Collections;
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
        VolUp,
        VolDown,
        ChangeCh1,
        ChangeCh2,
        ChangeCh3,
        ChangeChUpOne,
        ChangeChDownOne,
        mute
    }

    public string commandDisplay; // How the command will be displayed in game
    public Commands command; // Command enum that lets us know what to look for
}

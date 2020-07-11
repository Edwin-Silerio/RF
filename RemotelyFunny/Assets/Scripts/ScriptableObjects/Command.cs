using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Command : ScriptableObject
{
    public enum Commands
    {
        remoteVolUp,
        remoteVolDown,
        remoteCh1,
        remoteCh2,
        remoteCh3,
        mute
    }

    public TextMeshProUGUI commandDisplay; // How the command will be displayed in game
    public Commands command; // Command enum that lets us know what to look for

}

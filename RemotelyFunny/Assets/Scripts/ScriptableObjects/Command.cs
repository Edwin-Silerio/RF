using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds all the information for a command such as the name, the text it will 
/// display and the target value (e.g. target channel number that the player 
/// must enter) if there is one.
/// </summary>
[CreateAssetMenu(menuName = "Command")]
public class Command : ScriptableObject
{
    // Used when the player must hit a target value e.g target channel number
    // that the player must enter. Not every command will have a target value
    [SerializeField] private int targetValue = -1;
    [SerializeField] private string commandTextDisplay; 
    [SerializeField] private Commands command;


    // Getters
    public string CommandTextDisplay => commandTextDisplay;
    public Commands CommandName => command;
    public int TargetValue => targetValue;

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

}

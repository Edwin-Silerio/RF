using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TVRemote : MonoBehaviour, Remote
{
    [SerializeField] private GameObject tvRemote = default;
    [SerializeField] private GameObject tableTVRemote = default;
    [SerializeField] private int[] channels = default;

    private Command currCommand;
    public int currChannel = 123;
    public int currVolume = 2;
    private int targChannel;
    private int targVolume;
    private int numsNeeded = 0;
    private int currNums = 0;
    private int buildChannelNum = 0;


    private void Update()
    {
        if(currCommand != GameManager.CurrCommand) {
            Reset();
        }
    }

    public void Reset()
    {
        if (channels.Length < 3)
        {
            Debug.LogError("channels list has less than 3 channels");
            return;
        }

        currCommand = GameManager.CurrCommand;
        Debug.Log($"Current command: {currCommand.commandDisplay}");
        switch (currCommand.command)
        {
            case Command.Commands.VolUp:
                targVolume = currVolume + 1;
                break;
            case Command.Commands.VolDown:
                targVolume = currVolume - 1;
                break;
            case Command.Commands.ChangeChUpOne:
                targChannel = currChannel + 1;
                break;
            case Command.Commands.ChangeChDownOne:
                targChannel = currChannel - 1;
                break;
            case Command.Commands.mute:
                targVolume = 0;
                break;
            case Command.Commands.ChangeCh1:
                targChannel = channels[0];
                numsNeeded = 1;
                break;
            case Command.Commands.ChangeCh2:
                targChannel = channels[1];
                numsNeeded = 2;
                break;
            case Command.Commands.ChangeCh3:
                targChannel = channels[2];
                numsNeeded = 3;
                break;
        }
    }

    public void ShowLargeRemote()
    {
        //Debug.Log("Showing large remote");
        tableTVRemote.SetActive(false);
        tvRemote.SetActive(true);

        Reset();
    }

    public void ShowTableRemote()
    {
        //Debug.Log("Showing table remote");
        tvRemote.gameObject.SetActive(false);
        tableTVRemote.gameObject.SetActive(true);
    }

    /*
     * Checks which button was pressed. Please excuse the nasty code
     */
    public void RemoteButtonPressed(int button)
    {
        Debug.Log($"ButtonPressed: {(RemoteButtons)button}, curCommand: {currCommand.command}");
        switch ((RemoteButtons)button)
        {

            
            case RemoteButtons.ChannelUp:
                if (currCommand.command != Command.Commands.ChangeChUpOne)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                currChannel = targChannel;
                GameManager.CorrectAction();
                break;
            case RemoteButtons.ChannelDown:
                if (currCommand.command != Command.Commands.ChangeChDownOne)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                currChannel = targChannel;
                GameManager.CorrectAction();
                break;
            case RemoteButtons.VolumeUp:
                if (currCommand.command != Command.Commands.VolUp)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                currVolume = targVolume;
                GameManager.CorrectAction();
                break;
            case RemoteButtons.VolumeDown:
                if (currCommand.command != Command.Commands.VolDown)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                currVolume = targVolume;
                GameManager.CorrectAction();
                break;
            case RemoteButtons.Mute:
                if (currCommand.command != Command.Commands.mute)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                currVolume = 0;
                GameManager.CorrectAction();
                break;
            case RemoteButtons.One:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command == Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 1;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Two:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 2;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Three:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 3;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Four:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 4;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Five:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 5;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Six:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 6;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Seven:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 7;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Eight:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 8;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Nine:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 9;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.Zero:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum *= 10;
                Debug.Log($"buildChannelNum: {buildChannelNum}");
                currNums++;

                break;
            case RemoteButtons.OK:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2
                    && currCommand.command != Command.Commands.ChangeCh3 || currNums != numsNeeded)
                {
                    Debug.Log($"Currnums: {currNums}, numsNeeded: {numsNeeded}");
                    GameManager.DecreaseTime();
                    break;
                }

                else if (currNums == numsNeeded && buildChannelNum == targChannel)
                {
                    currNums = 0;
                    numsNeeded = 0;
                    buildChannelNum = 0;
                    currChannel = targChannel;
                    GameManager.CorrectAction();
                }
                break;
            default:
                Debug.LogError("Button doesn't exist");
                break;
        }
    }

    public enum RemoteButtons
    {
        ChannelUp,
        ChannelDown,
        VolumeUp,
        VolumeDown,
        Mute,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Zero,
        OK
    }
}



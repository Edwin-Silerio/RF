﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TVRemote : MonoBehaviour, IRemote
{
    #region Unity Variables
    [SerializeField] private GameObject tvRemote = default;
    [SerializeField] private GameObject tableTVRemote = default;
    [SerializeField] private GameManager gameManager = default;
    [SerializeField] private DVRRemote dvrRemote;
    [SerializeField] private BlenderRemote blenderRemote;
    #endregion

    // TV Class variables?
    [SerializeField] private TextMeshProUGUI channel = default;
    [SerializeField] private Slider volumeSlider = default;
    [SerializeField] private Animator tvAnimator = default;
    [SerializeField] private string[] animatorBools = default;
    [SerializeField] private int[] channels = default;
    [SerializeField] private int maxVolume = 5;

    #region Private Variables
    public int currChannel = 123;
    public int currVolume = 2;
    private int targChannel;
    private int targVolume;
    private int numsNeeded = 0;
    private int currNums = 0;
    private int buildChannelNum = 0;
    #endregion 

    private readonly int volumeUp = 1;
    private readonly int volumeDown = -1;
    private readonly int mute = 0;

    public GameObject GetTVRemote => tvRemote;
    public GameObject GetTableTVRemote => tableTVRemote;

    private void Start()
    {
        // Will be removed when the TV Class is made
        tvAnimator = GameObject.FindGameObjectWithTag("TV").GetComponent<Animator>();
        channel.text = currChannel.ToString();

        // Make sure exposed variables are set in the editor
        if (!tvRemote || !tableTVRemote || !gameManager || !dvrRemote || !blenderRemote)
        {
            Debug.LogError($"Exposed variables aren't set." +
                           $"tvRemote: {tvRemote}," +
                           $"tableTVRemote: {tableTVRemote}," +
                           $"gameManager: {gameManager}," +
                           $"dvrRemote: {dvrRemote}," +
                           $"blenderRemote: {blenderRemote}.");
        }

        // Make sure we have atleast 3 channels
        if (channels.Length < 3)
        {
            Debug.LogError("The channels list has less than 3 channels.");
        }


        ChangeChannel();
    }

    public void NextCommand()
    {

        Command currCommand = gameManager.CurrCommand;
        switch (currCommand.command)
        {
            case Command.Commands.ChangeChUpOne:
                targChannel = currChannel + 1;
                break;
            case Command.Commands.ChangeChDownOne:
                targChannel = currChannel - 1;
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
        tableTVRemote.SetActive(false);
        tvRemote.SetActive(true);
        HideOtherRemotes();
        NextCommand();
    }

    public void ShowTableRemote()
    {
        tvRemote.gameObject.SetActive(false);
        tableTVRemote.gameObject.SetActive(true);
        NextCommand();
    }

    public void ChangeChannel()
    {
        //Debug.Log($"currChannel: {currChannel}");
        channel.text = currChannel.ToString();
        switch (currChannel)
        {
            case 9:
                tvAnimator.SetBool(animatorBools[0], true);
                tvAnimator.SetBool(animatorBools[1], false);
                tvAnimator.SetBool(animatorBools[2], false);
                break;
            case 48:
                tvAnimator.SetBool(animatorBools[1], true);
                tvAnimator.SetBool(animatorBools[2], false);
                tvAnimator.SetBool(animatorBools[0], false);
                break;
            case 123:
                Debug.Log("In 123");
                tvAnimator.SetBool(animatorBools[2], true);
                tvAnimator.SetBool(animatorBools[0], false);
                tvAnimator.SetBool(animatorBools[1], false);
                break;
            default:
                tvAnimator.SetBool(animatorBools[0], false);
                tvAnimator.SetBool(animatorBools[1], false);
                tvAnimator.SetBool(animatorBools[2], false);
                break;

        }
    }

    public void ChangeVolume(int volumeMultiplier)
    {
        // Change volume up or down one 
        if(volumeMultiplier > 0)
        {
            volumeSlider.value += (1f/maxVolume) * volumeMultiplier;
        }
        // Mute
        else if(volumeMultiplier == 0)
        {
            volumeSlider.value = 0;
        }
    }

    /*
     * Checks which button was pressed. Please excuse the nasty code
     */
    public void RemoteButtonPressed(int button)
    {
        Command currCommand = gameManager.CurrCommand;
        //Debug.Log($"ButtonPressed: {(RemoteButtons)button}, curCommand: {currCommand.command}");
        switch ((RemoteButtons)button)
        {
            case RemoteButtons.ChannelUp:
                if (currCommand.command != Command.Commands.ChangeChUpOne)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                currChannel = targChannel;
                tvAnimator.speed = 1;
                ChangeChannel();
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.ChannelDown:
                if (currCommand.command != Command.Commands.ChangeChDownOne)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                currChannel = targChannel;
                tvAnimator.speed = 1;
                ChangeChannel();
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.VolumeUp:
                if (currCommand.command != Command.Commands.VolUp)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                ChangeVolume(volumeUp);
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.VolumeDown:
                if (currCommand.command != Command.Commands.VolDown)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                ChangeVolume(volumeDown);
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.Mute:
                if (currCommand.command != Command.Commands.mute)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                ChangeVolume(mute);
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.One:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command == Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 1;
                currNums++;

                break;
            case RemoteButtons.Two:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 2;
                currNums++;

                break;
            case RemoteButtons.Three:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 3;
                currNums++;

                break;
            case RemoteButtons.Four:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 4;
                currNums++;

                break;
            case RemoteButtons.Five:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 5;
                currNums++;

                break;
            case RemoteButtons.Six:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 6;
                currNums++;

                break;
            case RemoteButtons.Seven:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 7;
                currNums++;

                break;
            case RemoteButtons.Eight:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 8;
                currNums++;

                break;
            case RemoteButtons.Nine:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 9;
                currNums++;

                break;
            case RemoteButtons.Zero:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                buildChannelNum *= 10;
                currNums++;

                break;
            case RemoteButtons.OK:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2
                    && currCommand.command != Command.Commands.ChangeCh3 || currNums != numsNeeded)
                {
                    Debug.Log($"Currnums: {currNums}, numsNeeded: {numsNeeded}");
                    buildChannelNum = 0;
                    currNums = 0;
                    gameManager.DecreaseTime();
                    break;
                }

                else if (currNums == numsNeeded && buildChannelNum == targChannel)
                {
                    currNums = 0;
                    numsNeeded = 0;
                    buildChannelNum = 0;
                    currChannel = targChannel;
                    tvAnimator.speed = 1;
                    ChangeChannel();
                    gameManager.CorrectAction();
                    ShowTableRemote();
                }
                break;
            default:
                Debug.LogError("Button doesn't exist");
                break;
        }

    }

    public void HideOtherRemotes()
    {
        dvrRemote.GetDVRRemote.gameObject.SetActive(false);
        if(gameManager.GetDVR)
            dvrRemote.GetTableDVRRemote.gameObject.SetActive(true);
        blenderRemote.GetBlenderRemote.gameObject.SetActive(false);
        if(gameManager.GetBlender)
            blenderRemote.GetTableBlenderRemote.gameObject.SetActive(true);
    }

    /*
     * All of the buttons on the TV remote
     */
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



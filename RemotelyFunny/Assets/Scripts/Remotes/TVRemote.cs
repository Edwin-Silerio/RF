using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TVRemote : MonoBehaviour, Remote
{
    [SerializeField] private GameObject tvRemote = default;
    [SerializeField] private GameObject tableTVRemote = default;
    [SerializeField] private TextMeshProUGUI channel = default;
    [SerializeField] private Slider volumeSlider = default;
    [SerializeField] private Animator tvAnimator = default;
    [SerializeField] private string[] animatorBools = default;
    [SerializeField] private int[] channels = default;
    [SerializeField] private int maxVolume = 5;

    private Command currCommand;
    public int currChannel = 123;
    public int currVolume = 2;
    private int targChannel;
    private int targVolume;
    private int numsNeeded = 0;
    private int currNums = 0;
    private int buildChannelNum = 0;
    private DVRRemote dvrRemote;
    private BlenderRemote blenderRemote;

    private void Start()
    {
        channel.text = currChannel.ToString();
        dvrRemote = GetComponent<DVRRemote>();
        blenderRemote = GetComponent<BlenderRemote>();

        if (tvRemote == null || dvrRemote == null)
            Debug.LogError($"tvRemote: {blenderRemote}, dvrRemote: {dvrRemote}");

        ChangeChannel();
    }

    public void NextCommand()
    {
        if (channels.Length < 3)
        {
            Debug.LogError("channels list has less than 3 channels");
            return;
        }

        currCommand = GameManager.CurrCommand;
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
        tableTVRemote.SetActive(false);
        tvRemote.SetActive(true);
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
        Debug.Log($"currChannel: {currChannel}");
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

    public void ChangeVolume()
    {
        if (currVolume > maxVolume)
            currVolume = maxVolume;
        else if (currVolume < 0)
            currVolume = 0;

        volumeSlider.value += (1f/maxVolume) * targVolume;
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
                ChangeChannel();
                GameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.ChannelDown:
                if (currCommand.command != Command.Commands.ChangeChDownOne)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                currChannel = targChannel;
                ChangeChannel();
                GameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.VolumeUp:
                if (currCommand.command != Command.Commands.VolUp)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                targVolume = 1;
                ChangeVolume();
                GameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.VolumeDown:
                if (currCommand.command != Command.Commands.VolDown)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                targVolume = -1;
                ChangeVolume();
                GameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.Mute:
                if (currCommand.command != Command.Commands.mute)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                currVolume = 0;
                GameManager.CorrectAction();
                ShowTableRemote();
                break;
            case RemoteButtons.One:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command == Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 1;
                currNums++;

                break;
            case RemoteButtons.Two:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 2;
                currNums++;

                break;
            case RemoteButtons.Three:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 3;
                currNums++;

                break;
            case RemoteButtons.Four:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 4;
                currNums++;

                break;
            case RemoteButtons.Five:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 5;
                currNums++;

                break;
            case RemoteButtons.Six:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 6;
                currNums++;

                break;
            case RemoteButtons.Seven:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 7;
                currNums++;

                break;
            case RemoteButtons.Eight:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 8;
                currNums++;

                break;
            case RemoteButtons.Nine:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
                    break;
                }
                buildChannelNum = buildChannelNum * 10 + 9;
                currNums++;

                break;
            case RemoteButtons.Zero:
                if (currCommand.command != Command.Commands.ChangeCh1 && currCommand.command != Command.Commands.ChangeCh2 && currCommand.command != Command.Commands.ChangeCh3)
                {
                    GameManager.DecreaseTime();
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
                    GameManager.DecreaseTime();
                    break;
                }

                else if (currNums == numsNeeded && buildChannelNum == targChannel)
                {
                    currNums = 0;
                    numsNeeded = 0;
                    buildChannelNum = 0;
                    currChannel = targChannel;
                    ChangeChannel();
                    GameManager.CorrectAction();
                    ShowTableRemote();
                }
                break;
            default:
                Debug.LogError("Button doesn't exist");
                break;
        }
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



﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVRRemote : MonoBehaviour, IRemote
{
    [SerializeField] private GameObject dvrRemote = default;
    [SerializeField] private GameObject tableDVRRemote = default;
    [SerializeField] private Animator tvAnimator = default;
    [SerializeField] private GameObject recDot = default;
    [SerializeField] private GameManager gameManager = default;
    
    private Command currCommand = default;
    private TVRemote tvRemote;
    private BlenderRemote blenderRemote;

    public GameObject GetDVRRemote => dvrRemote;
    public GameObject GetTableDVRRemote => tableDVRRemote;

    private void Start()
    {
        tvRemote = GetComponent<TVRemote>();
        blenderRemote = GetComponent<BlenderRemote>();
        tvAnimator = GameObject.FindGameObjectWithTag("TV").GetComponent<Animator>();
        if (tvRemote == null || dvrRemote == null)
            Debug.LogError($"tvRemote: {tvRemote}, dvrRemote: {blenderRemote}");
    }

    public void NextCommand()
    {
        
        currCommand = gameManager.CurrCommand;
        switch (currCommand.CommandName)
        {
            case Command.Commands.Pause:
                //targVolume = currVolume + 1;
                break;
            case Command.Commands.REC:
                //targVolume = currVolume - 1;
                break;
            case Command.Commands.Play:
                //targChannel = currChannel + 1;
                break;
            case Command.Commands.FF:
                //targChannel = currChannel - 1;
                break;
            case Command.Commands.mute:
                //targVolume = 0;
                break;
            case Command.Commands.Rewind:
               // targChannel = channels[0];
               // numsNeeded = 1;
                break;
        }
    }

    /*
     * Checks which button was pressed. Please excuse the nasty code
     */
    public void RemoteButtonPressed(int button)
    {
        //Debug.Log($"ButtonPressed: {(RemoteButtons)button}, curCommand: {currCommand.command}");
        switch ((DVRRemoteButtons)button)
        {
            case DVRRemoteButtons.Pause:
                if(Command.Commands.Pause != currCommand.CommandName)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                tvAnimator.speed = 0;
                recDot.gameObject.SetActive(false);
                gameManager.CorrectAction();
                ShowTableRemote();
                Debug.Log("Pause");
                break;
            case DVRRemoteButtons.REC:
                if (Command.Commands.REC != currCommand.CommandName)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                Debug.Log("Record");
                recDot.gameObject.SetActive(true);
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case DVRRemoteButtons.Play:
                if (Command.Commands.Play != currCommand.CommandName)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                Debug.Log("Play");
                recDot.gameObject.SetActive(false);
                tvAnimator.speed = 1;
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case DVRRemoteButtons.FF:
                if (Command.Commands.FF != currCommand.CommandName)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                Debug.Log("FF");
                recDot.gameObject.SetActive(false);
                tvAnimator.speed = 2;
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
            case DVRRemoteButtons.Rewind:
                if (Command.Commands.Rewind != currCommand.CommandName)
                {
                    gameManager.DecreaseTime();
                    break;
                }
                Debug.Log("Rewind");
                recDot.gameObject.SetActive(false);
                tvAnimator.speed = -1;
                gameManager.CorrectAction();
                ShowTableRemote();
                break;
        }

    }

    public void ShowLargeRemote()
    {
        tableDVRRemote.SetActive(false);
        dvrRemote.SetActive(true);
        HideOtherRemotes();
        NextCommand();
    }

    public void ShowTableRemote()
    {
        dvrRemote.gameObject.SetActive(false);
        tableDVRRemote.gameObject.SetActive(true);
        NextCommand();
    }

    public void HideOtherRemotes()
    {
        tvRemote.GetTVRemote.gameObject.SetActive(false);
        tvRemote.GetTableTVRemote.gameObject.SetActive(true);
        blenderRemote.GetBlenderRemote.gameObject.SetActive(false);
        if(gameManager.GetBlender)
            blenderRemote.GetTableBlenderRemote.gameObject.SetActive(true);
    }

    public enum DVRRemoteButtons
    {
        Pause,
        REC,
        Play,
        FF,
        Rewind
    }
}

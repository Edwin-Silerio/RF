﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRemote
{
    /// <summary>
    /// Grab the next command
    /// </summary>
    void NextCommand();
   
    /// <summary>
    /// Hides the table remote and shows the large, interactable remote
    /// </summary>
    void ShowLargeRemote();

    /// <summary>
    /// Hides the large remote and shows the table remote
    /// </summary>
    void ShowTableRemote();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVRemote : MonoBehaviour, Remote
{
    [SerializeField] private GameObject tvRemote = default;
    [SerializeField] private GameObject tableTVRemote = default;

    public void ShowLargeRemote()
    {
        Debug.Log("Showing large remote");
        tableTVRemote.SetActive(false);
        tvRemote.SetActive(true);
    }

    public void ShowTableRemote()
    {
        Debug.Log("Showing table remote");
        tvRemote.gameObject.SetActive(false);
        tableTVRemote.gameObject.SetActive(true);
    }
}

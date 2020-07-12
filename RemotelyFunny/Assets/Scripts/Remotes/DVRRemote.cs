using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVRRemote : MonoBehaviour, Remote
{
    [SerializeField] private GameObject dvrRemote = default;
    [SerializeField] private GameObject tableDVRRemote = default;

    private TVRemote tvRemote;
    private BlenderRemote blenderRemote;

    public GameObject GetDVRRemote => dvrRemote;
    public GameObject GetTableDVRRemote => tableDVRRemote;

    private void Start()
    {
        tvRemote = GetComponent<TVRemote>();
        blenderRemote = GetComponent<BlenderRemote>();

        if (tvRemote == null || dvrRemote == null)
            Debug.LogError($"tvRemote: {tvRemote}, dvrRemote: {blenderRemote}");
    }

    public void NextCommand()
    {
        //throw new System.NotImplementedException();
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
        blenderRemote.GetTableBlenderRemote.gameObject.SetActive(true);
    }
}

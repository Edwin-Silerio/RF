using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderRemote : MonoBehaviour, Remote
{
    [SerializeField] private GameObject blenderRemote = default;
    [SerializeField] private GameObject tableBlenderRemote = default;

    private TVRemote tvRemote;
    private DVRRemote dvrRemote;

    private void Start()
    {
        tvRemote = GetComponent<TVRemote>();
        dvrRemote = GetComponent<DVRRemote>();

        if (tvRemote == null || dvrRemote == null)
            Debug.LogError($"tvRemote: {tvRemote}, dvrRemote: {dvrRemote}");
    }
    public void NextCommand()
    {
        //throw new System.NotImplementedException();
    }

    public void ShowLargeRemote()
    {
        blenderRemote.SetActive(true);
        tableBlenderRemote.SetActive(false);
        HideOtherRemotes();
        NextCommand();
    }

    public void ShowTableRemote()
    {
        blenderRemote.gameObject.SetActive(false);
        tableBlenderRemote.gameObject.SetActive(true);
        NextCommand();
    }

    public void HideOtherRemotes()
    {
        tvRemote.gameObject.SetActive(false);
        dvrRemote.gameObject.SetActive(false);
    }
}

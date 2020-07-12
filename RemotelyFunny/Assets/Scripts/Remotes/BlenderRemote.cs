using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderRemote : MonoBehaviour, Remote
{
    [SerializeField] private GameObject blenderRemote = default;
    [SerializeField] private GameObject tableBlenderRemote = default;

    private TVRemote tvRemote;
    private DVRRemote dvrRemote;

    public GameObject GetBlenderRemote => blenderRemote;
    public GameObject GetTableBlenderRemote => tableBlenderRemote;

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
        Debug.Log("Blender show table remote");
        blenderRemote.gameObject.SetActive(false);
        tableBlenderRemote.gameObject.SetActive(true);
        NextCommand();
    }

    public void HideOtherRemotes()
    {
        Debug.Log("Blender hide other remotes");
        tvRemote.GetTVRemote.gameObject.SetActive(false);
        tvRemote.GetTableTVRemote.gameObject.SetActive(true);
        dvrRemote.GetDVRRemote.gameObject.SetActive(false);
        dvrRemote.GetTableDVRRemote.gameObject.SetActive(true);

    }
}

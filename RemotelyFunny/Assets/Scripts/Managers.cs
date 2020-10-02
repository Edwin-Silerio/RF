using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A singleton class that provides singleton support for anything that needs to be a singleton.
 * All singletons can be referenced from afar here.
 */
public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance => instance;

    [SerializeField] private GameManager gameManager = default;
    public static GameManager GameManager => instance.gameManager;

    private void Awake() {

        // Check if a Manager object has already been created 
        // TODO: Determine if this is actually needed. This provides us
        // with singleton support but since this is only used in one scene it 
        // may not be needed.
        /*if (!instance)
        {
            Debug.Log("Setting instance");
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }*/

        instance = this;
    }
}

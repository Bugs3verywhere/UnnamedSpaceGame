using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject mapOverlay;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapOverlay.SetActive(!mapOverlay.activeSelf);
        }
    }
}

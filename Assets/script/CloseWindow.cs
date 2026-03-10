using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    public GameObject window;

    public void Close()
    {
        window.SetActive(false);
    }
}

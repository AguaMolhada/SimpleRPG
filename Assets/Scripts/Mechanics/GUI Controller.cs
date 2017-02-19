using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {

    public void SetActiveMenu(GameObject x)
    {
        x.SetActive(!x.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuUIController : MonoBehaviour
{
    public GameObject selectMenuUI;
    private bool showOrNot = false;

    private void Awake()
    {
        selectMenuUI.SetActive(showOrNot);
    }

    public void SwitchToMenuMode()
    {
        showOrNot = !showOrNot;
        selectMenuUI.SetActive(showOrNot);
    }

}

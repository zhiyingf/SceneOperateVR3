using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuUIController : MonoBehaviour
{
    public GameObject selectMenuUI;
    public GameObject HanderLeft;
    public GameObject HanderRight;
    private bool showOrNot = false;

    private void Awake()
    {
        selectMenuUI.SetActive(showOrNot);
        HanderLeft.SetActive(showOrNot);
        HanderRight.SetActive(showOrNot);
    }

    public void SwitchToMenuMode()
    {
        showOrNot = !showOrNot;

        selectMenuUI.SetActive(showOrNot);
        HanderLeft.SetActive(showOrNot);
        HanderRight.SetActive(showOrNot);
    }

}

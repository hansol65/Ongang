using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager
{
    public void ChangePage(GameObject current, GameObject next)
    {
        current.SetActive(false);
        next.SetActive(true);

        Debug.Log("Change complete!");
    }
}

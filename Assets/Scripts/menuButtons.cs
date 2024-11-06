using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButtons : MonoBehaviour
{
    private menuMNG menu;
    [SerializeField] private GameObject toMenu;

    public void onClickedSelf() {
        menu.onChange(toMenu);
    }

    private void Awake()
    {
        menu = GameObject.Find("MENU_MNG").GetComponent<menuMNG>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAnimations : MonoBehaviour
{
    public Animation MainMenu;

    public Animation AboutUsTop;
    public Animation AboutUsBottom;

    public Animation ControlsLeft;
    public Animation ControlsRight;

    public Animation SceneChanger;

    void Start()
    {
        SceneChanger.Play("WhitePanelFadeOut");
        MainMenu.Play("MenuItemsSlideRight");
    }

    public void StartClick()
    {
        MainMenu.Play("MenuItemsSlideLeftWithScript");
        SceneChanger.Play("SceneChanger");
    }

    public void AboutUsClick()
    {
        MainMenu.Play("MenuItemsSlideLeft");
        AboutUsTop.Play("AboutUsTopSlideDown");
        AboutUsBottom.Play("AboutUsBottomSlideUp");
    }


    public void AboutUsBackClick()
    {
        AboutUsTop.Play("AboutUsTopSlideUp");
        AboutUsBottom.Play("AboutUsBottomSlideDown");
        MainMenu.Play("MenuItemsSlideRight");
    }

     public void ControlsClick()
    {
        MainMenu.Play("MenuItemsSlideLeft");
        ControlsLeft.Play("ControlsItemsLeftSlideRight");
        ControlsRight.Play("ControlsItemsRightSlideLeft");
    }

    public void ControlsBackClick()
    {
        ControlsLeft.Play("ControlsItemsLeftSlideLeft");
        ControlsRight.Play("ControlsItemsRightSlideRight");
        MainMenu.Play("MenuItemsSlideRight");
    }

    public void RecordsClick()
    {
        MainMenu.Play("MenuItemsSlideLeft");
    }


}

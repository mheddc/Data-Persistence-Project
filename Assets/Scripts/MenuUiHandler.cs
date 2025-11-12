using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    private string playerName;

    private void Start()
    {
        //ColorPicker.Init();
        ////this will call the NewColorSelected function when the color picker have a color button clicked.
        //ColorPicker.onColorChanged += NewColorSelected;
        //if (MainManager.Instance != null)
        //{
        //    ColorPicker.SelectColor(MainManager.Instance.color);
        //}
    }

    //public void SaveColorClicked()
    //{
    //    MainManager.Instance.SaveColor();
    //}

    public void OnNameEntered(string name)
    {
        playerName= name;
        Debug.Log("playerName");
        if (DataManager.Instance != null)
        {
            // add code here to handle when a color is selected
            DataManager.Instance.PlayerName = playerName;
            Debug.Log(playerName);
        }
    }
    //public void LoadColorClicked()
    //{
    //    MainManager.Instance.LoadData();
    //    ColorPicker.SelectColor(MainManager.Instance.color);
    //}
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Exit()
    {
        DataManager.Instance.SaveScore();
        // mainmanager.instance.savecolor();
#if unity_editor
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

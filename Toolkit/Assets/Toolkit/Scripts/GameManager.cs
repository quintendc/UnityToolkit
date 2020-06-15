﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    public List<Player> Players = new List<Player>();
    public PersistentData PersistentData = null;
    public SceneSettingsObject InitialSceneSettings = null;

    public GameObject SaveGameManagerPrefab = null;
    public GameObject WidgetManagerPrefab = null;

    private void Awake()
    {
        #region Singleton Pattern GameManager instance

        // check if instance already exists
        if (Instance == null)
        {
            // if not, set instance to this
            Debug.Log("GameManager Instance created");
            Instance = this;
        }
        // if instance already exists and it's not this:
        else if (Instance != null)
        {
            // then destroy this. this enforces our singleton pattern, meaning there can only ever be one instance of the GameManager
            Debug.Log("GameManager Instance already exists this.destroyed");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        #endregion

        Validate();
    }


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// validate whether all necessary files have been delivered, if object is provided it will be instantiated.
    /// </summary>
    private void Validate()
    {
        // check for SaveGameManager, WidgetManager, PersistentData and InitialSceneData
        if (SaveGameManagerPrefab == null)
        {
            Debug.LogWarning("No SaveGameManager Provided! Some functionality may not work.");
        }
        else
        {
            GameObject.Instantiate(SaveGameManagerPrefab);
        }

        if (WidgetManagerPrefab == null)
        {
            Debug.LogWarning("No WidgetManager Provided! Some functionality may not work.");
        }
        else
        {
            GameObject.Instantiate(WidgetManagerPrefab);
        }

        if (PersistentData == null)
        {
            Debug.LogWarning("No PersistentDataObject Provided! Some functionality may not work.");
        }

        if (InitialSceneSettings == null)
        {
            Debug.LogWarning("No InitialSceneSettings object is provided!");
        }
        else
        {
            SetupInitialScene();
        }
    }


    private void SetupInitialScene()
    {
        // do stuff with InitialSceneSettingsObject
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene loaded");
        //do stuff

        // get sceneSettingsObject
        SceneSettingsObject sceneSettingsObject = GameObject.FindGameObjectWithTag("SceneSettingsManager").GetComponent<SceneSettingsManager>().SceneSettings;

        // handle SceneSettingsObject
        WidgetManager.Instance.ShowWidget(sceneSettingsObject.WidgetType);

    }


    #region Player Methods



    #endregion

}

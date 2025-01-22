using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class SceneUpdate : NetworkBehaviour
{
    public Button ChangeSceneButton;
    public SceneManagement sceneManagement;
    public string sceneName;

    void Start(){
        ChangeSceneButton.onClick.AddListener(OnChangeSceneButtonClicked);
    }
    void OnChangeSceneButtonClicked(){
        if (NetworkManager.Singleton.IsServer){
            sceneManagement.ChangeSceneServerRpc(sceneName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class SceneManagement : NetworkBehaviour
{
    [ServerRpc]
    public void ChangeSceneServerRpc(string sceneName)
    {
        if (IsServer)
        {
            NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
    public override void OnNetworkSpawn ()
    {
        if (IsClient && !IsServer)
        {
            NetworkManager.SceneManager.OnSceneEvent += OnClientSceneChanged;
        }
    }
    public void OnClientSceneChanged(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
        {
            Debug.Log("Scene loaded on client: "+ sceneEvent);
        }
    }
    private void OnDestroy()
    {
        if (NetworkManager != null && NetworkManager.SceneManager != null)
        {
            NetworkManager.SceneManager.OnSceneEvent -= OnClientSceneChanged;
        }
    }
}

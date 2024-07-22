using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class NetworkConnect : MonoBehaviour
{
    public int maxConnection = 20;
    public UnityTransport transport;
    private Lobby currentLobby;
    private float heartBeatTimer;


     public async void Awake ()
     {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        JoinOrCreate();
     }
      public async  void JoinOrCreate () {
        try{
            currentLobby = await  Lobbies.Instance.QuickJoinLobbyAsync();
            string relayJoinCode = currentLobby.Data["JOIN_CODE"].Value;//Retrieves the join code that was used when creating the lobby
        
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);
            
            transport.SetClientRelayData(allocation.RelayServer.IpV4,(ushort) allocation.RelayServer.Port,allocation.AllocationIdBytes,allocation.Key,allocation.ConnectionData, allocation.HostConnectionData);

            NetworkManager.Singleton.StartClient();
        } catch{
            Create();
        }

      }

    public async void Create ()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        Debug.LogError(newJoinCode);

        transport.SetHostRelayData(allocation.RelayServer.IpV4,(ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes,allocation.Key,allocation.ConnectionData);
        
        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate= false;
        lobbyOptions.Data= new Dictionary<string, DataObject>();
        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, newJoinCode);
        lobbyOptions.Data.Add("JOIN_CODE", dataObject);

        currentLobby = await Lobbies.Instance.CreateLobbyAsync("Lobby Name",maxConnection, lobbyOptions);
        NetworkManager.Singleton.StartHost();
    }
/*
    public void Update() {
        if (heartBeatTimer>15){
            heartBeatTimer -= 15;
            if ()
        }
    } */
}

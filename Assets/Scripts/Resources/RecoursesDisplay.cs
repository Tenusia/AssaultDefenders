using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class RecoursesDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text resourcesText = null;

    private RTSPlayer player;
    
    private void Update() 
    {
        if (player == null)
        {
            StartCoroutine(DelayPlayerGet()); 

            if(player != null)
            {
                ClientHandleResourcesUpdated(player.GetResources());
                
                player.ClientOnResourcesUpdated += ClientHandleResourcesUpdated;
            }
        }
    }

    IEnumerator DelayPlayerGet()
    {
        yield return new WaitForSeconds(0.5f);
        player = NetworkClient.connection.identity.GetComponent<RTSPlayer>(); 
    }

    private void OnDestroy() 
    {
        player.ClientOnResourcesUpdated -= ClientHandleResourcesUpdated;
    }

    private void ClientHandleResourcesUpdated(int resources)
    {
        resourcesText.text = $"Resources: {resources}";
    }
}

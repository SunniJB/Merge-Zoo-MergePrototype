using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public static class CheckNetwork
{
    public static UnityEvent<string, bool> ConnectionResult = new UnityEvent<string, bool>();

    public static IEnumerator TryConnection(string url, int connectionTimer)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ConnectionResult.Invoke("Not connected to wifi or carrier network!", false);
            yield return null;
        }
        else
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.timeout = connectionTimer;

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                ConnectionResult.Invoke(request.error, false);
            }
            else
            {
                ConnectionResult.Invoke("Connection Succeeded!", true);
            }
        }
    }
}

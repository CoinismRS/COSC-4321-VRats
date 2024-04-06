using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ApiHttp : MonoBehaviour
{
    // URL of your localhost server
    private const string localhostUrl = "http://localhost:3000/"; // Replace 3000 with your server's port

    void Start()
    {
        StartCoroutine(GetColors());
    }

    IEnumerator GetColors()
    {
        string getUrl = localhostUrl + "colors"; // Example GET endpoint

        using (UnityWebRequest webRequest = UnityWebRequest.Get(getUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("GET request error: Connection Error");
            }
            else if (webRequest.result == UnityWebRequest.Result.ProtocolError) 
            {
                Debug.LogError("GET request error: HTTP Protocol Error - " + webRequest.error);
            }
            else
            {
                Debug.Log("GET request successful! Response: " + webRequest.downloadHandler.text);
            }
        }
    }
}

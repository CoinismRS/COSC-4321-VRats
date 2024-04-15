using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ColorData
{
    public string name; 
    public string hex;
}

[System.Serializable]
public class RootColors 
{
    public ColorData[] colors;
}

public class ApiHttp : MonoBehaviour
{
    // URL of your localhost server
    private const string localhostUrl = "http://localhost:8080/"; // Replace 3000 with your server's port

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

                try
                {
                    // Deserialize the JSON response into a dictionary
                    RootColors rootColors = JsonUtility.FromJson<RootColors>(webRequest.downloadHandler.text);
                    // Extract the "colors" array from the response
                    foreach (ColorData colorData in rootColors.colors)
                    {
                        Debug.Log("Color Name: " + colorData.name + ", Hex: " + colorData.hex);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing JSON response: " + e.Message);
                }
            }
        }
    }
}

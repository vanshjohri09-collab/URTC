using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class ApproveCollab : MonoBehaviour
{
    public string apiUrl = "https://your-backend.com/github/addcollab";

    public void OnApproveButton(string collabId, string sessionId, string status)
    {
        StartCoroutine(SendApproval(collabId, sessionId, status));
    }

    IEnumerator SendApproval(string collabId, string sessionId, string status)
    {
        string json = "{\"collabId\":\"" + collabId + "\",\"sessionId\":\"" + sessionId + "\",\"status\":\"" + status + "\"}";

        UnityWebRequest req = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Sending Request: " + json);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Collab Approved Successfully: " + req.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Approval Failed: " + req.error);
        }
    }
}

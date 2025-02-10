using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InfuraRequest : MonoBehaviour
{
    private string infuraEndpoint = "https://sepolia.infura.io/v3/d011ca708b824a6cae996315a6fd0396";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeInfuraRequest());
    }

    // Funzione per fare una richiesta a Infura
    private IEnumerator MakeInfuraRequest()
    {
        // JSON-RPC request per ottenere l'ultimo blocco
        string jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"eth_blockNumber\",\"params\":[],\"id\":1}";

        // Crea una richiesta HTTP POST
        UnityWebRequest request = new UnityWebRequest(infuraEndpoint, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Invia la richiesta e attende la risposta
        yield return request.SendWebRequest();

        // Gestisci la risposta
        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log("Risposta da Infura: " + response);
        }
        else
        {
            Debug.LogError("Errore nella richiesta a Infura: " + request.error);
        }
    }
}

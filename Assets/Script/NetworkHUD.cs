using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;


public class NetworkHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        SceneManager.LoadScene(0);

    }

    // Update is called once per frame
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        SceneManager.LoadScene(0);

    }


}


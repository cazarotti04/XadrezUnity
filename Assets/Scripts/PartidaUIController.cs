using UnityEngine;
using UnityEngine.SceneManagement;

public class PartidaUIController : MonoBehaviour {

    public void Sair()
    {
        SceneManager.LoadScene("inicio");
    }
}

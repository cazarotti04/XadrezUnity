using UnityEngine;
using System.Collections;

public class Movimentacao : MonoBehaviour {

    GameController gameController;

    bool colidindo;
    GameObject casa;

    void Awake()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        colidindo = false;
        casa = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Casas"))
        {
            colidindo = true;
            casa = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(colidindo && other.gameObject == casa)
        {
            colidindo = false;
            casa = null;
        }
    }
}

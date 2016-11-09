using UnityEngine;
using tabuleiro;
using xadrez;
using UnityEngine.UI;

class GameController : MonoBehaviour {

    public GameObject reiBranco = null;
    public GameObject reiPreto = null;

    public Text txtMsg = null;
    public Text txtXeque = null;

    public GameObject pecaEscolhida { get; private set; }

    public Estado estado { get; private set; }

    PartidaDeXadrez partida;
    PosicaoXadrez origem, destino;
    Color corOriginal;

	void Start () {
        estado = Estado.AguadandoJogada;
        pecaEscolhida = null;
        corOriginal = txtMsg.color; 

        partida = new PartidaDeXadrez();

        txtXeque.text = "";
        informarAguardando();


        Util.instanciarRei('e', 1, Cor.Branca, partida, reiBranco);
        Util.instanciarRei('e', 8, Cor.Preta, partida, reiPreto);
    }

    void InformarAviso(string msg)
    {
        txtMsg.color = Color.red;
        txtMsg.text = msg;
        Invoke("informarAguardando", 1f);
    }

    void InformarAguardando()
    {
        txtMsg.color = corOriginal;
        txtMsg.text = "Aguardando jogada: " + partida.jogadorAtual;
    }
}

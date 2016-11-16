using UnityEngine;
using tabuleiro;
using xadrez;
using UnityEngine.UI;

class GameController : MonoBehaviour {

    public GameObject ReiBranco = null;
    public GameObject ReiPreto = null;
    public GameObject TorreBranca = null;
    public GameObject TorrePreta = null;

    public Text txtMsg = null;
    public Text txtXeque = null;

    public GameObject pecaEscolhida { get; private set; }

    public Estado estado { get; private set; }

    PartidaDeXadrez partida;
    PosicaoXadrez origem, destino;
    Color corOriginal;

	void Start () {
        estado = Estado.AguardandoJogada;
        pecaEscolhida = null;
        corOriginal = txtMsg.color; 

        partida = new PartidaDeXadrez();

        txtXeque.text = "";
        InformarAguardando();


        Util.instanciarRei('e', 1, Cor.Branca, partida, ReiBranco);
        Util.instanciarRei('e', 8, Cor.Preta, partida, ReiPreto);
        Util.instanciarTorre('a', 1, Cor.Branca, partida, TorreBranca);
        Util.instanciarTorre('h', 1, Cor.Branca, partida, TorreBranca);
        Util.instanciarTorre('a', 8, Cor.Preta, partida, TorrePreta);
        Util.instanciarTorre('h', 8, Cor.Preta, partida, TorrePreta);
    }

    public void processarMouseDown(GameObject peca, GameObject casa)
    {
        if(estado == Estado.AguardandoJogada)
        {
            if(casa != null)
            {
                try
                {
                    char coluna = casa.name[0];
                    int linha = casa.name[1] - '0';
                    origem = new PosicaoXadrez(coluna, linha);
                    partida.validarPosicaoDeOrigem(origem.toPosicao());
                    pecaEscolhida = peca;
                    estado = Estado.Arrastando;
                    txtMsg.text = "Solte a peça na casa de destino";
                }
                catch (TabuleiroException e)
                {
                    InformarAviso(e.Message);
                }
            }
        }
    }

    public void processarMouseUp(GameObject peca, GameObject casa)
    {
        if (estado == Estado.Arrastando)
        {
            Debug.Log("1");
            if (casa != null)
            {
                Debug.Log("2");
                if (pecaEscolhida != null && pecaEscolhida == peca)
                {
                    Debug.Log("3");
                    try
                    {
                        Debug.Log("4");
                        char coluna = casa.name[0];
                        int linha = casa.name[1] - '0';
                        destino = new PosicaoXadrez(coluna, linha);
                        
                        partida.validarPosicaoDeDestino(origem.toPosicao(), destino.toPosicao());
                        partida.realizaJogada(origem.toPosicao(), destino.toPosicao());

                        peca.transform.position = Util.posicaoNaCena(coluna, linha);

                        pecaEscolhida = null;

                        if (partida.terminada)
                        {
                            estado = Estado.GameOver;
                            txtMsg.text = "Vencedor: " + partida.jogadorAtual;
                            txtXeque.text = "XEQUEMATE";
                        }
                        else
                        {
                            estado = Estado.AguardandoJogada;
                            InformarAguardando();
                            txtXeque.text = (partida.xeque) ? "XEQUE" : "";
                        }

                    }
                    catch (TabuleiroException e)
                    {
                        peca.transform.position = Util.posicaoNaCena(origem.coluna, origem.linha);
                        estado = Estado.AguardandoJogada;
                        InformarAviso(e.Message);
                    }
                }              
            }
        }
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

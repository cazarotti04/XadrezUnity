using UnityEngine;
using tabuleiro;
using xadrez;
using UnityEngine.UI;

class GameController : MonoBehaviour {

    public GameObject ReiBranco = null;
    public GameObject ReiPreto = null;
    public GameObject RainhaBranca = null;
    public GameObject RainhaPreta = null;
    public GameObject TorreBranca = null;
    public GameObject TorrePreta = null;
    public GameObject CavaloBranco = null;
    public GameObject CavaloPreto = null;
    public GameObject BispoBranco = null;
    public GameObject BispoPreto = null;
    public GameObject PeaoBranco = null;
    public GameObject PeaoPreto = null;

    public Text txtMsg = null;
    public Text txtXeque = null;

    public GameObject pecaEscolhida { get; private set; }

    public Estado estado { get; private set; }

    PartidaDeXadrez partida;
    PosicaoXadrez origem, destino;
    Color corOriginal;
    Vector3 posDescarteBrancas, posDescartePretas;

    void Start () {
        estado = Estado.AguardandoJogada;
        pecaEscolhida = null;
        corOriginal = txtMsg.color;

        posDescarteBrancas = new Vector3(-2.5f, 0f, -1);
        posDescartePretas = new Vector3(2.5f, 0f, 1);

        partida = new PartidaDeXadrez();

        txtXeque.text = "";
        InformarAguardando();


        Util.instanciarTorre('a', 1, Cor.Branca, partida, TorreBranca);
        Util.instanciarCavalo('b', 1, Cor.Branca, partida, CavaloBranco);
        Util.instanciarBispo('c', 1, Cor.Branca, partida, BispoBranco);
        Util.instanciarRainha('d', 1, Cor.Branca, partida, RainhaBranca);
        Util.instanciarRei('e', 1, Cor.Branca, partida, ReiBranco);
        Util.instanciarBispo('f', 1, Cor.Branca, partida, BispoBranco);
        Util.instanciarCavalo('g', 1, Cor.Branca, partida, CavaloBranco);
        Util.instanciarTorre('h', 1, Cor.Branca, partida, TorreBranca);
        Util.instanciarPeao('a', 2, Cor.Branca, partida, PeaoBranco);
        Util.instanciarPeao('b', 2, Cor.Branca, partida, PeaoBranco);
        Util.instanciarPeao('c', 2, Cor.Branca, partida, PeaoBranco);
        Util.instanciarPeao('d', 2, Cor.Branca, partida, PeaoBranco);
        Util.instanciarPeao('e', 2, Cor.Branca, partida, PeaoBranco);
        Util.instanciarPeao('f', 2, Cor.Branca, partida, PeaoBranco);
        Util.instanciarPeao('g', 2, Cor.Branca, partida, PeaoBranco);
        Util.instanciarPeao('h', 2, Cor.Branca, partida, PeaoBranco);


        Util.instanciarTorre('a', 8, Cor.Preta, partida, TorrePreta);
        Util.instanciarCavalo('b', 8, Cor.Preta, partida, CavaloPreto);
        Util.instanciarBispo('c', 8, Cor.Preta, partida, BispoPreto);
        Util.instanciarRainha('d', 8, Cor.Preta, partida, RainhaPreta);
        Util.instanciarRei('e', 8, Cor.Preta, partida, ReiPreto);
        Util.instanciarBispo('f', 8, Cor.Preta, partida, BispoPreto);
        Util.instanciarCavalo('g', 8, Cor.Preta, partida, CavaloPreto);
        Util.instanciarTorre('h', 8, Cor.Preta, partida, TorrePreta);
        Util.instanciarPeao('a', 7, Cor.Preta, partida, PeaoPreto);
        Util.instanciarPeao('b', 7, Cor.Preta, partida, PeaoPreto);
        Util.instanciarPeao('c', 7, Cor.Preta, partida, PeaoPreto);
        Util.instanciarPeao('d', 7, Cor.Preta, partida, PeaoPreto);
        Util.instanciarPeao('e', 7, Cor.Preta, partida, PeaoPreto);
        Util.instanciarPeao('f', 7, Cor.Preta, partida, PeaoPreto);
        Util.instanciarPeao('g', 7, Cor.Preta, partida, PeaoPreto);
        Util.instanciarPeao('h', 7, Cor.Preta, partida, PeaoPreto);

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
            if (casa != null)
            {
                if (pecaEscolhida != null && pecaEscolhida == peca)
                {
                    try
                    {
                        char coluna = casa.name[0];
                        int linha = casa.name[1] - '0';
                        destino = new PosicaoXadrez(coluna, linha);
                        
                        partida.validarPosicaoDeDestino(origem.toPosicao(), destino.toPosicao());
                        Peca pecaCapturada =  partida.realizaJogada(origem.toPosicao(), destino.toPosicao());

                        if(pecaCapturada != null)
                        {
                            removerObjetoCapturado(pecaCapturada);
                        }

                        peca.transform.position = Util.posicaoNaCena(coluna, linha);

                        tratarJogadasEspeciais();
                    
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

    void removerObjetoCapturado(Peca peca)
    {
        GameObject obj = peca.obj;
        if(peca.cor == Cor.Branca)
        {
            obj.transform.position = posDescarteBrancas;
            posDescarteBrancas.z = posDescarteBrancas.z + 0.4f; 
        }
        else
        {
            obj.transform.position = posDescartePretas;
            posDescartePretas.z = posDescartePretas.z - 0.4f;
        }
    }

    void tratarJogadasEspeciais()
    {
        Posicao pos = destino.toPosicao();
        Peca pecaMovida = partida.tab.peca(pos);

        if(pecaMovida is Rei && destino.coluna == origem.coluna + 2)
        {
            GameObject torre = partida.tab.peca(pos.linha, pos.coluna - 1).obj;
            torre.transform.position = Util.posicaoNaCena('f', origem.linha);
        }

        if (pecaMovida is Rei && destino.coluna == origem.coluna - 2)
        {
            GameObject torre = partida.tab.peca(pos.linha, pos.coluna + 1).obj;
            torre.transform.position = Util.posicaoNaCena('d', origem.linha);
        }

        if(partida.promovida != null)
        {
            removerObjetoCapturado(partida.promovida);
            Vector3 posPromovida = Util.posicaoNaCena(destino.coluna, destino.linha);
            GameObject prefab = (pecaMovida.cor == Cor.Branca) ? RainhaBranca : RainhaPreta;
            GameObject rainha = Instantiate(prefab, posPromovida, Quaternion.identity) as GameObject;
            pecaMovida.obj = rainha;
        }
    }
}

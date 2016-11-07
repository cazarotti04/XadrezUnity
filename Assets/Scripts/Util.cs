using UnityEngine;
using tabuleiro;
using xadrez;

class Util : MonoBehaviour {
    
    public static void instanciarRei(char coluna, int linha, Cor cor, PartidaDeXadrez partida, GameObject prefab){
        Vector3 pos = posicaoNaCena(coluna, linha);
        GameObject rei = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        Peca peca = new Rei(partida.tab, cor, partida, rei);
        partida.colocarNovaPeca(coluna, linha, peca);
        rei.GetComponent<ReferenciaPeca>().peca = peca;
    }

    public static Vector3 posicaoNaCena (char coluna, int linha)
    {
        Vector3 posChao = GameObject.Find("PlanoDasPecas").transform.position;
        Vector3 posCasa = GameObject.Find("" + coluna + linha).transform.position;
        return new Vector3(posCasa.x, posChao.y, posCasa.z);
    }
}

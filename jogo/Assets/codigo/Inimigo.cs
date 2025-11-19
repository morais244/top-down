using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour
{
    public float velocidade = 2f;
    public int vidaMaxima = 50;
    public int vidaAtual;
    public int dano = 10;
    public int pontosAoMorrer = 10;
    public float intervaloDano = 1f;

    public Slider barraVida;
    public Transform corpo;

    private Transform jogador;
    private float proximoDano = 0f;

    void Start()
    {
        vidaAtual = vidaMaxima;
        jogador = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (jogador != null)
        {
            Vector2 direcao = (jogador.position - transform.position).normalized;
            transform.position += (Vector3)direcao * velocidade * Time.deltaTime;

            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            if (corpo != null)
                corpo.rotation = Quaternion.Euler(0, 0, angulo - 90f);
        }

        if (barraVida != null)
            barraVida.value = (float)vidaAtual / vidaMaxima;
    }

    void OnTriggerStay2D(Collider2D outro)
    {
        if (outro.CompareTag("Player") && Time.time >= proximoDano)
        {
            Jogador j = outro.GetComponent<Jogador>();
            if (j != null)
                j.LevarDano(dano);

            proximoDano = Time.time + intervaloDano;
        }
    }

    public void LevarDano(int danoRecebido)
    {
        vidaAtual -= danoRecebido;

        if (vidaAtual <= 0)
        {
            vidaAtual = 0;

            // Adiciona pontos ao jogador antes de destruir
            GameObject jogadorObj = GameObject.FindGameObjectWithTag("Player");
            if (jogadorObj != null)
            {
                Jogador j = jogadorObj.GetComponent<Jogador>();
                if (j != null)
                {
                    j.AdicionarPontuacao(pontosAoMorrer);
                    Debug.Log($"Inimigo morto — +{pontosAoMorrer} pontos");
                }

            }

            Destroy(gameObject);
        }

        if (barraVida != null)
            barraVida.value = (float)vidaAtual / vidaMaxima;
    }
}

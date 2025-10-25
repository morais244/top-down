using UnityEngine;
using UnityEngine.UI;

public class UFO : MonoBehaviour
{
    public float velocidade = 2.5f;
    public int vidaMaxima = 60;
    private int vidaAtual;
    public int danoColisao = 12;
    public int pontosPorDestruicao = 15;

    public Slider barraDeVida;
    public Transform corpoVisual;

    private Transform alvo; // referência ao jogador

    void Start()
    {
        vidaAtual = vidaMaxima;
        alvo = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (alvo != null)
        {
            // movimentação em direção ao jogador
            Vector2 direcao = (alvo.position - transform.position).normalized;
            transform.position += (Vector3)direcao * velocidade * Time.deltaTime;

            // rotação do corpo visual
            if (corpoVisual != null)
            {
                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                corpoVisual.rotation = Quaternion.Euler(0, 0, angulo - 90f);
            }
        }

        // atualiza a barra de vida
        if (barraDeVida != null)
            barraDeVida.value = (float)vidaAtual / vidaMaxima;
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            Player jogador = outro.GetComponent<Player>();
            if (jogador != null)
                jogador.LevarDano(danoColisao);
        }
    }

    public void ReceberDano(int danoRecebido)
    {
        vidaAtual -= danoRecebido;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (vidaAtual <= 0)
            DestruirUFO();
    }

    private void DestruirUFO()
    {
        if (alvo != null)
        {
            Player jogador = alvo.GetComponent<Player>();
            if (jogador != null)
                jogador.AdicionarPontuacao(pontosPorDestruicao);
        }

        // pequeno efeito antes de sumir (opcional)
        Destroy(gameObject);
    }
}

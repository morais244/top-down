using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour
{
    [Header("Atributos")]
    public float velocidade = 2f;
    public float raioDeteccao = 5f; // distância em que o inimigo detecta o jogador
    public int vidaMaxima = 50;
    public int vidaAtual;
    public int dano = 10;
    public int pontosAoMorrer = 10;

    [Header("Referências")]
    public Slider barraVida;
    public Transform corpo;

    private Transform player;
    private bool jogadorDetectado = false;

    void Start()
    {
        vidaAtual = vidaMaxima;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Verifica a distância até o jogador
        float distancia = Vector2.Distance(transform.position, player.position);
        jogadorDetectado = distancia <= raioDeteccao;

        // Se o jogador estiver dentro da área de detecção, segue
        if (jogadorDetectado)
        {
            Vector2 direcao = (player.position - transform.position).normalized;
            transform.position += (Vector3)direcao * velocidade * Time.deltaTime;

            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            if (corpo != null)
                corpo.rotation = Quaternion.Euler(0, 0, angulo - 90f);
        }

        // Atualiza a barra de vida
        if (barraVida != null)
            barraVida.value = (float)vidaAtual / vidaMaxima;
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            Player p = outro.GetComponent<Player>();
            if (p != null)
                p.LevarDano(dano);
        }
    }

    public void LevarDano(int danoRecebido)
    {
        vidaAtual -= danoRecebido;

        if (vidaAtual <= 0)
        {
            vidaAtual = 0;

            if (player != null)
            {
                Player p = player.GetComponent<Player>();
                if (p != null)
                    p.AdicionarPontuacao(pontosAoMorrer);
            }

            Destroy(gameObject);
        }
    }

    // Desenha o raio de detecção na Scene View (apenas visual)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeteccao);
    }
}

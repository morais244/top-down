using UnityEngine;

public class Arma : MonoBehaviour
{
    public Transform saidaDoTiro;
    public GameObject balaPrefab;
    public float intervaloDeDisparo = 0.25f;
    public float velocidadeBala = 10f;

    private float tempoDeDisparo;
    private Camera cam;
    private bool viradoPraDireita = true;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Pega posição do mouse e direção
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direcao = mousePos - transform.position;

        // Calcula ângulo
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        // Aplica rotação só no eixo Z
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        // Detecta se precisa flipar (mouse passou pro outro lado)
        if (mousePos.x < transform.position.x && viradoPraDireita)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !viradoPraDireita)
        {
            Flip();
        }

        // Disparo
        if (Input.GetMouseButton(0) && Time.time > tempoDeDisparo)
        {
            tempoDeDisparo = Time.time + intervaloDeDisparo;
            GameObject bala = Instantiate(balaPrefab, saidaDoTiro.position, saidaDoTiro.rotation);
            bala.GetComponent<Rigidbody2D>().linearVelocity = saidaDoTiro.right * velocidadeBala;
        }
    }

    void Flip()
    {
        viradoPraDireita = !viradoPraDireita;
        Vector3 escala = transform.localScale;
        escala.y *= -1; // inverte no eixo Y pra não ficar de ponta cabeça
        transform.localScale = escala;
    }

    void OnDrawGizmos()
    {
    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position, transform.position + transform.right * 1f);
    }
}

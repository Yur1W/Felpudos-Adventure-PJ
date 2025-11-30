using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDePedra : MonoBehaviour
{
    // Prefab do inimigo (arraste no Inspector)
    public GameObject Pedra;
   

    // Intervalo entre spawns (segundos)
    [SerializeField]
    public float intervalo = 3f;

    // Limites de spawn no cenário
    public float limiteX = 9.5f;
    public float limiteY = 4f;

    // Velocidade de movimento dos inimigos
    public float velocidade = 3f;

    // Limite X para destruir o inimigo ao sair da tela
    public float limiteDestruicaoX = -14f;

    void Start()
    {
        // Começa a gerar inimigos repetidamente
        InvokeRepeating("GerarInimigo", 0f, intervalo);
    }
    void Update()
    {       
    }

    void GerarInimigo()
    {
        // Define posição de spawn (à direita da tela)
        float x = limiteX;
        float y = Random.Range(-1.5f, -3.80f );
        Vector2 posicaoAleatoria = new Vector2(x, y);

        // Instancia o inimigo
                Instantiate(Pedra, posicaoAleatoria, Quaternion.identity);
        
    }
}

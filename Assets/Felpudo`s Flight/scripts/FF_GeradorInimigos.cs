using UnityEngine;
using System.Collections;

public class FFGeradorInimigos : MonoBehaviour
{
    // Prefab do inimigo (arraste no Inspector)
    public GameObject LesmaAmarelaPrefab;
    public GameObject LesmaBasicaPrefab;
    public GameObject LesmaSuperPrefab;

    // Intervalo entre spawns (segundos)
    public float intervalo = 1f;

    // Limites de spawn no cenário
    public float limiteX = 8f;
    public float limiteY = 4f;

    // Velocidade de movimento dos inimigos
    public float velocidade = 3f;
    int Enemy;

    // Limite X para destruir o inimigo ao sair da tela
    public float limiteDestruicaoX = -12f;

    void Start()
    {
        // Começa a gerar inimigos repetidamente
        InvokeRepeating("GerarInimigo", 0f, intervalo);
    }
    void Update()
    {
        Enemy = Random.Range(1, 4);       
    }

    void GerarInimigo()
    {
        // Define posição de spawn (à direita da tela)
        float x = limiteX;
        float y = Random.Range(-limiteY, limiteY);
        Vector2 posicaoAleatoria = new Vector2(x, y);

        // Instancia o inimigo
        switch (Enemy)
        {
            case 1:
                Instantiate(LesmaBasicaPrefab, posicaoAleatoria, Quaternion.identity);
                break;
            case 2:
                Instantiate(LesmaAmarelaPrefab, posicaoAleatoria, Quaternion.identity);
                break;
            case 3:
                Instantiate(LesmaSuperPrefab, posicaoAleatoria, Quaternion.identity);
                break;
        }
    }
}

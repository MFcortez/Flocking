using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject[] fishPrefab;//array de prefabs de peixes
    public int numFish = 20;//numero de peixes na cena
    public GameObject[] allFish;//array para guardar os peixes presentes na cena
    public Vector3 swinLimits = new Vector3(5, 5, 5);//define os limites de spawn dos peixes
    public Vector3 goalPos;//posição destino

    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]//Velocidade minima do peixe
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;//velocidade maxima do peixe
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;//distancia minima para que o peixe seja considerado próximo/grupo
    [Range(1.0f, 5.0f)]
    public float rotationSpeed;//velocidade de rotação do grupo

    void Start()
    {
        allFish = new GameObject[numFish];//define o tamanho do array baseado no numero de peixes que serao instanciados
        for(int i = 0; i < numFish; i++)//executa enquanto não chegar no numero de peixes definido
        {
            int index = Random.Range(0, 2);//index pega um numero entre 0 e 1
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                Random.Range(-swinLimits.y, swinLimits.y),
                Random.Range(-swinLimits.z, swinLimits.z));//pos recebe uma posição aleatoria dentro do swin limits definido acima
            allFish[i] = (GameObject)Instantiate(fishPrefab[index], pos, Quaternion.identity);//instancia um peixe[index](vermelho ou azul) na posição pos
            allFish[i].GetComponent<Flock>().manager = this;//se define como o manager de cada peixe instanciado
        }
    }

    void Update()
    {
        goalPos = transform.position;//define a posição destino como a posição do objeto carregando este script
        if (Random.Range(0, 100) < 10) //aleatoriamente muda a posição destino do objeto
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,
                        swinLimits.x),
                Random.Range(-swinLimits.y,
                        swinLimits.y),
                Random.Range(-swinLimits.z,
                        swinLimits.z));
        }
    }
}

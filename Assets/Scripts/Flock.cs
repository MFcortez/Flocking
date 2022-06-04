using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager manager;
    float speed;//guarda a velocidade do peixe

    void Start()
    {
        speed = Random.Range(manager.minSpeed,
            manager.maxSpeed); //Define uma velocidade aleatoria para o peixe baseado nos limites do manager
    }

    void Update()
    {
        ApplyRules();//Chama o m�todo que faz o movimento de grupo e evita colis�o
        transform.Translate(0, 0, Time.deltaTime * speed); //Move o peixe no eixo Z
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = manager.allFish;//declara um array de gameObjects cotendo todos os peixes da cena (Get from manager)

        Vector3 vCenter = Vector3.zero;//Dire��o ao centro do grupo � igual a zero
        Vector3 vAvoid = Vector3.zero;//Dire��o contra os peixes pr�ximos � igual a zero
        float gSpeed = 0.01f;//velocidade do grupo come�a no 0.01f
        float nDistance;//Distancia dos peixes proximos
        int groupSize = 0;//Tamanho do grupo � igual a zero

        foreach(GameObject go in gos)//Para cada gameObject dentro do array de gameObjects
        {
            if(go != gameObject)//Verifica se n�o � o objeto atual (onde se encontra o script)
            {
                nDistance = Vector3.Distance(go.transform.position, transform.position);//calcula a distancia entre o objeto atual e o objeto no array 
                if(nDistance <= manager.neighbourDistance)//verifica se o objeto peixe esta proximo de outro objeto para se agrupar
                {
                    //calcula o centro do grupo e o seu tamanho 
                    vCenter += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)//Se os peixes est�o pr�ximos de mais
                    {
                        vAvoid += (transform.position - go.transform.position);//Define a dire��o para evitar a colis�o
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();//Pega o script flock do objeto pr�ximo
                    gSpeed += anotherFlock.speed;//aumenta a velocidade do grupo conforme a velocidade dos peixes pr�ximos
                }
            }
        }
        if(groupSize > 0)//caso o grupo tenha pelo menos um peixe
        {
            vCenter /= groupSize;//Faz a m�dia do centro do grupo
            gSpeed /= groupSize;//Faz a m�dia da velocidade do grupo

            Vector3 direction = (vCenter - vAvoid) - transform.position;//Calcula a diferen�a entre dire��o do movimento do grupo e a posi��o do peixe
            if(direction != Vector3.zero)//verifica se o peixe est� respeitando a dire��o do grupo, e executa caso n�o esteja
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                    Quaternion.LookRotation(direction),
                    manager.rotationSpeed * Time.deltaTime);//faz a rota��o para que o peixe continue na dire��o do grupo
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PowerStates state;
    public List<GameObject> antorchas;
    public List<bool> torchsLit;
    public int codeCount;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        antorchas = new List<GameObject>();
        torchsLit = new List<bool>();
    }

    private void Start()
    {
        codeCount = 0;

        GetAllTorchs();
    }

    public void GetAllTorchs()
    {
        for (int i = 0; i < antorchas.Count; i++) //Cambiar para que solo haga el GetComponent 1 vez y cada vez que lo llame chequee los bools
        {
            torchsLit[i] = antorchas[i].GetComponent<CodeInteractor>().isLit;
        }

        CheckTorchStatus();
    }

    public void CheckTorchStatus()
    {
        bool n1Lit = torchsLit[0];
        bool n5Lit = torchsLit[4];
        bool n6Lit = torchsLit[5];

        if (n1Lit && n5Lit && n6Lit)
        {
            Debug.Log("Las antorchas 1, 5 y 6 están encendidas. ¡La puerta se abre!");
        }
    }
}

public enum PowerStates
{
    OnLaser,
}

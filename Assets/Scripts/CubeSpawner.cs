using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;
    Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubequeCapacity = 20;
    [SerializeField] private bool autogrow = true;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubecolors;
    [HideInInspector] public int MaxCubeNum;
    private int max = 11; //2 power 11= 2048
    private Vector3 defaultSpawnpos;
    private void Awake()
    {
        Instance = this;
        defaultSpawnpos = transform.position;
        MaxCubeNum = (int)Mathf.Pow(2, max);
        InitalizeCubesqueue();
    }


    private void InitalizeCubesqueue() {
        for (int i = 0; i < cubequeCapacity; i++)
        {
            AddCubeToqueu();
        }
    }
    private void AddCubeToqueu(){
        Cube cube = Instantiate(cubePrefab, defaultSpawnpos, Quaternion.identity, transform).GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQueue.Enqueue(cube);
    }
    public Cube Spawn(int numb,Vector3 position)
    {
        if (cubesQueue.Count==0)
        {
            if (autogrow)
            {
                cubequeCapacity++;
                AddCubeToqueu();
            }
            else
            {
                Debug.Log("Pool'da  Cube kalmadý...");
            }
        }
        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(numb);
        cube.SetColor(GetColor(numb));
        cube.gameObject.SetActive(true);
        return cube;
    }
    public Cube spawnrandom()
    {
        return Spawn(GenerateRand(), defaultSpawnpos);
    }
    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);


    }
    public int GenerateRand()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    private Color GetColor(int numb)
    {
        return cubecolors[(int)(Mathf.Log(numb) / Mathf.Log(2) - 1)];
    }
}

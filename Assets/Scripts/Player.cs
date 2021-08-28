using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float push;
    [SerializeField] private float cubeMaxPox;
    [Space]
    [SerializeField] private TouchSlider touchSlider;
     private Cube maincube;
    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;

    private void Start()
    {
        SpawnCube();
        canMove = true;
        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }
    private void Update()
    {
        if (isPointerDown)
        {
            maincube.transform.position = Vector3.Lerp(maincube.transform.position,cubePos,moveSpeed*Time.deltaTime);
        }
    }
    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    private void OnPointerDrag(float xmove)
    {
        if (isPointerDown)
        {
            cubePos = maincube.transform.position;
            cubePos.x = xmove * cubeMaxPox;
        }
    }
    private void OnPointerUp()
    {
        if (isPointerDown&&canMove)
        {
            isPointerDown = false;
            canMove = false;
            maincube.CubeRigidbody.AddForce(Vector3.forward * push, ForceMode.Impulse);
            Invoke("spawnnewcube", 0.45f);
        }
    }
    private void spawnnewcube()
    {
        maincube.IsMainCube = false;
        canMove = true;
        SpawnCube();
    }
    private void SpawnCube()
    {
        maincube = CubeSpawner.Instance.spawnrandom();
        maincube.IsMainCube = true;
        cubePos = maincube.transform.position;

    }
}

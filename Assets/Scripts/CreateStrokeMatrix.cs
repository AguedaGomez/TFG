﻿using UnityEngine;
using System.Collections;

public class CreateStrokeMatrix : MonoBehaviour {

    private const int NCELLS = 100; // numero de filas y columnas de la matriz en la que se guardarán los datos y en las que se dividirá pantalla

    public int[,] matrix = new int[NCELLS, NCELLS]; // matriz de datos del dibujo

    private float cellsWidth = Screen.width / NCELLS; // ancho de cada celda
    private float cellsHeigth = Screen.height / NCELLS; // alto de cada celda
    private int row, column; // coordenadas columna, fila
    private bool held;
    private Camera cam;
    private Vector3 mousePosition;
    private Vector3 prevPosition;
    private Vector3 endPosition;
    private Mesh drawMesh;
    private Material mat;
    private Draw drawScript;
    private ControladorPaneles controladorPanelesScript;
    private EjemploGuardar ejemploGuardarScript;

    private void Start () {
        cam = GetComponent<Camera>();
        drawScript = GetComponent<Draw>();
        drawMesh = new Mesh();
        mat = new Material(Shader.Find("Sprites/Default"));
        held = false;
        controladorPanelesScript = GetComponent<ControladorPaneles>();
        ejemploGuardarScript = GetComponent<EjemploGuardar>();
        InitializeMatrix();
	
	}

    private void InitializeMatrix()
    {
        for (int r = 0; r < NCELLS; r++)
        {
            for (int c = 0; c < NCELLS; c++)
            {
                matrix[r,c] = 0;
            }
        }
    }

    private int Mousex2Column (float coorX)
    {
        return (int)(coorX / cellsWidth);
    }

    private int Mousey2Row (float coorY)
    {
        return (int)(NCELLS - coorY / cellsHeigth);
    }

    private void CreateDrawObject()
    {
        GameObject drawObject = new GameObject("DrawObject");
        drawObject.tag = "drawObject";
        MeshFilter mf = drawObject.AddComponent<MeshFilter>();
        mf.mesh = drawMesh;
        MeshRenderer mr = drawObject.AddComponent<MeshRenderer>();
        mr.material = mat;
        drawObject.transform.position = Vector3.forward;

        drawMesh = new Mesh();

    }

    public void GuardarMatrizSimbolo()
    {
        bool fin = false;
        switch(ControladorPaneles.contadorSimbolos)
        {
            case 1:
                EjemploGuardar.matrizSimbolo1 = matrix;
                break;
            case 2:
                EjemploGuardar.matrizSimbolo2 = matrix;
                break;
            case 3:
                EjemploGuardar.matrizSimbolo3 = matrix;
                fin = true;
                break;
        }
        
        InitializeMatrix();
        if (fin)
            ejemploGuardarScript.GuardarPartida();
        else
        {
            controladorPanelesScript.ActivarDibujo(false);
            controladorPanelesScript.CambiarANombreSimbolo();
        }

    }
	
	private void Update () {

        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            row = Mousey2Row(mousePosition.y);
            column = Mousex2Column(mousePosition.x);
            //Debug.Log("Y = " + mousePosition.y + "--> " + row + " X = " + mousePosition.x + "--> " + column);

            if (matrix[row, column] == 0)
            {
                matrix[row, column] = 1;
            }

            if (!held)
            {
                prevPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                held = true;
            }
            else
            {
                endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                drawScript.AddLineSegmentToDrawMesh(drawMesh, prevPosition, endPosition);
                prevPosition = endPosition;
            }
                
        }

        if (Input.GetMouseButtonUp(0))
        {
            held = false;
            CreateDrawObject();
        }

        Graphics.DrawMesh(drawMesh, Vector3.forward, Quaternion.identity, mat, 0);
	}
}

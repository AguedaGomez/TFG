﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorPaneles : MonoBehaviour {

    public GameObject panelPartida;
    public GameObject panelSimbolo;
    public InputField inputNombrePartida;
    public InputField inputNombreSimbolo;
    public Text textoExplicativoSimbolo;

    private CreateStrokeMatrix createStrokeMatrixComponente;
    private Draw drawComponente;
    public static int contadorSimbolos;

    // Use this for initialization
    void Start()
    {
        panelPartida.SetActive(true);
        panelSimbolo.SetActive(false);
        createStrokeMatrixComponente = GetComponent<CreateStrokeMatrix>();
        drawComponente = GetComponent<Draw>();
        createStrokeMatrixComponente.enabled = false;
        drawComponente.enabled = false;
        contadorSimbolos = 1;

    }

    public void CambiarAPanelSimbolo()
    {
        EjemploGuardar.nombre = inputNombrePartida.text;
        textoExplicativoSimbolo.text = "Introduce el nombre del símbolo " + contadorSimbolos;
        panelPartida.SetActive(false);
        panelSimbolo.SetActive(true);
    }

    public void ActivarDibujo (bool activar)
    {
        createStrokeMatrixComponente.enabled = activar;
        drawComponente.enabled = activar;
    }

    public void CambiarADibujarSimbolo()
    {
        textoExplicativoSimbolo.text = "Dibuja el símbolo " + contadorSimbolos;
        switch(contadorSimbolos)
        {
            case 1:
                EjemploGuardar.nombreSimbolo1 = inputNombrePartida.text;
                break;
            case 2:
                EjemploGuardar.nombreSimbolo2 = inputNombrePartida.text;
                break;
            case 3:
                EjemploGuardar.nombreSimbolo3 = inputNombrePartida.text;
                break;
        }
        inputNombreSimbolo.gameObject.SetActive(false);

        createStrokeMatrixComponente.Invoke("GuardarMatrizSimbolo", 5);

    }

    public void CambiarANombreSimbolo()
    {
        contadorSimbolos++;
        textoExplicativoSimbolo.text = "Introduce el nombre del símbolo " + contadorSimbolos;
        inputNombreSimbolo.gameObject.SetActive(true);
        GameObject[] drawGOArray = GameObject.FindGameObjectsWithTag("drawObject");
        foreach (GameObject dGO in drawGOArray)
        {
            Destroy(dGO);
        }
        
    }
}
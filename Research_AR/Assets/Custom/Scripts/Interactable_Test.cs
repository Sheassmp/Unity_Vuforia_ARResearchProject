using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Interactable_Test : Interactable

{
    public GameObject Cubee, _Canvas, instructCanvas, _Results, ResButn;
    public GameObject[] buttonsOb;
    public List<Color> colorsCorrect;
    public List<Color> colorsDecoyA;
    public List<Color> colorsDecoyB;
    public List<Button> buttons;
    public Text colNum, res, subRes, cubeNum;
    public Button btnA, btnB, btnC;
    public AudioClip nextChoiceTick;

    private Color[] primeCol = new Color[8];
    private int lastNumber;
    private bool printedCol;
    private Renderer rend;
    private string display = "";
    private string subDisplay = "";

    public AudioSource audioSource;
    bool done = false;
    int i = 0;
    int z = 0;
    List<string> Answer;
    List<string> subAnswer;

    public void Awake()
    {
        Answer = new List<string>();
        subAnswer = new List<string>();

        _Results.SetActive(false);
        ResButn.SetActive(false);
        _Canvas.SetActive(false);
        printedCol = false;

        buttons.Add(btnA);
        buttons.Add(btnB);
        buttons.Add(btnC);


        cubeNum = instructCanvas.GetComponentInChildren<Text>();
        rend = Cubee.GetComponent<Renderer>();
        colNum.GetComponent<Text>();
        audioSource.GetComponent<AudioSource>();
    }

    //Start is called before the first frame update
    protected override void OnMouseDown()
    {
        Respond();
    }

    protected override void Respond()
    {
        if (printedCol)
        {
            return;
        }
        StartCoroutine(StartTest());
    }

    public void ChangeColor()
    {
        primeCol[0] = Color.blue;
        primeCol[1] = Color.red;
        primeCol[2] = Color.green;
        primeCol[3] = Color.cyan;
        primeCol[4] = Color.black;
        primeCol[5] = Color.magenta;
        primeCol[6] = Color.red;
        primeCol[7] = Color.yellow;

        Color color = primeCol[UnityEngine.Random.Range(0, primeCol.Length)];
        colorsCorrect.Add(color);
        rend.material.SetColor("_Color", color);
    }
    private IEnumerator StartTest()
    {
        for (int y = 1; y <= 10; y++)
        {
            ChangeColor();
            printedCol = true;
            //hopefully change intructino text to number of cube
            cubeNum.text = y.ToString();
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(2);
        PrintColors();
        ShuffleColors();
    }

    public void PrintColors()
    {
        foreach (var _col in colorsCorrect)
        {
            Debug.Log(_col);
        }
        _Canvas.SetActive(true);
    }

    public void NextColor()
    {
        if (i < 10)
        {
            colNum.text = "Choose Cube Color #" + (i + 1);
            audioSource.PlayOneShot(nextChoiceTick, 0.5f);
            int index = GetRandom(0, buttons.Count);

            switch (index)
            {
                case 0:
                    btnA.image.color = colorsCorrect[i];
                    btnC.image.color = colorsDecoyA[i];
                    btnB.image.color = colorsDecoyB[i];
                    Debug.Log("A is Correct");
                    Answer.Add("A");
                    if (IsColorIdentical(colorsDecoyA[i], colorsDecoyB[i]))
                    {
                        Debug.Log("same ab");
                        btnC.image.color = Color.white;
                    }
                    if (IsColorIdentical(colorsCorrect[i], colorsDecoyA[i]))
                    {
                        Debug.Log("same coA");
                        btnC.image.color = Color.gray;
                    }
                    if (IsColorIdentical(colorsCorrect[i], colorsDecoyB[i]))
                    {
                        Debug.Log("same CoB");
                        btnB.image.color = Color.grey;
                    }
                    break;

                case 1:
                    btnB.image.color = colorsCorrect[i];
                    btnA.image.color = colorsDecoyA[i];
                    btnC.image.color = colorsDecoyB[i];
                    Debug.Log("B is Correct");
                    Answer.Add("B");
                    if (IsColorIdentical(colorsDecoyA[i], colorsDecoyB[i]))
                    {
                        Debug.Log("same ab");
                        btnC.image.color = Color.yellow;
                    }
                    if (IsColorIdentical(colorsCorrect[i], colorsDecoyA[i]))
                    {
                        Debug.Log("same coA");
                        btnA.image.color = Color.gray;
                    }
                    if (IsColorIdentical(colorsCorrect[i], colorsDecoyB[i]))
                    {
                        Debug.Log("same CoB");
                        btnC.image.color = Color.white;
                    }
                    break;

                case 2:
                    btnC.image.color = colorsCorrect[i];
                    btnB.image.color = colorsDecoyA[i];
                    btnA.image.color = colorsDecoyB[i];
                    Debug.Log("C is Correct");
                    Answer.Add("C");
                    if (IsColorIdentical(colorsDecoyA[i], colorsDecoyB[i]))
                    {
                        Debug.Log("same ab");
                        btnB.image.color = Color.grey;
                    }
                    if (IsColorIdentical(colorsCorrect[i], colorsDecoyA[i]))
                    {
                        Debug.Log("same coA");
                        btnB.image.color = Color.white;
                    }
                    if (IsColorIdentical(colorsCorrect[i], colorsDecoyB[i]))
                    {
                        Debug.Log("same CoB");
                        btnA.image.color = Color.gray;
                    }
                    break;
            }
            done = false;
            i++;
        }
        else
        {
            colNum.text = "Finished";
            ResButn.SetActive(true);
        }
    }


    void ShuffleColors()
    {
        colorsDecoyA = colorsCorrect.OrderBy(x => UnityEngine.Random.value).ToList();
        colorsDecoyB = colorsCorrect.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    public void CompareAnswers()
    {
        _Results.SetActive(true);

        foreach (var str in Answer)
        {
            display = display.ToString() + str.ToString() + "\n";
        }
        res.text = display;
        // scriptInstance = GetComponent<ButtonOnClick>();

        foreach (var itm in subAnswer)
        {
            subDisplay = subDisplay.ToString() + itm.ToString() + "\n";
        }
        subRes.text = subDisplay;
    }

    public void BUttonGiveName()
    {
        if (z >= 1 && z < 11)
        {
            subAnswer.Add(EventSystem.current.currentSelectedGameObject.name.ToString());
        }
        z++;
    }

    public int GetRandom(int min, int max)
    {
        int rand = Random.Range(min, max);
        while (rand == lastNumber)
            rand = Random.Range(min, max);
        lastNumber = rand;
        return rand;
    }
    public bool IsColorIdentical(Color _providedColor, Color _targetColor)
    {
        return (
            Mathf.Approximately(_providedColor.r, _targetColor.r)
        && Mathf.Approximately(_providedColor.g, _targetColor.g)
        && Mathf.Approximately(_providedColor.b, _targetColor.b)
        );
    }
}

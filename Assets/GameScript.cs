using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    GameObject[,,] gameBoard;
    int[,] rowTotals;
    int[,] colTotals;
    GameObject currPiece;
    GameObject[] allPieces;
    // Start is called before the first frame update
    public float dropTime = 0.0f;
    void Start()
    {
        gameBoard = new GameObject[10, 10, 20];
        rowTotals = new int[10, 20];
        colTotals = new int[10, 20];
        allPieces = new GameObject[7];
        allPieces[0] = GameObject.Find("L Tetro");
        allPieces[1] = GameObject.Find("T Tetro");
        allPieces[2] = GameObject.Find("I Tetro");
        allPieces[3] = GameObject.Find("Square Tetro");
        allPieces[4] = GameObject.Find("Z Tetro");
        allPieces[5] = GameObject.Find("RZ Tetro");
        allPieces[6] = GameObject.Find("J Tetro");
        currPiece = Instantiate(allPieces[Random.Range(0, 7)], new Vector3(0, 40, 0), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
    }

    // Update is called once per frame
    void Update()
    {
        if (currPiece.transform.position.y <= 2)
        {
            currPiece = Instantiate(allPieces[Random.Range(0, 7)], new Vector3(0, 40, 0), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
        }
        if (dropTime >= 0.5f)
        {
            currPiece.transform.Translate(0, -2, 0);
            dropTime = 0.0f;
        }
        dropTime += Time.deltaTime;
    }

    void printOneDimArray(int[] arr)
    {
        foreach (int n in arr)
        {
            Debug.Log(n);
        }
    }

    void printTwoDimArray(int[,,] arr)
    {
        foreach (int n in arr)
        {
            Debug.Log(n);
        }
    }
}

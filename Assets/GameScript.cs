using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    GameObject[,,] listOfBlocks;
    public int[,,] gameBoard;
    int[,] rowTotals;
    int[,] colTotals;
    GameObject currTetro;
    string[] allPieces;
    public float dropTime = 2.0f;
    float currTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // listOfBlocks = new GameObject[10, 10, 20];
        gameBoard = new int[10, 20, 10];
        rowTotals = new int[10, 20];
        colTotals = new int[10, 20];
        allPieces = new string[7];
        allPieces[0] = "L Tetro";
        allPieces[1] = "T Tetro";
        allPieces[2] = "I Tetro";
        allPieces[3] = "Square Tetro";
        allPieces[4] = "Z Tetro";
        allPieces[5] = "RZ Tetro";
        allPieces[6] = "J Tetro";
        createNewTetrisPiece();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currCopy = createInvisibleCopy(currTetro);
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            currCopy.transform.Translate(0.0f, 0.0f, 1.0f, Space.World);
            if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
            {
                Debug.Log("W: Invalid Position");
            }
            else
            {
                currTetro.transform.Translate(0.0f, 0.0f, 1.0f, Space.World);
            }
        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            currCopy.transform.Translate(-1.0f, 0.0f, 0.0f, Space.World);
            if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
            {
                Debug.Log("A: Invalid Position");
            }
            else
            {
                currTetro.transform.Translate(-1.0f, 0.0f, 0.0f, Space.World);
            }
        }
        if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            currCopy.transform.Translate(0.0f, 0.0f, -1.0f, Space.World);
            if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
            {
                Debug.Log("S: Invalid Position");
            }
            else
            {
                currTetro.transform.Translate(0.0f, 0.0f, -1.0f, Space.World);
            }
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            currCopy.transform.Translate(1.0f, 0.0f, 0.0f, Space.World);
            if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
            {
                Debug.Log("D: Invalid Position");
            }
            else
            {
                currTetro.transform.Translate(1.0f, 0.0f, 0.0f, Space.World);
            }
        }
        if (currTime >= dropTime)
        {
            // Debug.Log("Current Block 0: " + getSingleBlockPos(currPiece, 0));
            // Debug.Log("Current Block 1: " + getSingleBlockPos(currPiece, 1));
            // Debug.Log("Current Block 2: " + getSingleBlockPos(currPiece, 2));
            // Debug.Log("Current Block 3: " + getSingleBlockPos(currPiece, 3));

            currCopy.transform.Translate(0.0f, -1.0f, 0.0f, Space.World);
            if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
            {
                for (int i = 0; i < 4; i++)
                {
                    gameBoard[Mathf.RoundToInt(getSingleBlockPos(currTetro, i).x), Mathf.RoundToInt(getSingleBlockPos(currTetro, i).y), Mathf.RoundToInt(getSingleBlockPos(currTetro, i).z)] = 1;
                }
                createNewTetrisPiece();
            }
            else
            {
                currTetro.transform.Translate(0.0f, -1.0f, 0.0f, Space.World);
            }
            currTime = 0.0f;
        }
        currTime += Time.deltaTime;
        Destroy(currCopy);
    }

    public void createNewTetrisPiece()
    {
        int num = Random.Range(0, 7);
        switch (allPieces[num])
        {
            case "L Tetro":
                currTetro = Instantiate(GameObject.Find(allPieces[num]), new Vector3(4, 17, 4), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
                break;
            case "T Tetro":
                currTetro = Instantiate(GameObject.Find(allPieces[num]), new Vector3(4, 18, 4), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
                break;
            case "I Tetro":
                currTetro = Instantiate(GameObject.Find(allPieces[num]), new Vector3(4, 16, 4), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
                break;
            case "Square Tetro":
                currTetro = Instantiate(GameObject.Find(allPieces[num]), new Vector3(4, 18, 4), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
                break;
            case "Z Tetro":
                currTetro = Instantiate(GameObject.Find(allPieces[num]), new Vector3(4, 18, 4), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
                break;
            case "RZ Tetro":
                currTetro = Instantiate(GameObject.Find(allPieces[num]), new Vector3(4, 18, 4), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
                break;
            case "J Tetro":
                currTetro = Instantiate(GameObject.Find(allPieces[num]), new Vector3(4, 17, 4), Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)));
                break;
            default:
                break;
        }
    }

    void dropTetro()
    {

    }

    GameObject createInvisibleCopy(GameObject tetro)
    {
        GameObject copy = Instantiate(tetro, tetro.transform.position, tetro.transform.rotation);
        foreach (Renderer rend in copy.GetComponentsInChildren<Renderer>())
        {
            rend.enabled = false;
        }
        return copy;
    }

    bool checkOutOfBounds(GameObject tetro)
    {
        Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
        for (int i = 0; i < 4; i++)
        {
            Vector3 blockLocation = (tetro.transform.GetChild(i).transform.position - offset);
            if (blockLocation.x < 0 || blockLocation.x > 9)
            {
                return true;
            }
            if (blockLocation.y < 0 || blockLocation.y > 19)
            {
                return true;
            }
            if (blockLocation.z < 0 || blockLocation.z > 9)
            {
                return true;
            }
        }
        return false;
    }

    bool checkIfColliding(GameObject tetro)
    {
        Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
        for (int i = 0; i < 4; i++)
        {
            Vector3 blockLocation = (tetro.transform.GetChild(i).transform.position - offset);
            if (gameBoard[Mathf.RoundToInt(blockLocation.x), Mathf.RoundToInt(blockLocation.y), Mathf.RoundToInt(blockLocation.z)] == 1)
            {
                return true;
            }
        }
        return false;
    }

    Vector3 getSingleBlockPos(GameObject tetro, int childNumber)
    {
        Vector3 blockLocation = tetro.transform.GetChild(childNumber).transform.position;
        Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
        return blockLocation - offset;
    }

    void printSlice(int sliceNumber)
    {
        string toBePrint = "";
        for (int z = 9; z >= 0; z--)
        {
            for (int x = 0; x < 10; x++)
            {
                toBePrint += gameBoard[x, sliceNumber, z] + "";
            }
            toBePrint += "\n";
        }
        Debug.Log(toBePrint);
    }
}

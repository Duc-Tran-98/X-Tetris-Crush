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
    public float dropTime;
    float currTime = 0.0f;
    int cameraPostion;
    // Start is called before the first frame update
    void Start()
    {
        listOfBlocks = new GameObject[10, 10, 20];
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
        cameraPostion = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraPostion)
        {
            case 1:
                if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
                {
                    GameObject currCopy = createInvisibleCopy(currTetro);
                    currCopy.transform.Translate(0.0f, 0.0f, 1.0f, Space.World);
                    if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
                    {
                        Debug.Log("W: Invalid Position");
                    }
                    else
                    {
                        currTetro.transform.Translate(0.0f, 0.0f, 1.0f, Space.World);
                        currTetro.GetComponent<AudioSource>().Play();
                    }
                    Destroy(currCopy);
                }
                if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
                {
                    GameObject currCopy = createInvisibleCopy(currTetro);
                    currCopy.transform.Translate(-1.0f, 0.0f, 0.0f, Space.World);
                    if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
                    {
                        Debug.Log("A: Invalid Position");
                    }
                    else
                    {
                        currTetro.transform.Translate(-1.0f, 0.0f, 0.0f, Space.World);
                        currTetro.GetComponent<AudioSource>().Play();
                    }
                    Destroy(currCopy);
                }
                if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
                {
                    GameObject currCopy = createInvisibleCopy(currTetro);
                    currCopy.transform.Translate(0.0f, 0.0f, -1.0f, Space.World);
                    if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
                    {
                        Debug.Log("S: Invalid Position");
                    }
                    else
                    {
                        currTetro.transform.Translate(0.0f, 0.0f, -1.0f, Space.World);
                        currTetro.GetComponent<AudioSource>().Play();
                    }
                    Destroy(currCopy);
                }
                if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
                {
                    GameObject currCopy = createInvisibleCopy(currTetro);
                    currCopy.transform.Translate(1.0f, 0.0f, 0.0f, Space.World);
                    if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
                    {
                        Debug.Log("D: Invalid Position");
                    }
                    else
                    {
                        currTetro.transform.Translate(1.0f, 0.0f, 0.0f, Space.World);
                        currTetro.GetComponent<AudioSource>().Play();
                    }
                    Destroy(currCopy);
                }
                if (Input.GetKeyDown("."))
                {
                    GameObject currCopy = createInvisibleCopy(currTetro);
                    currCopy.transform.Rotate(0.0f, 0.0f, -90.0f, Space.World);
                    if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
                    {
                        Debug.Log("Right: Invalid Rotation");
                    }
                    else
                    {
                        currTetro.transform.Rotate(0.0f, 0.0f, -90.0f, Space.World);
                        currTetro.GetComponent<AudioSource>().Play();
                    }
                    Destroy(currCopy);
                }
                if (Input.GetKeyDown(","))
                {
                    GameObject currCopy = createInvisibleCopy(currTetro);
                    currCopy.transform.Rotate(0.0f, 0.0f, 90.0f, Space.World);
                    if (checkOutOfBounds(currCopy) || checkIfColliding(currCopy))
                    {
                        Debug.Log("Left: Invalid Rotation");
                    }
                    else
                    {
                        currTetro.transform.Rotate(0.0f, 0.0f, 90.0f, Space.World);
                        currTetro.GetComponent<AudioSource>().Play();
                    }
                    Destroy(currCopy);
                }
                break;
            default:
                break;
        }
        if (Input.GetKeyDown("space"))
        {
            dropTetro();
            GetComponent<AudioSource>().Play();
        }
        if (currTime >= dropTime)
        {
            GameObject secondCopy = createInvisibleCopy(currTetro);
            secondCopy.transform.Translate(0.0f, -1.0f, 0.0f, Space.World);
            if (checkOutOfBounds(secondCopy) || checkIfColliding(secondCopy))
            {
                for (int i = 0; i < 4; i++)
                {
                    int[] blockPostions = getSingleBlockPos(currTetro, i);
                    gameBoard[blockPostions[0], blockPostions[1], blockPostions[2]] = 1;
                }
                createNewTetrisPiece();
                GetComponent<AudioSource>().Play();
            }
            else
            {
                currTetro.transform.Translate(0.0f, -1.0f, 0.0f, Space.World);
            }
            Destroy(secondCopy);
            currTime = 0.0f;
        }
        currTime += Time.deltaTime;
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
        GameObject secondCopy = createInvisibleCopy(currTetro);
        while (true)
        {
            secondCopy.transform.Translate(0.0f, -1.0f, 0.0f, Space.World);
            if (checkOutOfBounds(secondCopy) || checkIfColliding(secondCopy))
            {
                for (int i = 0; i < 4; i++)
                {
                    int[] blockPostions = getSingleBlockPos(currTetro, i);
                    gameBoard[blockPostions[0], blockPostions[1], blockPostions[2]] = 1;
                }
                createNewTetrisPiece();
                break;
            }
            else
            {
                currTetro.transform.Translate(0.0f, -1.0f, 0.0f, Space.World);
            }
        }
        Destroy(secondCopy);
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
            int[] roundedCordinates = new int[3];
            roundedCordinates[0] = Mathf.RoundToInt(blockLocation.x);
            roundedCordinates[1] = Mathf.RoundToInt(blockLocation.y);
            roundedCordinates[2] = Mathf.RoundToInt(blockLocation.z);
            if (roundedCordinates[0] < 0 || roundedCordinates[0] > 9)
            {
                return true;
            }
            if (roundedCordinates[1] < 0 || roundedCordinates[1] > 19)
            {
                return true;
            }
            if (roundedCordinates[2] < 0 || roundedCordinates[2] > 9)
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

    int[] getSingleBlockPos(GameObject tetro, int childNumber)
    {
        Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 blockLocation = tetro.transform.GetChild(childNumber).transform.position - offset;
        int[] positions = new int[3];
        positions[0] = Mathf.RoundToInt(blockLocation.x);
        positions[1] = Mathf.RoundToInt(blockLocation.y);
        positions[2] = Mathf.RoundToInt(blockLocation.z);
        return positions;
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

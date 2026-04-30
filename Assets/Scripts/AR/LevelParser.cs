using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelParser : MonoBehaviour
{
    public TextAsset levelFile;
    public Transform levelRoot;

    [Header("Prefabs")]
    public GameObject basePrefab;
    public GameObject leftBasePrefab;
    public GameObject rightBasePrefab;
    public GameObject groundFloorPrefab; // Lyn
    public GameObject levelFloorPrefab; // Lyn
    public GameObject ladderPrefab;
    public GameObject ladderTopPrefab;
    public GameObject ladderBottomPrefab;
    public GameObject wormPrefab;

    public int wormCount;
    
    void Start()
    {
        LoadLevel();
    }

    // void Update()
    // {
    //     if (Keyboard.current.rKey.wasPressedThisFrame)
    //         ReloadLevel();
    // }

    public int wormCountGetter()
    {
        return wormCount;
    }

    void LoadLevel()
    {
        // Push lines onto a stack so we can pop bottom-up rows. This is easy to reason
        //  about, but an index-based loop over the string array is faster.
        Stack<string> levelRows = new Stack<string>();

        foreach (string line in levelFile.text.Split('\n'))
            levelRows.Push(line);

        int row = 0;
        while (levelRows.Count > 0)
        {
            string rowString = levelRows.Pop();
            char[] rowChars = rowString.ToCharArray();

            for (var columnIndex = 0; columnIndex < rowChars.Length; columnIndex++)
            {
                var currentChar = rowChars[columnIndex];

                // Base Block
                if (currentChar == 'x')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(basePrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Left Base Block
                if (currentChar == 'l')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(leftBasePrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Right Base Block
                if (currentChar == 'r')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(rightBasePrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Ground Floor Block - Lyn
                if (currentChar == 'g')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(groundFloorPrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Level Floor Block - Lyn
                if (currentChar == 'f')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(levelFloorPrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Ladder Block
                if (currentChar == 'u')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(ladderPrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Ladder Top Block
                if (currentChar == 't')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(ladderTopPrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Ladder Bottom Block
                if (currentChar == 'b')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(ladderBottomPrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                }
                
                // Worm Block
                if (currentChar == 'w')
                {
                    Vector3 newPostition = new Vector3(columnIndex + 0.5f, row + 0.5f, -0.5f);
                    Transform dirtInstance = Instantiate(wormPrefab, levelRoot).transform;
                    dirtInstance.position = newPostition;
                    wormCount++;
                }
            }

            row++;
        }

        Debug.Log(wormCount + " worms loaded");

        ScoreSystem scoreSystem = ScoreSystem.Instance != null ? ScoreSystem.Instance : FindFirstObjectByType<ScoreSystem>();
        if (scoreSystem != null)
        {
            scoreSystem.SetWormCount(wormCount);
        }
    }
}
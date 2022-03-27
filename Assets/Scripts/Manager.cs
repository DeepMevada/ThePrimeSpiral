using System;
using System.Collections;
using UnityEngine;

enum Direction
{
    RIGHT = 0,
    UP,
    LEFT,
    DOWN
}

public class Manager : MonoBehaviour
{
    #region Data Members

    public static Manager Instance;

    private int number = 1;
    private int turnCounter = 1;
    private int currentNumOfSteps;
    private float x, y;
    
    [SerializeField] private int maxNumToGo = 100;
    [SerializeField] private int multiplier;
    [SerializeField] private GameObject numberObj;
    [SerializeField] private GameObject line;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform mainContainer;

    private Direction currentDir = Direction.RIGHT;
    
    #endregion
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Application.targetFrameRate = 60;
        mainCamera = Camera.main;
    }

    public void InitData(int maxNumber, int fps, float scaleFactor, bool isNumberVisible)
    {
        maxNumToGo = maxNumber;
        Application.targetFrameRate = fps;
        numberObj.GetComponent<Number>().SetScaleFactor(scaleFactor);
        numberObj.GetComponent<Number>().SetNumbersVisible(isNumberVisible);
    }

    public void StartSpiral()
    {
        number = 1;
        x = y = 0;
        turnCounter = 1;
        currentNumOfSteps = 1;
        currentDir = Direction.RIGHT;
        
        SetCameraOrthographicSize();
        StartCoroutine(SpiralRoutine());
    }

    public void SetCameraOrthographicSize()
    {
        int height = GetHeightOfSpiral(maxNumToGo);

        mainCamera.orthographicSize = (float) height / 2;
    }

    IEnumerator SpiralRoutine()
    {
        while (number <= maxNumToGo)
        {
            var newNum = Instantiate(numberObj, new Vector3(x * multiplier, y * multiplier, 0), Quaternion.identity, mainContainer);
            var numObj = newNum.GetComponent<Number>();
            
            numObj.SetText(number);
            numObj.ChangeForPrime(IsPrime(number));

            var lineObj = Instantiate(line,mainContainer);
            var lineR = lineObj.GetComponent<LineRenderer>();
        
            //Line Starting with Previous X and Y
            lineR.SetPosition(0,new Vector3(x * multiplier, y * multiplier, 0));
        
            switch (currentDir)
            {
                case Direction.RIGHT:
                    x += 1;
                    break;
                case Direction.UP:
                    y += 1;
                    break;
                case Direction.LEFT:
                    x -= 1;
                    break;
                case Direction.DOWN:
                    y -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Line Ending with new X and Y
            lineR.SetPosition(1, number + 1 <= maxNumToGo 
                ? new Vector3(x * multiplier, y * multiplier, 0) 
                : lineR.GetPosition(0));

            if (number % currentNumOfSteps == 0)
            {
                ChangeDirection();
                turnCounter++;
                if (turnCounter % 2 == 0)
                {
                    currentNumOfSteps++;
                }
            }

            number++;

            yield return null;
        }
        
        UIManager.Instance.EnableUI(false);
    }

    public void ClearSpiral()
    {
        foreach (Transform child in mainContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public int GetHeightOfSpiral(int maxSteps)
    {
        int height = 1;
        int turn = 1;
        
        for (int i = 1; i <= maxSteps; i++)
        {
            if (i % height == 0)
            {
                turn++;
                if (turn % 2 == 0)
                {
                    height++;
                }
            }
        }

        return (height + 1) * multiplier;

    }

    public static bool IsPrime(int value)
    {
        if (value == 1) return false;
        
        bool isPrime = true;

        for (int i = 2; i <= value / 2; i++)
        {
            if (value % i == 0)
            {
                isPrime = false;
                break;
            }
        }

        return isPrime;
    }

    private void ChangeDirection()
    {
        var dir = (int) currentDir;
        dir = (dir + 1) % 4;
        currentDir = (Direction) dir;
    }
}

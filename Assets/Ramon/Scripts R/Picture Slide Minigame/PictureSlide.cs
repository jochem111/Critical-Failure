using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureSlide : MonoBehaviour
{
    public GameObject uiHolder;
    public GameObject gameVCam;

    public Texture2D image;
    public int blocksPerLine;
    public int shuffleLength;
    private int shuffleMovesRemaining;
    public float blockMoveSpeed;
    public float shuffleMoveSpeed;
    public float cameraZoom;

    enum PuzzleState { Solved, Shuffling, InPlay };
    PuzzleState state;

    Queue<Block> inputs;
    Block emptyBlock;
    Block[,] blocks;
    Vector2Int previousShuffleOffset;
    bool blockIsMoving;
    public bool hasStartedBefore;


    public GameObject picturePos;
    public GameObject intendedPicturePos;
    public Vector3 neededScale;

    public GameObject exitScreen;
    public GameObject failScreen;
    public GameObject gameScreen;
    public GameObject checkScreenW;
    public GameObject checkScreenF;


    public void OnStart()
    {
        Manager.manager.fadeManager.StartFade(gameVCam, true, uiHolder);
        Invoke("ChangeToOrtographic", Manager.manager.fadeManager.fadeTime);
        ResetProgress();
    }

    void ChangeToOrtographic() { Camera.main.orthographic = true; }

    void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        ExitGame();
    }

    public void ResetProgress()
    {
        if (hasStartedBefore)
        {
            checkScreenW.SetActive(false);
            gameScreen.SetActive(true);

            foreach (Block block in blocks)
            {
                block.SetBlockActive();
            }

            StartShuffle();
        }
        else
        {
            CreateGrid();
        }
    }

    public void CreateGrid()
    {
        blocks = new Block[blocksPerLine, blocksPerLine];
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, blocksPerLine);
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += PlayerMoveBlockInput;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                blocks[x, y] = block;

                if (y == 0 && x == blocksPerLine - 1)
                {
                    emptyBlock = block;
                }
            }
        }

        Camera.main.orthographicSize = blocksPerLine * cameraZoom;
        inputs = new Queue<Block>();

        if (state == PuzzleState.Solved)
        {
            StartShuffle();
        }
    }

    void PlayerMoveBlockInput(Block blockToMove)
    {
        if (state == PuzzleState.InPlay)
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
    }

    void MakeNextPlayerMove()
    {
        while (inputs.Count > 0 && !blockIsMoving)
        {
            MoveBlock(inputs.Dequeue(), blockMoveSpeed);
        }
    }

    void MoveBlock(Block blockToMove, float duration)
    {
        if ((blockToMove.coord - emptyBlock.coord).sqrMagnitude == 1)
        {
            blocks[blockToMove.coord.x, blockToMove.coord.y] = emptyBlock;
            blocks[emptyBlock.coord.x, emptyBlock.coord.y] = blockToMove;

            Vector2Int targetCoord = emptyBlock.coord;
            emptyBlock.coord = blockToMove.coord;
            blockToMove.coord = targetCoord;

            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
        }
    }

    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if (state == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (state == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
            {
                ShuffleBlocks();
            }
            else
            {
                Debug.Log("Puzzlestate = InPlay");
                state = PuzzleState.InPlay;
                hasStartedBefore = true;
                print(picturePos.transform.position + " || " + intendedPicturePos.transform.position);
                picturePos.transform.position = intendedPicturePos.transform.position;
                picturePos.transform.localScale = neededScale;
            }
        }
    }

    void StartShuffle()
    {
        Debug.Log("Puzzlestate = Shuffling");
        state = PuzzleState.Shuffling;
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        ShuffleBlocks();
    }

    void ShuffleBlocks()
    {
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, offsets.Length);

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
            if (offset != previousShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.coord + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                {
                    MoveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveSpeed);
                    shuffleMovesRemaining--;
                    previousShuffleOffset = offset;
                    break;
                }
            }
        }
    }

    void CheckIfSolved()
    {
        foreach (Block block in blocks)
        {
            if (!block.IsAtStartingCoord())
            {
                return;
            }
        }

        Debug.Log("Puzzlestate = Solved");
        state = PuzzleState.Solved;
        emptyBlock.gameObject.SetActive(true);
    }

    //From here on it's just UI stuff and exiting the game.

    public void CheckIfFinished()
    {
        if (state != PuzzleState.Shuffling)
        {
            SetActivePuzzle();
            gameScreen.SetActive(false);

            if (state != PuzzleState.Solved)
            {
                checkScreenF.SetActive(true);
            }
            else
            {
                checkScreenW.SetActive(true);
            }
        }
    }

    public void ExitGame()
    {
        if (Input.GetButtonDown("Cancel") && state == PuzzleState.InPlay && gameScreen.activeInHierarchy)
        {
            SetActivePuzzle();
            gameScreen.SetActive(false);
            exitScreen.SetActive(true);
        }
    }

    public void SetActivePuzzle()
    {
        for (int c = 0; c < transform.childCount; c++)
        {
            if (transform.GetChild(c).gameObject.activeInHierarchy)
            {
                transform.GetChild(c).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(c).gameObject.SetActive(true);
            }
        }

        emptyBlock.gameObject.SetActive(false);
    }

    public void ExitGameYes()
    {
        exitScreen.SetActive(false);
        checkScreenF.SetActive(false);
        failScreen.SetActive(true);
    }

    public void ExitGameNo()
    {
        SetActivePuzzle();
        gameScreen.SetActive(true);
        exitScreen.SetActive(false);
        checkScreenF.SetActive(false);
    }

    public void SucceedExit()
    {
        Camera.main.orthographic = false;
        uiHolder.SetActive(false);
        Manager.manager.starManager.AddStar();
        gameVCam.SetActive(false);
    }

    public void FailedExit()
    {
        Camera.main.orthographic = false;
        uiHolder.SetActive(false);
        Manager.manager.starManager.FailStar();
        gameVCam.SetActive(false);
    }
}

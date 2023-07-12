using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] Rewards rewards;
    [SerializeField] Transform puzzlePos;
    [SerializeField] Transform pieceStartTransform;
    Vector3 pieceFinalPos;
    Vector3 pieceStartPos;

    [SerializeField] Transform puzzlePiecesParent;

    public Transform[] puzzlePieces;

    int currentPuzzle;
    int lastUnlockedPiece;

    bool puzzleCompleted = false;

    private void Awake()
    {
        InitializePuzzle();
        pieceStartPos = pieceStartTransform.position;
    }

    void InitializePuzzle()
    {
        currentPuzzle = PlayerPrefs.GetInt("current puzzle", 0);
        GameObject _puzzle = Instantiate(rewards.puzzles[currentPuzzle], puzzlePos);
        puzzlePiecesParent = _puzzle.transform.Find("content");
        puzzlePieces = puzzlePiecesParent.GetComponentsInChildren<Transform>(true);
        puzzleCompleted = false;
    }

    private void OnEnable()
    {
        if(puzzleCompleted)
            InitializePuzzle();
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            // if the name of the puzzle piece is a number, then it is a puzzle piece
            if (int.TryParse(puzzlePieces[i].name, out int result))
            {
                // creating player prefs for each puzzle piece
                if(!PlayerPrefs.HasKey(currentPuzzle + " puzzle, piece " + i))
                    PlayerPrefs.SetInt(currentPuzzle + " puzzle, piece " + i, 0);
                else
                {
                    // activating the puzzle piece if it is already unlocked
                    int pieceStatus = PlayerPrefs.GetInt(currentPuzzle + " puzzle, piece " + i);
                    if (pieceStatus == 1)
                    {
                        puzzlePieces[i].gameObject.SetActive(true);
                        PlayerPrefs.SetInt("last puzzle piece", i);
                    }
                }
                
            }
        }

        EnableNewRewardPiece();
    }


    private void EnableNewRewardPiece()
    {
        // if there is no last puzzle piece, then it is the first piece
        lastUnlockedPiece = PlayerPrefs.GetInt("last puzzle piece", 0);
        //unlock the next piece
        lastUnlockedPiece++;
        puzzlePieces[lastUnlockedPiece].gameObject.SetActive(true);
        // move the unlocked piece to the start point to animate it
        pieceFinalPos = puzzlePieces[lastUnlockedPiece].position;
        puzzlePieces[lastUnlockedPiece].transform.position = pieceStartPos;
        AnimatePiece();

        PlayerPrefs.SetInt("last puzzle piece", lastUnlockedPiece);
        PlayerPrefs.SetInt(currentPuzzle + " puzzle, piece " + lastUnlockedPiece, 1);
        
        if(lastUnlockedPiece == puzzlePieces.Length - 2)
        {
            PlayerPrefs.SetInt("current puzzle", currentPuzzle+1);
            PlayerPrefs.DeleteKey("last puzzle piece");
            puzzleCompleted = true;
        }
    }

    void AnimatePiece()
    {
        puzzlePieces[lastUnlockedPiece].transform.position = Vector3.MoveTowards(puzzlePieces[lastUnlockedPiece].position, pieceFinalPos, 0.5f);
        if (puzzlePieces[lastUnlockedPiece].transform.position != pieceFinalPos)
        {
            Invoke("AnimatePiece", 0.01f);
        }
    }
}

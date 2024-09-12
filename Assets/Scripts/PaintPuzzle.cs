using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaintPuzzle : MonoBehaviour, Interactor
{
    [SerializeField] GameObject correct;
    [SerializeField] GameObject incorrect;
    List<GameObject> paintings = new List<GameObject>();
    bool isDone;
    int boolIndex;

    private void Awake()
    {
        paintings.Add(correct);
        paintings.Add(incorrect);
        boolIndex = GameManager.Instance.paintings.Count;
        GameManager.Instance.paintings.Add(isDone);
    }

    private void Start()
    {
        CheckBools();
    }

    void UpdateBool(bool newValue)
    {
        isDone = newValue;
        GameManager.Instance.UpdatePaintings(boolIndex, isDone);
    }

    void CheckBools()
    {
        if (correct.activeInHierarchy) isDone = true;
        else isDone = false;

        UpdateBool(isDone);
    }

    public void Interact()
    {
        foreach (var painting in paintings)
        {
            painting.SetActive(!painting.activeSelf);

            if (correct.activeInHierarchy) isDone = true;
            else isDone = false;

            UpdateBool(isDone);
        }
    }
}

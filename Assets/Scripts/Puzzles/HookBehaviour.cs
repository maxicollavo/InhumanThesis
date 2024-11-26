using System.Collections.Generic;
using UnityEngine;

public class HookBehaviour : MonoBehaviour
{
    public int index;
    private int maxIndex = 5;
    public int bodyNum;

    public bool canMove;

    public bool isWinner;
    public int winPos;

    [SerializeField] Animator anim;

    public List<HookBehaviour> otherHooks = new List<HookBehaviour>();

    private void Awake()
    {
        canMove = true;
    }

    public void Mover()
    {
        if (canMove)
        {
            index++;

            if (index > maxIndex)
            {
                anim.SetInteger("State", 5);
                index = 1;
            }
            else
            {
                anim.SetInteger("State", index - 1);
            }

            canMove = false;
        }
    }

    public void WinChecker()
    {
        if (!isWinner) return;

        if (index == winPos)
        {
            GameManager.Instance.rail[bodyNum - 1] = true;
        }
        else
        {
            if (!GameManager.Instance.rail[index]) return;

            GameManager.Instance.rail[bodyNum - 1] = false;
        }

        RailManager.Instance.CheckWin();
    }

    public void AnimatorChanger()
    {
        canMove = true;

        if (index == 1)
        {
            Debug.Log("Entra al foreach");
            foreach (var item in otherHooks)
            {
                item.canMove = true;
            }
        }
    }
}
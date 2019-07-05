using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlManager : MonoBehaviour
{
    private Vector2 startPos = Vector2.zero;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 toMove = Input.mousePosition - startPos;
            if (toMove.magnitude != 0)
            {
                float max = 0;
                max = Mathf.Max(toMove.x, toMove.y);

                if (max == toMove.x)
                    toMove = new Vector3(toMove.x, 0, 0).normalized;
                else
                    toMove = new Vector3(0, toMove.y, 0).normalized;

                player.ChangeDirection(toMove);
            }
        }

*/




        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startPos = Input.touches[0].position;
            }
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Vector2 toMove = Input.touches[0].position - startPos;
                if (toMove.magnitude != 0)
                {
                    float max = 0;
                    max = Mathf.Max(Mathf.Abs(toMove.x), Mathf.Abs(toMove.y));

                    if (max == Mathf.Abs(toMove.x))
                        toMove = new Vector2(toMove.x, 0).normalized;
                    else
                        toMove = new Vector2(0, toMove.y).normalized;

                    player.ChangeDirection(toMove);

                }

                startPos = Vector3.zero;
            }
        }
    }

    public void AutoChange()
    {
        if (startPos.magnitude != 0)
        {
            Vector2 toMove = Input.touches[0].position - startPos;
            if (toMove.magnitude != 0)
            {
                float max = 0;
                max = Mathf.Max(toMove.x, toMove.y);

                if (max == toMove.x)
                    toMove = new Vector2(toMove.x, 0).normalized;
                else
                    toMove = new Vector2(0, toMove.y).normalized;

                player.ChangeDirection(toMove);

            }

            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary)
                startPos = Input.touches[0].position;
        }

    }
}

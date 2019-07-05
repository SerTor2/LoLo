using UnityEngine;
using UnityEngine.EventSystems;

namespace DitzeGames.MobileJoystick
{
    /// <summary>
    /// Put it on any Image UI Element
    /// </summary>
    public class Button : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {

        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public Direction m_Direction = Direction.UP;
        public GameObject m_Player;

        [HideInInspector]
        public bool Pressed;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Pressed)
            {
                switch(m_Direction)
                {
                    case Direction.UP:
                        m_Player.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    case Direction.DOWN:
                        m_Player.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                    case Direction.RIGHT:
                        m_Player.transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                    case Direction.LEFT:
                        m_Player.transform.eulerAngles = new Vector3(0, -90, 0);
                        break;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
    }

}
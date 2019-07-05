using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IRestartGameElement
{
    public KeyCode m_LeftKeyCode = KeyCode.A;
    public KeyCode m_RightKeyCode = KeyCode.D;
    public KeyCode m_UpKeyCode = KeyCode.W;
    public KeyCode m_DownKeyCode = KeyCode.S;

    public GameObject ChildernPlayer;

    Vector3 m_IniPosition;
    Vector3 m_iniPositionChilder;
    private Vector3 target;
    private Vector3 restartForward;
    public Animation m_animation;

    private bool pinchado = false;
    private bool m_Hooked = false;

    private float timeLevel = 0;
    private float lifesLevel = 3;

    public LayerMask m_LayerMask;
    public List<GameObject> corcheras;
    private GameController m_controller;
    private bool died = false;
    public List<AnimationClip> animations;
    public List<AudioClip> audios;
    private AudioSource mysound;
    private bool animationed = false;
    private bool victory = false;
    private bool clear = false;
    public string nextScene;

    private DataManager dataManager;

    private void Start()
    {
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        lifesLevel = 3;
        mysound = GetComponent<AudioSource>();
        m_controller = Camera.main.GetComponent<GameController>();
        m_controller.AddRestartGameElement(this);
        m_controller.AddRestartGameObjects(gameObject);
        target = transform.position;
        m_IniPosition = transform.position;
        m_iniPositionChilder = ChildernPlayer.transform.position;
        restartForward = transform.forward;
        timeLevel = 0;
    }

    private void Update()
    {
        timeLevel += Time.deltaTime;
        if (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, (m_controller.bpm / 130) * 12 * Time.deltaTime);
        }

        pinchado = PinchoDetector();
        if(pinchado && !died)
        {
            m_animation.clip = animations[1];
            m_animation.Play();
            mysound.clip = audios[1];
            mysound.Play();
            died = true;
        }

        victory = true;
        foreach (GameObject go in corcheras)
        {
            if (go.GetComponent<Corchera>().active)
                victory = false;

            if ((go.transform.position.x + 0.2f >= transform.position.x && go.transform.position.x - 0.2f <= transform.position.x) && (go.transform.position.z - 0.2f <= transform.position.z && go.transform.position.z + 0.2f >= transform.position.z))
            {
                if (go.GetComponent<Corchera>().active)
                {
                    mysound.clip = audios[0];
                    mysound.Play();
                    go.GetComponent<Corchera>().active = false;
                    go.GetComponent<Corchera>().hijo.enabled = false;

                }
            }
        }

        if (!m_animation.isPlaying && died)
        {
            timeLevel = 0;
            m_controller.Restart();
        }
        if (victory)
        {
            if (!animationed)
            {
                m_animation.clip = animations[2];
                m_animation.Play();
                animationed = true;
            }
            else
            {
                if(!m_animation.isPlaying && !clear)
                {
                    print(timeLevel);
                    m_controller.ShowStars(timeLevel);
                    clear = true;
                    SceneManager.LoadScene(nextScene);
                }
            }

        }
    }

    public void ChangeDirection(Vector2 move)
    {
        if (move.magnitude != 0)
        {
            switch((int)move.x)
            {
                case 1:
                    gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                    break;
                case -1:
                    gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
                    break;
                default:
                    switch((int)move.y)
                    {
                        case 1:
                            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                            break;
                        case -1:
                            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                            break;
                    }
                    break;
            }
            /*
            if (Input.GetKey(m_UpKeyCode))
            else if (Input.GetKey(m_DownKeyCode))
                this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            else if (Input.GetKey(m_RightKeyCode))
            else if (Input.GetKey(m_LeftKeyCode))
                */
        }
    }

    public void Move()
    {
        if (!died && !victory)
        {
            bool l_Move = true;
            Hook l_Hook;
            m_Hooked = HookDetector(out l_Hook);

            if (m_Hooked && !died)
            {
                l_Move = l_Hook.CanMove();
            }

            if (l_Move)
            {
                if (!PlatformDetector())
                {
                    mysound.clip = audios[1];
                    mysound.Play();
                    m_animation.clip = animations[1];
                    m_animation.Play();
                    died = true;
                }
                else
                {
                    m_animation.clip = animations[0];
                    m_animation.Play();
                    target = transform.position + transform.forward;
                }
            }
            else
            {
                m_animation.clip = animations[3];
                m_animation.Play();
            }
        }
    }

    bool PlatformDetector()
    {
        Ray l_Ray = new Ray(transform.position + Vector3.up, -transform.up);
        RaycastHit l_RaycastHit;

        if (Physics.Raycast(l_Ray, out l_RaycastHit, 100.0f, m_LayerMask.value))
        {
            if(l_RaycastHit.collider.gameObject.GetComponent<Desaparecible>() != null)
            {
                l_RaycastHit.collider.gameObject.GetComponent<Desaparecible>().Pisado();
            }
            return true;
        }
        else


            return false;
    }

    bool PinchoDetector()
    {
        Ray l_Ray = new Ray(transform.position + Vector3.up, -transform.up);
        RaycastHit l_RaycastHit;

        if (Physics.Raycast(l_Ray, out l_RaycastHit, 100.0f, m_LayerMask.value))
        {
            if (l_RaycastHit.collider.gameObject.tag == "DeadZone")
                return true;
            else
                return false;
        }
        else
            return false;
    }

    bool HookDetector(out Hook l_Hook)
    {
        Ray l_Ray = new Ray(transform.position + Vector3.up, -transform.up);
        RaycastHit l_RaycastHit;

        if (Physics.Raycast(l_Ray, out l_RaycastHit, 100.0f, m_LayerMask.value))
        {
            if (l_RaycastHit.collider.gameObject.tag == "Hooker")
            {
                l_Hook = l_RaycastHit.collider.gameObject.GetComponent<Hook>();
                return true;
            }
            else
            {
                l_Hook = null;
                return false;
            }
        }
        else
        {
            l_Hook = null;
            return false;
        }
    }

    public void RestartGame()
    {
        lifesLevel--;

        if (lifesLevel > 0)
        {
            transform.position = m_IniPosition;
            target = transform.position;
            transform.forward = restartForward;
            ChildernPlayer.transform.localScale = Vector3.one;
            ChildernPlayer.transform.position = m_iniPositionChilder;
            died = false;
            m_Hooked = false;
            pinchado = false;
        }
        else
        {
            dataManager.ModifyLifes(-1);
            SceneManager.LoadScene(nextScene);
        }
    }

}

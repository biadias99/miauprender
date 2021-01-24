using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cat_movement : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rig;
    public Transform cam;
    public AudioSource walkingAudio;
    public AudioSource jumpingAudio;
    public GameManager gameManager;
    public float verticalAxis;
    public float horizontalAxis;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public bool canPlayAudio = true;
    public float animMovNumber;
    public float velocity;
    public float customGravity = 15.0f;

    //jump
    public bool canJump = true;
    public bool grounded = false;
    public float jumpHeight = 10.0f;

    void Start()
    {
        // Esconde e trava o cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Instancia o rigbody
        rig = GetComponent<Rigidbody>();

        // Congela a rotação do rigbody
        rig.freezeRotation = true;
    }

    void Update() 
    {

        // Verifica se o personagem está no chão
        if (grounded && Time.timeScale == 1)
        {
            // Pega os inputs
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            // Controla a animação conforme os inputs
            animMovNumber = Mathf.Abs(verticalAxis) + Mathf.Abs(horizontalAxis);
            anim.SetFloat("vertical", animMovNumber);
            
            // Pega a direção atual
            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;
            
            // Verifica a magnitude da direção
            if (direction.magnitude >= 0.1f)
            {
                canJump = true;
                // Pegar o melhor ângulo para apontar o personagem
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                // Faz o ângulo virar não-bruscamente, mais limpo
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                // Rotaciona na direção da câmera
                rig.MoveRotation(Quaternion.Euler(0f, angle, 0f));
                
                // Move de fato sempre na direção da câmera
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                velocity = animMovNumber > 0.6 ? 20.0f : 3.0f;

                // Move o rigbody do personagem
                rig.velocity = moveDir.normalized * velocity;

                if (canPlayAudio)
                {

                    // Toca áudio de andar
                    walkingAudio.Play();
                    canPlayAudio = false;
                }
            }

            else
            {
                canJump = false;
                if (!canPlayAudio)
                {

                    // Para áudio de andar
                    walkingAudio.Stop();
                    canPlayAudio = true;
                }
            }

            // Verifica se pode pular e se o botão de pulo foi pressionado
            if(canJump && Input.GetButton("Jump") && grounded)
            {
                // Toca áudio de pular
                jumpingAudio.Play();

                // Executa o pulo e habilita o chão como falso, pois não está mais no chão
                rig.velocity = new Vector3(rig.velocity.x, 12.0f, rig.velocity.z);
                grounded = false;
                if (!canPlayAudio)
                {
                    // Para áudio de andar
                    walkingAudio.Stop();
                    canPlayAudio = true;
                }
            }
        }

        // Gravidade customizada
        rig.AddForce(new Vector3(0, -customGravity * rig.mass, 0));

        if(PauseMenu.canVerifyPauseMenu)
        {
            if(!PauseMenu.GameIsPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                canPlayAudio = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                walkingAudio.Stop();
                jumpingAudio.Stop();
            }
            PauseMenu.canVerifyPauseMenu = false;
        }
    }

    // Função que verifica se a colisão com algum objeto permaneceu
    void OnCollisionStay(Collision collisionInfo)
    {
        grounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
       gameManager.UpdateSelectedWord(collision.gameObject.name, collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Terrain")
        {
            grounded = false;
        }
    }

}

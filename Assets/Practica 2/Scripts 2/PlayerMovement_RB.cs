using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovementRB : MonoBehaviour
{
    public float walkingspeed, runingSpeed, aceleration, rotationSpeed, JumpForce, sphereRadius; //*, gravityScale*; rotationSpeed o MouseSense
    public string groundName;
    //public LayerMask groundMask;

    private Rigidbody rb; // Referencia al Rigidbody del jugador
    private float x, z, mouseX; //input
    private bool jumpPressed; // Indica si el jugador presionó el botón de salto.
    private bool shiftPressed; // Indica si el jugador está presionando la tecla Shift para correr.
    private float currentSpeed; // Velocidad actual del jugador, interpolada según el estado.
    public int life = 100; // Vida del jugador.
    private bool stun = false; // Indica si el jugador está aturdido

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();// Asigna Rigidbody.
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor para que no salga de la pantalla.
        //gravityScale = -Mathf.Abs(gravityScale); // menos el valor absoluto de la gravedad. Mathf = float
    }

    // Update is called once per frame
    void Update()
    {
        if (!stun) // Solo permite movimiento si el jugador no está aturdido.
        {
            x = Input.GetAxisRaw("Horizontal"); // Movimiento lateral.
            z = Input.GetAxisRaw("Vertical"); // Movimiento hacia adelante/atras.
            mouseX = Input.GetAxis("Mouse X"); // Movimiento del mouse en el eje X para rotar.
            shiftPressed = Input.GetKey(KeyCode.LeftShift); // Detectar si se presiona Shift.

            InterpolationSpeed(); // Interpola la velocidad dependiendo del estado del jugador.

            //jumpPressed = Input.GetAxis("Jump");
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) // Detecta salto si está en el suelo.
            {
                jumpPressed = true;
            }
            RotatePlayer(); // Rota al jugador según el movimiento del mouse.
        }

    }

    public void InterpolationSpeed() // Ajusta la velocidad actual usando interpolación lineal.
    {
        if (shiftPressed) // Si está corriendo.
        {
            currentSpeed = Mathf.Lerp(currentSpeed, runingSpeed, aceleration * Time.deltaTime);
        }
        else if (x != 0 || z != 0) // Si está caminando.
        {
            currentSpeed = Mathf.Lerp(currentSpeed, walkingspeed, aceleration * Time.deltaTime);
        }
        else // Si no se está moviendo.
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, aceleration * Time.deltaTime);
        }
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed; // Retorna la velocidad actual.
    }

    void RotatePlayer()
    {
        Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime; // Calcula la rotación en el eje Y usando el movimiento del mouse.
        transform.Rotate(rotation); // Se aplica la rotacion, tiene numeros imaginarios
    }
    private void FixedUpdate()
    {
        ApplySpeed(); // Aplica movimiento basado en la velocidad actual.
        ApplyJumpForce(); // Aplica la fuerza de salto si corresponde.

    }

    void ApplySpeed()
    {
        //Calcula la velocidad del Rigidbody combinando direccion y velocidad.
        rb.velocity = (transform.forward * currentSpeed * z) + (transform.right * currentSpeed * x) + new Vector3(0, rb.velocity.y, 0);
        //Se aplica la rotacion, tiene numeros imaginarios.
        //*+ (transform.up * gravityScale)*/; GRAVEDAD CONSTANTE NO REALISTA.
        //rb.AddForce(transform.up * gravityScale); GRAVEDAD REALISTA.
        // + new Vector3(0, rb.velocity.y, 0); GRAVEDAD BASE DE UNITY, PUEDE AJUSTARSE EN EL EDITOR.
    }
    
    void ApplyJumpForce()
    {
        if (jumpPressed) // Si el jugador presiono el boton de salto.
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reiniciar la velocidad vertical.
            rb.AddForce(transform.up * JumpForce); // Aplicar fuerza de salto hacia arriba.
            jumpPressed = false;
        } 
    }
    private bool IsGrounded()
    {
        // Detecta si hay colisiones dentro de una esfera debajo del jugador.
        Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
        for(int i = 0; i < colliders.Length; i++)//recorremos elemento a elemento
        {
            //comprobamos si ese elemento es suelo
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Ground")                                                                                                                                                      )
            {
                return true;
            }
        }
        return false; // No esta en el suelo.
    }
    private void OnDrawGizmos()
    {
        // Dibuja un gizmo en el editor para visualizar la esfera de deteccion del suelo.
        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemigo") // Si colisiona con un enemigo.
        {
            Debug.Log(other.tag); // Muestra el tag del objeto en consola.
            life -= 10; // Reduce la vida en 10.
            stun = true; // Activa el estado de aturdimiento.
            StartCoroutine(stunC()); // Inicia la corrutina de aturdimiento.
        }
    }
    public IEnumerator stunC()
    {
        Debug.Log("Stun 5 seg");
        yield return new WaitForSeconds(5); // Esperar 5 segundos.
        stun = false; // Para el estado de aturdimiento.

    }
}

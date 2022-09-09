using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float speed = 30f;    // Player hareket h�z�
    [SerializeField] private float horizontalspeed = 10f; // Player y�n hareket h�z�
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveInput();
    }
    void MoveInput()
    {        

        #region Mobile Controller 4 Direction

        float moveX = transform.position.x; // Player objesinin x pozisyonun de�erini al�r      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun de�erini al�r       

        if (Input.GetKey(KeyCode.LeftArrow))
        {   // E�er klavyede sol ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeLeft de�eri True ise  Sola hareket gider
            moveX = moveX - 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon s�n�rland�r�lmas� yoksa 
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {   // E�er klavyede sa� ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeRight de�eri True ise Sa�a hareket gider  
            moveX = moveX + 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon s�n�rland�r�lmas� yoksa 
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {   // E�er klavyede yukar� ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeUp de�eri True ise �leri hareket gider         
            moveZ = moveZ + 1 * speed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {   // E�er klavyede a�a�� ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeDown de�eri True ise Geri hareket gider         
            moveZ = moveZ - 1 * speed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero; // E�er hareket edilmediyse Player objesi sabit kals�n
        }

        transform.position = new Vector3(moveX, transform.position.y, moveZ);
        // Player objesinin pozisyonu moveX de�erine g�re x ekseninde, moveZ de�erine g�re z ekseninde hareket eder ve y ekseninde sabit kal�r 

        #endregion
    }
}

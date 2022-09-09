using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float speed = 30f;    // Player hareket hýzý
    [SerializeField] private float horizontalspeed = 10f; // Player yön hareket hýzý
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

        float moveX = transform.position.x; // Player objesinin x pozisyonun deðerini alýr      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun deðerini alýr       

        if (Input.GetKey(KeyCode.LeftArrow))
        {   // Eðer klavyede sol ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeLeft deðeri True ise  Sola hareket gider
            moveX = moveX - 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon sýnýrlandýrýlmasý yoksa 
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {   // Eðer klavyede sað ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeRight deðeri True ise Saða hareket gider  
            moveX = moveX + 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon sýnýrlandýrýlmasý yoksa 
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {   // Eðer klavyede yukarý ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeUp deðeri True ise Ýleri hareket gider         
            moveZ = moveZ + 1 * speed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {   // Eðer klavyede aþaðý ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeDown deðeri True ise Geri hareket gider         
            moveZ = moveZ - 1 * speed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero; // Eðer hareket edilmediyse Player objesi sabit kalsýn
        }

        transform.position = new Vector3(moveX, transform.position.y, moveZ);
        // Player objesinin pozisyonu moveX deðerine göre x ekseninde, moveZ deðerine göre z ekseninde hareket eder ve y ekseninde sabit kalýr 

        #endregion
    }
}

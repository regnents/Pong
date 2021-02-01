using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    private float initialForceMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        initialForceMagnitude = Mathf.Sqrt(Mathf.Pow(xInitialForce, 2) + Mathf.Pow(yInitialForce, 2));

        // Mulai game
        RestartGame();
        trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // tentukan besaran gaya pada sumbu vertikal (sumbu y)
        float yForce = Random.Range(-yInitialForce, yInitialForce);

        // Tentukan besaran gaya pada sumbu horizontal (sumbu X)
        // Tentukan berdasarkan kecepatan serta besar gaya sumbu Y
        float xForce = Mathf.Sqrt(Mathf.Pow(initialForceMagnitude, 2) - Mathf.Pow(yForce, 2));

        // Tentukan nilai acak untuk menentukan arah X
        float randomXDirection = Random.Range(0, 2);


        // Jika nilainya di bawah 1, bola bergerak ke kiri. 
        // Jika tidak, bola bergerak ke kanan.
        if (randomXDirection < 1.0f)
        {
            xForce *= (-1);
        }

        // Gunakan gaya untuk menggerakkan bola ini.
        rigidBody2D.AddForce(new Vector2(xInitialForce, yInitialForce));

    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}

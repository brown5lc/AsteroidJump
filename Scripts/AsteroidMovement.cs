using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float minSpinSpeed = 75f;
    public float maxSpinSpeed = 125f;
    private float spinSpeed = 0f;
    public float minScale = 0.25f;
    public float maxScale = 0.5f;
    public ParticleSystem ParticlePrefab;
    private ParticleSystem dustSystem;
    void UpdateDustPosition(Collision2D collision2D) {
        dustSystem.transform.position = collision2D.contacts[0].point;
        dustSystem.transform.rotation = collision2D.transform.rotation;
    }
    void PlayDust() {
        dustSystem.Stop();
        dustSystem.Play();
    }
    void OnCollisionEnter2D(Collision2D collision2D) {
        UpdateDustPosition(collision2D);
        PlayDust();
    }
    void OnCollisionExit2D(Collision2D collision2D) {
        PlayDust();
    }
    void InitializeDustSystem() {
        dustSystem = Instantiate(ParticlePrefab);
        dustSystem.transform.SetParent(transform);
    }
    void InitializeSpinSpeed() {
        spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
    }
    void InitializeScale() {
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, 1);
    }
    void HandleRotate() {
        transform.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
    }
    void HandleDestroySelf() {
        if (ShouldDestroySelf()) {
            DestorySelf();
        }
    }
    bool ShouldDestroySelf() {
        return (Camera.main.transform.position.y - transform.position.y > Camera.main.orthographicSize * 2 + (Camera.main.orthographicSize / 2));
    }
    void DestorySelf() {
        Destroy(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeDustSystem();
        InitializeSpinSpeed();
        InitializeScale();
    }
    // Update is called once per frame
    void Update()
    {
        HandleRotate();
        HandleDestroySelf();
    }
}

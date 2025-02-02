using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Aviao : MonoBehaviour
{
    [SerializeField] //faz aparecer na unity
    private float forca;
    [SerializeField]
    private UnityEvent aoBater;
    [SerializeField]
    private UnityEvent aoPassarPorObstaculo;
    private Rigidbody2D fisica;
    private Vector3 posicaoInicial;
    private bool deveImpulsionar = false;
    private Animator animacao;

    private void Awake()
    {
        this.fisica = this.GetComponent<Rigidbody2D>();
        this.posicaoInicial = this.transform.position;
        this.animacao = this.GetComponent<Animator>();
    }

    private void Update()
    {
        this.animacao.SetFloat("VelocidadeY", this.fisica.velocity.y);
    }

    public void DarImpulso()
    {
        this.deveImpulsionar = true;
    }
    private void FixedUpdate()
    {
        if (this.deveImpulsionar)
        {
            this.Impulsionar();
        }
    }

    private void Impulsionar()
    {
        this.fisica.velocity = Vector2.zero;
        this.fisica.AddForce(Vector2.up * this.forca, ForceMode2D.Impulse);
        this.deveImpulsionar = false;
    }

    public void Reiniciar()
    {
        this.transform.position = this.posicaoInicial;
        this.fisica.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        this.fisica.simulated = false;
        this.aoBater.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        this.aoPassarPorObstaculo.Invoke();
    }
}

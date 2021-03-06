using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    private SoundsManager soundsManager;
    private PaddleManager paddleManager;
    [HideInInspector] public ParticleSystem particle;

    // Variables
    public bool isAuto;
    private float cooldown;

    // Components
    private HingeJoint2D hinge;
    private JointMotor2D motor;

    // Button Refs
    public Sprite buttonUnpressed;
    public Sprite buttonPressed;

    
    
    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        cooldown = 0;
        soundsManager = PlayerManager.instance.GetComponent<SoundsManager>();
        paddleManager = GetComponentInParent<PaddleManager>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
    }

    public void EnablePaddle()
    {
        if (cooldown >= 0.1f)
        {
            paddleManager.Payout(PlayerManager.instance.playerTicketBuffs.paddleBuff);
            particle.Play();
            //soundsManager.PlaySound("flipper");
            hinge.useMotor = true;
            cooldown = 0;
        }

    }

    public void DisablePaddle()
    {
        hinge.useMotor = false;
    }

    public IEnumerator AutoPaddle()
    {
        EnablePaddle();
        yield return new WaitForSeconds(0.1f);
        DisablePaddle();
    }
}

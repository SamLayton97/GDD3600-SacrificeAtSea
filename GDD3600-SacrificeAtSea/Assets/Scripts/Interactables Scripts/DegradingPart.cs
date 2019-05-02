using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A submarine part that degrades over time
/// </summary>
public class DegradingPart : MonoBehaviour
{
    #region Fields

    // serialized variables
    [SerializeField] float degredationRate = 0.01f;
    [SerializeField] float repairRate = 5;
    [SerializeField] SubmarineParts myPart = SubmarineParts.ballastTanks;
    [SerializeField] float functionalityThreshold = 30;

    // sprite variables
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite functioningSprite;
    [SerializeField] Sprite damagedSprite;
    [SerializeField] Sprite underRepairSprite;

    // health variables
    [SerializeField] float startingHealth = 100;
    const float MaxHealth = 100;
    float currHealth;
    bool isFunctioning = true;

    // degredation support variables
    float degredationTimer = 0;

    // repair support variables
    bool playerIsColliding = false;

    // update part-functionality event support
    UpdateFunctionalityEvent updateFunctionalityEvent;

    // audio-visual feedback variables
    [SerializeField] RepairTerminalParticles myParticleController;
    [SerializeField] AudioSource repairAudioSource;
    [SerializeField] AudioSource repairCompleteAudioSource;
    [SerializeField] AudioSource malfunctionAudioSource;

    #endregion

    #region Properties

    // Read-access to current health of degrading part
    public float CurrentHealth
    {
        get { return currHealth; }
    }

    // Read-access to functionality threshold of degrading part
    public float FunctionalityThreshold
    {
        get { return functionalityThreshold; }
    }

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // initialize current health to starting health
        currHealth = Mathf.Min(MaxHealth, startingHealth);

        // retrieve references to relevant components
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myParticleController = GetComponent<RepairTerminalParticles>();

        // calculate time to degrade
        degredationTimer = 1 / degredationRate;

        // add self as invoker of Update Functionality event
        updateFunctionalityEvent = new UpdateFunctionalityEvent();
        EventManager.AddUpdateFunctionalityInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        // retrieve interact input
        float interactInput = Input.GetAxisRaw("Interact");

        // if player is colliding with part and interacting with it
        if (playerIsColliding && interactInput != 0)
        {
            // increment health
            currHealth = Mathf.Min(MaxHealth, currHealth + (Time.deltaTime * repairRate));

            // toggle repairing audio-visual feedback on
            myParticleController.ToggleInProgressParticles(true);
            if (!repairAudioSource.isPlaying)
                repairAudioSource.Play();
        }
        // otherwise
        else
        {
            // decrement health
            currHealth = Mathf.Max(0, currHealth - (Time.deltaTime * degredationRate));

            // toggle repairing audio-visual feedback off
            myParticleController.ToggleInProgressParticles(false);
            if (repairAudioSource.isPlaying)
                repairAudioSource.Stop();
        }

        // if part health sinks below threshold and is currently operating
        if (currHealth < functionalityThreshold && isFunctioning)
        {
            // update functionality to be false
            isFunctioning = false;
            updateFunctionalityEvent.Invoke(myPart, false);

            // give audio feedback to reflect new status
            malfunctionAudioSource.Play();
        }
        // if part health rises above threshold and is currently not operating
        else if (currHealth >= functionalityThreshold && !isFunctioning)
        {
            // update functionality to be true
            isFunctioning = true;
            updateFunctionalityEvent.Invoke(myPart, true);

            // give audio-visual feedback to reflect new status
            myParticleController.SpawnRepairCompleteParticles();
            repairCompleteAudioSource.Play();
        }

        // swap sprite according to reflect current state
        if (playerIsColliding && interactInput != 0)
            mySpriteRenderer.sprite = underRepairSprite;
        else if (isFunctioning)
            mySpriteRenderer.sprite = functioningSprite;
        else
            mySpriteRenderer.sprite = damagedSprite;
    }

    // Called on first frame part is in collision with another
    void OnTriggerEnter2D(Collider2D collision)
    {
        // set player-collision to true
        playerIsColliding = true;
    }

    // Called on frame part leaves collision with another
    void OnTriggerExit2D(Collider2D collision)
    {
        // set player collision to false
        playerIsColliding = false;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds given listener for Update Functionality event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddUpdateFunctionalityListener(UnityAction<SubmarineParts, bool> listener)
    {
        updateFunctionalityEvent.AddListener(listener);
    }

    #endregion
}

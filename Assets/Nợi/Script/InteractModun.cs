using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float detectRadius = 1.2f;
    public LayerMask interactLayer;
    public Animator anim;
    private Interactable currentTarget;
    public Rigidbody2D rb;
    public PlayerType playerType;
    public Transform InteractPoint;
    public SpriteRenderer CharSprite;
    void Start()
    {
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        CharSprite=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Detect();
        HandleInput();
        Typestate();
        if(playerType!=PlayerType.None) InteractUIManager.Instance.Hide();
    }
    public void Typestate()
    {
        switch (playerType)
        {
            case PlayerType.Chair:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Un_Chair();
                }
                break;
        }
    }
    void Detect()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            InteractPoint.position,
            detectRadius,
            interactLayer
        );

        if (hit == null)
        {
            Clear();
            return;
        }

        Interactable interactable = hit.GetComponent<Interactable>();
        
        if (interactable != null && interactable != currentTarget)
        {
            Debug.Log("AA12");
            currentTarget = interactable;
            InteractUIManager.Instance.Show(currentTarget.GetText());
        }
    }

    void HandleInput()
    {
        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            TryInteract(currentTarget);
        }
    }

    void Clear()
    {
        if (currentTarget != null)
        {
            currentTarget = null;
            InteractUIManager.Instance.Hide();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(InteractPoint.position, detectRadius);
    }
    public void TryInteract(Interactable Target)
    {
        InteractType interactType = Target.GetComponent<Interactable>().interactType;
        switch (interactType)
        {
            case InteractType.Chair:
            playerType=PlayerType.Chair;
                transform.position=Target.PointNUM.position;
                CharSprite.sortingOrder=Target.Sorting;
                rb.constraints |= RigidbodyConstraints2D.FreezePosition;
                anim.SetFloat("LastY",Target.CharFace);
                GetComponent<PlayerMovement>().enabled=false;
                anim.SetBool("Shitdown",true);
                break;
        }
    }
    public void Un_Chair()
    {
        playerType=PlayerType.None;
        anim.SetBool("Shitdown",false);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        GetComponent<PlayerMovement>().enabled=true;
        CharSprite.sortingOrder=2;
    }
}

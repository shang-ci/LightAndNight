// DialogManager.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("UI References")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    //public Animator dialogAnimator;
    public Image continueIcon;

    [Header("Settings")]
    [SerializeField] private float textSpeed = 0.05f; // 每个字符显示间隔
    [SerializeField] private float autoContinueDelay = 2f; // 自动继续时间

    private Queue<string> sentences = new Queue<string>();
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private bool dialogActive = false;

    //public Image speakerAvatar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialog(Dialog dialog)
    {
        if (dialogActive) return;

        dialogActive = true;
        sentences.Clear();
        dialogBox.SetActive(true);
        //dialogAnimator.Play("DialogEnter");

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

        //speakerAvatar.sprite = dialog.speakerImage;
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
        }

        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";
        continueIcon.gameObject.SetActive(false);

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        continueIcon.gameObject.SetActive(true);

        // 自动继续逻辑
        yield return new WaitForSeconds(autoContinueDelay);
        if (!isTyping) DisplayNextSentence();
    }

    void EndDialog()
    {
        dialogActive = false;
        //dialogAnimator.Play("DialogExit");
        StartCoroutine(DeactivateAfterAnimation());
    }

    IEnumerator DeactivateAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        dialogBox.SetActive(false);
    }

    // 玩家输入检测
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}
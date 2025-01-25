using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using OpenAI;
using OpenAI.Chat;

/// <summary>
/// 管理讨论的类，包括显示消息气泡、与OpenAI API交互等。
/// </summary>
public class DiscussionManager : MonoBehaviour
{
    // UI元素
    [SerializeField] private DiscussionBubble bubblePrefab;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Transform bubblesParent;

    // 事件
    public static Action onMessageReceived;

    // 认证信息
    [SerializeField] private string[] apiKey;
    private OpenAIClient api;

    // 设置
    [SerializeField]
    private List<Message> chatPrompts = new List<Message>();

    // 单例实例
    public static DiscussionManager Instance;

    // 在第一次帧更新之前调用
    void Start()
    {
        // 创建初始消息气泡
        CreateBubble("Hey There ! How can I help you ?", false);

        // 进行认证
        Authenticate();

        // 初始化设置
        Initiliaze();

        // 设置单例实例
        Instance = this;
    }

    // 每帧调用
    void Update()
    {
        
    }

    /// <summary>
    /// 认证OpenAI API密钥。
    /// </summary>
    private void Authenticate()
    {
        api = new OpenAIClient(new OpenAIAuthentication(apiKey[0]));
    }

    /// <summary>
    /// 初始化聊天提示。
    /// </summary>
    private void Initiliaze()
    {
        Message prompt = new Message(OpenAI.Role.System, "You are a Unity expert.");
        chatPrompts.Add(prompt);
    }

    /// <summary>
    /// 处理用户提问按钮点击事件。
    /// </summary>
    public async void AskButtonCallback()
    {
        // 创建用户消息气泡
        CreateBubble(inputField.text, true);

        Message prompt = new Message(OpenAI.Role.User, inputField.text);
        chatPrompts.Add(prompt);

        inputField.text = "";

        ChatRequest request = new ChatRequest(
            messages: chatPrompts,
            model: OpenAI.Models.Model.GPT3_5_Turbo,
            temperature: 0.2);

        try
        {
            var result = await api.ChatEndpoint.GetCompletionAsync(request);

            Message chatResult = new Message(OpenAI.Role.Assistant, result.FirstChoice.ToString());
            chatPrompts.Add(chatResult);

            // 创建回复消息气泡
            CreateBubble(result.FirstChoice.ToString(), false);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    /// <summary>
    /// 创建消息气泡。
    /// </summary>
    /// <param name="message">要显示的消息文本。</param>
    /// <param name="isUserMessage">是否为用户消息。</param>
    public void CreateBubble(string message, bool isUserMessage)
    {
        DiscussionBubble discussionBubble = Instantiate(bubblePrefab, bubblesParent);
        discussionBubble.Configure(message, isUserMessage);

        onMessageReceived?.Invoke();
    }
}
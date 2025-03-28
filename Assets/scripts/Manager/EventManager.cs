using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    // 事件信息基础接口[7](@ref)
    public interface IEventInfo { }

    // 无参数事件容器[7](@ref)
    public class EventInfo : IEventInfo
    {
        public UnityAction Actions;
        public EventInfo(UnityAction action = null) => Actions = action;
    }

    // 泛型参数事件容器[1,7](@ref)
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> Actions;
        public EventInfo(UnityAction<T> action = null) => Actions = action;
    }

    /// <summary>
    /// 全局事件管理器（单例模式）[6,7](@ref)
    /// </summary>
    public class EventManager
    {
        #region 单例实现
        private static EventManager _instance;
        public static EventManager Instance => _instance ??= new EventManager();
        private readonly Dictionary<string, IEventInfo> _eventDict = new();
        #endregion

        #region 杨童童
        public static event Action OnBossDefeated;

        public static void BossDefeated()
        {
            OnBossDefeated?.Invoke();
        }
        #endregion



        #region 事件注册
        public void AddListener(string eventName, UnityAction action)
        {
            if (_eventDict.TryGetValue(eventName, out var info))
                (info as EventInfo).Actions += action;
            else
                _eventDict.Add(eventName, new EventInfo(action));
        }

        public void AddListener<T>(string eventName, UnityAction<T> action)
        {
            if (_eventDict.TryGetValue(eventName, out var info))
                (info as EventInfo<T>).Actions += action;
            else
                _eventDict.Add(eventName, new EventInfo<T>(action));
        }
        #endregion


        #region 事件触发
        public void TriggerEvent(string eventName)
        {
            if (_eventDict.TryGetValue(eventName, out var info))
                (info as EventInfo)?.Actions?.Invoke();
        }

        public void TriggerEvent<T>(string eventName, T param)
        {
            if (_eventDict.TryGetValue(eventName, out var info))
                (info as EventInfo<T>)?.Actions?.Invoke(param);
        }
        #endregion


        #region 事件移除
        public void RemoveListener(string eventName, UnityAction action)
        {
            if (_eventDict.TryGetValue(eventName, out var info))
                (info as EventInfo).Actions -= action;
        }

        public void RemoveListener<T>(string eventName, UnityAction<T> action)
        {
            if (_eventDict.TryGetValue(eventName, out var info))
                (info as EventInfo<T>).Actions -= action;
        }
        #endregion

        #region 辅助功能
        public bool ContainsEvent(string eventName) => _eventDict.ContainsKey(eventName);
        public void ClearAllEvents() => _eventDict.Clear();
        #endregion
    }
}

/* 使用示例：
1. 注册事件：
   EventManager.Instance.AddListener("PlayerDeath", OnDeath);
   EventManager.Instance.AddListener<int>("ScoreUpdate", UpdateScore);

2. 触发事件：
   EventManager.Instance.TriggerEvent("PlayerDeath");
   EventManager.Instance.TriggerEvent("ScoreUpdate", 100);

3. 移除事件：
   EventManager.Instance.RemoveListener("PlayerDeath", OnDeath);

注意事项：
1. 使用前添加 EventSystem 命名空间
2. 场景切换时建议调用 ClearAllEvents() 防止残留监听[7](@ref)
3. 支持任意参数类型扩展（需对应泛型版本）[1](@ref)
4. 建议配合 MonoBehaviour 生命周期管理订阅（OnEnable/OnDisable）[3](@ref)
*/
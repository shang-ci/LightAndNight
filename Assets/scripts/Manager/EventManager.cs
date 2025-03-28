using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    // �¼���Ϣ�����ӿ�[7](@ref)
    public interface IEventInfo { }

    // �޲����¼�����[7](@ref)
    public class EventInfo : IEventInfo
    {
        public UnityAction Actions;
        public EventInfo(UnityAction action = null) => Actions = action;
    }

    // ���Ͳ����¼�����[1,7](@ref)
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> Actions;
        public EventInfo(UnityAction<T> action = null) => Actions = action;
    }

    /// <summary>
    /// ȫ���¼�������������ģʽ��[6,7](@ref)
    /// </summary>
    public class EventManager
    {
        #region ����ʵ��
        private static EventManager _instance;
        public static EventManager Instance => _instance ??= new EventManager();
        private readonly Dictionary<string, IEventInfo> _eventDict = new();
        #endregion

        #region ��ͯͯ
        public static event Action OnBossDefeated;

        public static void BossDefeated()
        {
            OnBossDefeated?.Invoke();
        }
        #endregion



        #region �¼�ע��
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


        #region �¼�����
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


        #region �¼��Ƴ�
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

        #region ��������
        public bool ContainsEvent(string eventName) => _eventDict.ContainsKey(eventName);
        public void ClearAllEvents() => _eventDict.Clear();
        #endregion
    }
}

/* ʹ��ʾ����
1. ע���¼���
   EventManager.Instance.AddListener("PlayerDeath", OnDeath);
   EventManager.Instance.AddListener<int>("ScoreUpdate", UpdateScore);

2. �����¼���
   EventManager.Instance.TriggerEvent("PlayerDeath");
   EventManager.Instance.TriggerEvent("ScoreUpdate", 100);

3. �Ƴ��¼���
   EventManager.Instance.RemoveListener("PlayerDeath", OnDeath);

ע�����
1. ʹ��ǰ��� EventSystem �����ռ�
2. �����л�ʱ������� ClearAllEvents() ��ֹ��������[7](@ref)
3. ֧���������������չ�����Ӧ���Ͱ汾��[1](@ref)
4. ������� MonoBehaviour �������ڹ����ģ�OnEnable/OnDisable��[3](@ref)
*/
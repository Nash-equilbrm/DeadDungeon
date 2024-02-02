using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class DialougeManager : MonoBehaviour
{
    [SerializeField] private string[] m_SentencesList;
    [SerializeField] private Text m_DialougeText;
    [SerializeField] private string m_NextScene;
    Queue<string> m_SentencesQueue;


    private void Start()
    {
        m_SentencesQueue = new Queue<string>(m_SentencesList);
        NextSentence();
    }

    public void NextSentence()
    {
        if (m_SentencesQueue.Count == 0)
            SceneManager.LoadScene(m_NextScene);
        else 
        {
            string sentence = m_SentencesQueue.Dequeue();
            sentence = sentence.Replace("@", Environment.NewLine);
            m_DialougeText.text = sentence;
        }
    }
}

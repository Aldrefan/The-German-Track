using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationParent
{
    public List<SentencePatern> sentenceList = new List<SentencePatern>();
}

public class SentencePatern
{
    public int index;
    public string speaker;
    public string fullSentence;
    public int indexOutput;

    public SentencePatern(int newIndex, string newSpeaker, string newFullSentence, int newIndexOutput)
    {
        index = newIndex;
        speaker = newSpeaker;
        fullSentence = newFullSentence;
        indexOutput = newIndexOutput;
    }
}


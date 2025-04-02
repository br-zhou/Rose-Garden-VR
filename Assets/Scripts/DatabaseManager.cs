using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference dbReference;
    public int messageLimit = 7; // Change this to fetch a different number of messages
    List<string> messages;

    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        messages = new List<string>()
        {
            "Let your dreams blossom like these flowers.",
            "A little sunshine and a lot of love make everything bloom.",
            "Take a moment to breathe and enjoy the beauty around you.",
            "Every day is a chance to grow and flourish.",
            "Happiness is found in the simplest of moments, like a blooming garden."
        };
        StartCoroutine(GetRecentMessages(messageLimit));
    }

    public IEnumerator GetRecentMessages(int limit)
    {
        while (true) // run this every 30s
        {
            var msg = dbReference.Child("notes").OrderByKey().LimitToLast(limit).GetValueAsync();
            yield return new WaitUntil(() => msg.IsCompleted);
            var newMessages = new List<string>();
            if (msg.IsFaulted)
            {
                Debug.LogError("Error fetching data: " + msg.Exception);
            }
            else if (msg.IsCompleted && msg.Result.Exists)
            {
                DataSnapshot snapshot = msg.Result;
                foreach (var child in snapshot.Children)
                {
                    string key = child.Key;
                    string message = child.Value?.ToString();
                    newMessages.Add(message);
                }
            }
            else
            {
                Debug.Log("No recent messages found.");
            }

            if (newMessages.Count > 3)
            {
                messages = newMessages;
            }

            yield return new WaitForSeconds(30f);
        }
    }

    public string GetRandomMessage()
    {
        System.Random random = new System.Random();

        int index = random.Next(messages.Count);
        return messages[index];
    }
}
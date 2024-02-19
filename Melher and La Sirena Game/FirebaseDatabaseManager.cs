using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using static FirebaseDatabaseManager;
using Firebase.Analytics;

public class FirebaseDatabaseManager : MonoBehaviour
{
    public static FirebaseDatabaseManager FirebaseManager { get; private set; }
    public enum CheckRewardResult
    {
        AddedScoreButIsNotInTheTopScores,
        CouldNotAddScoreError,
        AddedScoreAndIsInTheTop,
        TimeoutError,
        CouldNotRetrieveTopScoresError

    }
    public void LogEvent(string eventName, Dictionary<string, object> eventParameters = null)
    {
        if (eventParameters == null)
        {
            FirebaseAnalytics.LogEvent(eventName);
        }
        else
        {
            FirebaseAnalytics.LogEvent(eventName, eventParameters.Select(kvp => new Parameter(kvp.Key, kvp.Value.ToString())).ToArray());
        }
    }

    public bool ShowAds { get; private set; }
    private DatabaseReference databaseReference;
    private bool isFirebaseInitialized;
    private bool initializationFailed;
    private IEnumerator WaitFBStart(float timeoutSeconds)
    {
        if (!initializationFailed)
        {
            float startTime = Time.time;
            while (!isFirebaseInitialized && Time.time - startTime < timeoutSeconds)
                yield return null;
        }
        if (!isFirebaseInitialized)
            Debug.LogError("Firebase initialization timed out.");

    }

    private void Awake()
    {
        if (FirebaseManager != null && FirebaseManager != this)
        {
            Destroy(gameObject);
            return;
        }

        FirebaseManager = this;
        DontDestroyOnLoad(gameObject);

        InitializeFirebase();
    }

    public void SetScoreAndCheckTopScores(string username, float score, int topX, float timeout, Action<CheckRewardResult> callback)
    {
        StartCoroutine(SetScoreAndCheckTopScoresCoroutine(username, score, topX, timeout));
        IEnumerator SetScoreAndCheckTopScoresCoroutine(string username, float score, int topX, float timeout)
        {
            float startTime = Time.time;

            // Wait for Firebase to start
            yield return StartCoroutine(WaitFBStart(30f));

            if (Time.time - startTime > timeout)
            {
                Debug.LogError("Operation timed out.");
                callback(CheckRewardResult.TimeoutError);
                yield break;
            }

            // Begin transaction
            var userRef = databaseReference.Child("users").Child(username);
            var transactionTask = userRef.RunTransaction(mutableData =>
            {
                mutableData.Child("score").Value = score;
                return TransactionResult.Success(mutableData);
            });

            // Wait for transaction to complete or timeout
            yield return new WaitUntil(() => transactionTask.IsCompleted || Time.time - startTime > timeout);
            if (Time.time - startTime > timeout || transactionTask.Exception != null)
            {
                Debug.LogError(transactionTask.Exception != null ? $"Failed to set score: {transactionTask.Exception}" : "Operation timed out.");
                callback(transactionTask.Exception != null ? CheckRewardResult.CouldNotAddScoreError : CheckRewardResult.TimeoutError);
                yield break;
            }

            // Check if the transaction was successful
            if (transactionTask.Exception != null)
            {
                Debug.LogError($"Transaction failed: {transactionTask.Exception}");
                callback(CheckRewardResult.CouldNotAddScoreError);
                yield break;
            }


            // Retrieve the top X scores
            DatabaseReference scoresRef = databaseReference.Child("users");
            var getScoresTask = scoresRef.OrderByChild("score").LimitToLast(topX).GetValueAsync();

            // Wait for scores retrieval or timeout
            yield return new WaitUntil(() => getScoresTask.IsCompleted || Time.time - startTime > timeout);
            if (Time.time - startTime > timeout || getScoresTask.Exception != null)
            {
                Debug.LogError(getScoresTask.Exception != null ? "Failed to retrieve top scores." : "Operation timed out.");
                callback(getScoresTask.Exception != null ? CheckRewardResult.CouldNotRetrieveTopScoresError : CheckRewardResult.TimeoutError);
                yield break;
            }

            // Process the results
            DataSnapshot snapshot = getScoresTask.Result;
            bool isInTopScores = snapshot.Children.Any(childSnapshot => childSnapshot.Key == username);

            // Return the appropriate result
            callback(isInTopScores ? CheckRewardResult.AddedScoreAndIsInTheTop : CheckRewardResult.AddedScoreButIsNotInTheTopScores);
        }
    }

    public void UserExists(string username, Action<bool> callback)
    {
        StartCoroutine(UserExistsCoroutine(username, callback));

        IEnumerator UserExistsCoroutine(string username, Action<bool> callback)
        {
            yield return StartCoroutine(WaitFBStart(30f));

            if (initializationFailed)
            {
                callback(false);
                yield break;
            }

            DatabaseReference userRef = databaseReference.Child("users").Child(username);

            var checkUserTask = userRef.GetValueAsync();
            float timeout = 20.0f;
            float startTime = Time.time;

            yield return new WaitUntil(() => checkUserTask.IsCompleted || Time.time - startTime > timeout);

            if (Time.time - startTime > timeout)
            {
                Debug.LogError("Timeout reached while checking if user exists.");
                callback(false);
                yield break;
            }

            if (checkUserTask.Exception != null)
            {
                Debug.LogError($"Error checking if user exists: {checkUserTask.Exception}");
                callback(false);
            }
            else
            {
                DataSnapshot snapshot = checkUserTask.Result;
                callback(snapshot.Exists);
            }
        }
    }

    public void GetUserRank(string username, float timeout, Action<int> callback)
    {
        StartCoroutine(GetUserRankCoroutine(username, timeout, callback));
        IEnumerator GetUserRankCoroutine(string username, float timeout, Action<int> callback)
        {
            float startTime = Time.time;

            // Wait for Firebase to start
            yield return StartCoroutine(WaitFBStart(30f));

            if (initializationFailed)
                callback(-1);
            if (Time.time - startTime > timeout)
            {
                Debug.LogError("Operation timed out.");
                callback(-1); // Using -1 to indicate an error
                yield break;
            }

            // Retrieve all scores
            DatabaseReference scoresRef = databaseReference.Child("users");
            var getScoresTask = scoresRef.OrderByChild("score").GetValueAsync();

            // Wait for scores retrieval or timeout
            yield return new WaitUntil(() => getScoresTask.IsCompleted || Time.time - startTime > timeout);
            if (Time.time - startTime > timeout || getScoresTask.Exception != null)
            {
                Debug.LogError(getScoresTask.Exception != null ? "Failed to retrieve scores." : "Operation timed out.");
                callback(-1); // Using -1 to indicate an error
                yield break;
            }

            // Process the results
            DataSnapshot snapshot = getScoresTask.Result;
            if (snapshot.Exists && snapshot.HasChildren)
            {
                int rank = 1;
                foreach (var childSnapshot in snapshot.Children.Reverse()) // Assuming higher scores are better
                {
                    if (childSnapshot.Key == username)
                    {
                        callback(rank);
                        yield break;
                    }
                    rank++;
                }

                // User not found in the ranking
                Debug.LogError("User not found in the rankings.");
                callback(-1); // Using -1 to indicate an error
            }
            else
            {
                // No data found
                Debug.LogError("No data found.");
                callback(-1); // Using -1 to indicate an error
            }
        }
    }


    public void SetScore(string username, int scoreToSet, Action<bool> callback)
    {
        StartCoroutine(SetScoreCoroutine());

        IEnumerator SetScoreCoroutine()
        {
            yield return StartCoroutine(WaitFBStart(30f));
            string sanitizedUsername = username.Replace(" ", "_");
            DatabaseReference userRef = databaseReference.Child("users").Child(sanitizedUsername).Child("score");

            var setScoreTask = userRef.SetValueAsync(scoreToSet);
            float timeout = 20.0f;
            float startTime = Time.time;

            yield return new WaitUntil(() => setScoreTask.IsCompleted || Time.time - startTime > timeout);

            if (Time.time - startTime > timeout)
            {
                Debug.LogError("Timeout reached while setting score.");
                callback(false);
                yield break;
            }

            if (setScoreTask.Exception != null)
            {
                Debug.LogError($"Error setting score: {setScoreTask.Exception}");
                callback(false);
            }
            else
            {
                Debug.Log($"Score set successfully for user {username}");
                callback(true);
            }
        }
    }
    public void GetTopXScores(int topX, Action<List<UserScore>> callback)
    {
        StartCoroutine(GetTopXScoresCoroutine());

        IEnumerator GetTopXScoresCoroutine()
        {
            yield return StartCoroutine(WaitFBStart(30f));

            DatabaseReference scoresRef = databaseReference.Child("users");
            var getScoresTask = scoresRef.OrderByChild("score").LimitToLast(topX).GetValueAsync();

            float timeout = 20.0f;
            float startTime = Time.time;

            yield return new WaitUntil(() => getScoresTask.IsCompleted || Time.time - startTime > timeout);

            if (Time.time - startTime > timeout)
            {
                Debug.LogError("Timeout reached while getting scores.");
                callback(null);
                yield break;
            }

            if (getScoresTask.Exception != null)
            {
                Debug.LogError($"Error getting scores: {getScoresTask.Exception}");
                callback(null);
            }
            else
            {
                DataSnapshot snapshot = getScoresTask.Result;
                List<UserScore> topScores = new List<UserScore>();

                foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse())
                {
                    string userName = childSnapshot.Key;
                    var scoreValue = childSnapshot.Child("score").Value;

                    if (scoreValue != null)
                    {
                        float score = float.Parse(scoreValue.ToString());
                        topScores.Add(new UserScore { UserName = userName, Score = score });
                    }
                }

                callback(topScores);
            }
        }
    }

    public void AddNewUser(Action<string> callback, float _timeout)
    {
        StartCoroutine(AddNewUserCoroutine(callback, _timeout));
        IEnumerator AddNewUserCoroutine(Action<string> callback, float timeout) // Coroutine accepts the timeout parameter
        {
            yield return StartCoroutine(WaitFBStart(30f));
            if (isFirebaseInitialized)
            {
                var userCountRef = databaseReference.Child("userCount");
                var transactionResult = userCountRef.RunTransaction(mutableData =>
                {
                    int userCount = 0;
                    if (mutableData.Value != null)
                        userCount = int.Parse(mutableData.Value.ToString());
                    mutableData.Value = userCount + 1;
                    return TransactionResult.Success(mutableData);
                });

                bool isTimeout = false;
                float startTime = Time.time;
                while (!transactionResult.IsCompleted && !isTimeout)
                {
                    if (Time.time - startTime > timeout) // Use the passed timeout value
                    {
                        isTimeout = true;
                    }
                    yield return null;
                }

                if (isTimeout)
                {
                    Debug.LogError("Transaction timeout.");
                    callback?.Invoke(null);
                    yield break;
                }

                if (transactionResult.Exception != null)
                {
                    Debug.LogError($"Failed to increment user count: {transactionResult.Exception}");
                    callback?.Invoke(null);
                }
                else
                {
                    int userCount = int.Parse(transactionResult.Result.Value.ToString());
                    string username = UsernameGenerator.GenerateUsernameFromUserCount(userCount - 1);

                    // Add user to database
                    string sanitizedUsername = username.Replace(" ", "_");
                    DatabaseReference newUserRef = databaseReference.Child("users").Child(sanitizedUsername);

                    var userEntry = new Dictionary<string, object>
                {
                    { "score", 0 }
                };

                    newUserRef.SetValueAsync(userEntry).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError($"Error adding new user: {task.Exception}");
                            callback?.Invoke(null);
                        }
                        else
                        {

                            LogEvent("user_created", new Dictionary<string, object>
                    {
                        { "username", sanitizedUsername }
                    });

                            LocalUser.NewUserStart();
                            LocalUser.Username = sanitizedUsername;
                            Debug.Log($"Successfully added new user: {username}");
                            callback?.Invoke(sanitizedUsername);
                        }
                    });
                }
            }
            else
            {
                callback?.Invoke(null);
            }
        }
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Exception}");
                initializationFailed = true;
                return;
            }
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                isFirebaseInitialized = true;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                initializationFailed = true;
            }
        });


    }
}

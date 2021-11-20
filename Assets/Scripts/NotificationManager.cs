using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [Header("Notification IDs")]
    [SerializeField] private int idleTimeReachedId;
    [SerializeField] private int dailyChallengeResetId;

    // Start is called before the first frame update
    void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "idle_machine_channel",
            Name = "Idle Machine Channel",
            Importance = Importance.Default,
            Description = "Idle Machine notifications",
        };

        var challengeChannel = new AndroidNotificationChannel()
        {
            Id = "daily_challenge_channel",
            Name = "Daily Challenge Channel",
            Importance = Importance.Default,
            Description = "Daily Challenge notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        AndroidNotificationCenter.RegisterNotificationChannel(challengeChannel);


        ScheduleDailyChallengeNotification();
    }


    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            var notification = new AndroidNotification();
            notification.Title = "Your Pinball Machines Are Full!";
            notification.Text = "Collect your idle coins so your machines can start collecting again.";
            notification.FireTime = System.DateTime.Now.AddHours(PlayerManager.instance.maxIdleTime);

            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "idle_machine_channel", idleTimeReachedId);

        }
        else
        {
            var idleNotificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(idleTimeReachedId);

            Debug.Log(idleNotificationStatus);

            if (idleNotificationStatus != NotificationStatus.Unavailable)
            {
                AndroidNotificationCenter.CancelNotification(idleTimeReachedId);
            }
        }

    }

    private void ScheduleDailyChallengeNotification()
    {
        var idleNotificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(dailyChallengeResetId);

        if (idleNotificationStatus != NotificationStatus.Scheduled)
        {
            if (idleNotificationStatus == NotificationStatus.Delivered)
            {
                AndroidNotificationCenter.CancelNotification(idleTimeReachedId);
            }
            var notification = new AndroidNotification();
            notification.Title = "Daily Challenges Reset";
            notification.Text = "Complete dailies to earn gems!";
            notification.FireTime = System.DateTime.Now.AddDays(1).Date;

            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "daily_challenge_channel", dailyChallengeResetId);
        }
    }
}

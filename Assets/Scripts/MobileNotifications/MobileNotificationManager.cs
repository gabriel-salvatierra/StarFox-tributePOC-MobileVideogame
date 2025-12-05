using System;
using Unity.Notifications.Android;
using UnityEngine;

// Recuerden que tienen que agregar el package de Mobile Notifications para que todo esto funcione.
public class MobileNotificationManager : MonoBehaviour
{
    // Puede hacer la clase static directamente.
    public static MobileNotificationManager Instance { get; private set; }
    private AndroidNotificationChannel _notifChannel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Limpieza preventiva inicial.
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        // De esta forma definitos TIPOS de notificaciones, pueden crear todos los que quieran.
        _notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notifications",
            Description = "Reminders to login",
            Importance = Importance.High         
        };

        AndroidNotificationCenter.RegisterNotificationChannel(_notifChannel);

        DisplayNotification("Come back!", "I MISS YOU!!! Come to recolect some coins", IconSelecter.myicon_0, IconSelecter.myicon_1, DateTime.Now.AddDays(1));
    }

    public int DisplayNotification(string title, string text, IconSelecter iconSmall, IconSelecter iconLarge, DateTime fireTime)
    {
        // Estas son realmente las notificaciones que verá el usuario y que al hacer el request, quedan vinculadas a un channel creado previamente.
        AndroidNotification notification = new()
        {
            Title = title,
            Text = text,
            SmallIcon = iconSmall.ToString(),
            LargeIcon = iconLarge.ToString(),
            FireTime = fireTime
        };

        return AndroidNotificationCenter.SendNotification(notification, _notifChannel.Id);
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }
}

public enum IconSelecter
{
    myicon_0,
    myicon_1
}

using System;

// 1. Event Data (Custom Arguments)
// Instead of just passing a string, we encapsulate data in a class inheriting from EventArgs.
// This allows adding more data later without breaking the signature.
public class VideoUploadedEventArgs : EventArgs
{
    public string VideoTitle { get; }
    public DateTime UploadTime { get; }

    public VideoUploadedEventArgs(string title)
    {
        VideoTitle = title;
        UploadTime = DateTime.Now;
    }
}

// 2. Subject (Publisher)
public class YouTubeChannel
{
    public string ChannelName { get; }

    // DECLARATION:
    // We use the standard generic delegate 'EventHandler<T>'.
    // The 'event' keyword adds a layer of protection (encapsulation) 
    // so clients can only use += or -=.
    public event EventHandler<VideoUploadedEventArgs> VideoUploaded;

    public YouTubeChannel(string name)
    {
        ChannelName = name;
    }

    public void UploadVideo(string title)
    {
        Console.WriteLine($"\n[Channel] Uploading '{title}'...");

        // ... heavy processing ...

        // NOTIFICATION:
        // We create the event args and call the protection method.
        OnVideoUploaded(new VideoUploadedEventArgs(title));
    }

    // Standard pattern for invoking events:
    // 1. 'protected virtual' allows derived classes to override the logic.
    // 2. '?.Invoke' is a thread-safe way to check if there are any subscribers (null check).
    protected virtual void OnVideoUploaded(VideoUploadedEventArgs e)
    {
        VideoUploaded?.Invoke(this, e);
    }
}

// 3. Observers (Subscribers)
// These classes don't need to implement a specific interface.
// They just need a method that matches the EventHandler signature: (object sender, T e)
public class Subscriber
{
    public string Name { get; }

    public Subscriber(string name)
    {
        Name = name;
    }

    // The event handler method
    public void OnVideoUploaded(object sender, VideoUploadedEventArgs e)
    {
        var channel = sender as YouTubeChannel;
        Console.WriteLine($"[Subscriber {Name}] Hey! {channel?.ChannelName} uploaded '{e.VideoTitle}' at {e.UploadTime.ToShortTimeString()}");
    }
}

public class AnalyticsService
{
    public void LogUpload(object sender, VideoUploadedEventArgs e)
    {
        Console.WriteLine($"[Analytics System] Logging upload event: {e.VideoTitle}");
    }
}

// 4. Client Code
public class Program
{
    public static void Main()
    {
        var channel = new YouTubeChannel("DotNet Mastery");
        var sub1 = new Subscriber("Alice");
        var sub2 = new Subscriber("Bob");
        var analytics = new AnalyticsService();

        // SUBSCRIPTION using '+='
        // We attach methods to the event.
        channel.VideoUploaded += sub1.OnVideoUploaded;
        channel.VideoUploaded += sub2.OnVideoUploaded;
        channel.VideoUploaded += analytics.LogUpload;

        // Triggering the event
        channel.UploadVideo("Understanding Events in C#");

        // UNSUBSCRIPTION using '-='
        // Crucial for memory management!
        Console.WriteLine("\n[System] Alice unsubscribes.");
        channel.VideoUploaded -= sub1.OnVideoUploaded;

        channel.UploadVideo("Advanced Patterns");

        channel.VideoUploaded -= sub2.OnVideoUploaded;
        channel.VideoUploaded -= analytics.LogUpload;
    }
}
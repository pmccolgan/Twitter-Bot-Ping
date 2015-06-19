#Twitter Bot Ping

Simple console app used to ping a URL as a scheduled task using a background worker.  Made for a twitter bot deployed on AppHarbor. 

Based on this tutorial
http://tech.pro/tutorial/1222/put-the-cloud-to-work-pt-1-create-a-background-worker-using-quartznet

Replace the keys in the app.config:
```
    <add key="URLToPing" value="YOUR_URL" />
    <add key="HourTimeToPing" value="THE_HOUR_PART_OF_THE_TIME_TO_PING" />
    <add key="MinuteTimeToPing" value="THE_MINUTE_PART_OF_THE_TIME_TO_PING" />
```
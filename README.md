#Twitter Bot Ping

Simple console app used to ping a URL as a scheduled task daily at a given hour and minute.  Made to trigger a twitter bot deployed on AppHarbor as a separate instance to the twitter bot, scheduled with Quartz.NET.

Based on this tutorial:
http://tech.pro/tutorial/1222/put-the-cloud-to-work-pt-1-create-a-background-worker-using-quartznet

More on the twitter bot this was built for here:
https://github.com/pmccolgan/Twitter-Bot

Replace the keys in the app.config:
```
    <add key="URLToPing" value="YOUR_URL" />
    <add key="HourTimeToPing" value="THE_HOUR_PART_OF_THE_TIME_TO_PING" />
    <add key="MinuteTimeToPing" value="THE_MINUTE_PART_OF_THE_TIME_TO_PING" />
```
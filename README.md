# SpotHero
 An API that allows a user to enter a date time range and get back the rate at which they would be charged to park for that time span.
 # App start
 Open the solution in VS2017 and press F5. The browser will open and navigate to the Swagger UI and shows an available endpoints
 # Rate information
 ```{
    "rates": [
        {
            "days": "mon,tues,thurs",
            "times": "0900-2100",
            "price": 1500
        },
        {
            "days": "fri,sat,sun",
            "times": "0900-2100",
            "price": 2000
        },
        {
            "days": "wed",
            "times": "0600-1800",
            "price": 1750
        },
        {
            "days": "mon,wed,sat",
            "times": "0100-0500",
            "price": 1000
        },
        {
            "days": "sun,tues",
            "times": "0100-0700",
            "price": 925
        }
    ]
}
```
# Notes
By starting app uses memory database, but you can change it in appsettings.Development.json, if set "UseMemoryDb" to "false", then will be using SqlServer.
All info from "Sample JSON for testing" will seed in the Database.
API support JSON & XML over HTTP.
Unit tests have a place:)

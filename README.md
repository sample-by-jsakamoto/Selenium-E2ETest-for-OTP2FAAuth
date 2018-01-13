# End-to-End Test program sample with Selenium for OTP Two Factor Auth Web site. 

## Test Target Demonstration Web Site

C# on .NET Core 2.0 + ASP.NET Core 2.0 + EFCore 2.0 + SQLite

### Docker Image

You can pull the docker image of this Test Target Demonstration Web Site from bellow.

https://hub.docker.com/r/samplebyjsakamoto/otp2faauthwebappdemo/

Once you pulled this docker image,

```shell
> docker pull samplebyjsakamoto/otp2faauthwebappdemo:latest
```

You can run the demo app server by following command.

```shell
> docker run -p 52375:80 -d samplebyjsakamoto/otp2faauthwebappdemo:latest
```

## E2E Test Program

C# on .NET Core 2.0 + xUnit + Selenium

![demo](.asset/movie001.gif)

### How to compute 2FA code?

Powered by **[Otp.NET](https://www.nuget.org/packages/Otp.NET/)** 


## License

[The Unlicense](LICENSE)
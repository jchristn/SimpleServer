# SimpleServer

A simple HTTP server using Watson (https://github.com/jchristn/watsonwebserver) that displays requests on the console and returns the serialized request to the caller.  Supports both .NET Core and .NET Framework.

## Use Cases

SimpleServer is useful for cases where you need to stand up an HTTP server to simply listen for an enumerate incoming requests.  SimpleServer is also useful for developers that want a simple skeleton to use to start building their own RESTful backend services.

## Getting Started

It's easy.  Clone, build, and run.  SimpleServer listens on port 8888 (no SSL) by default.

```
git clone https://github.com/jchristn/simpleserver.git
dotnet build -f netcoreapp2.2
cd bin/debug/netcoreapp2.2
dotnet SimpleServer.dll
```

Then, just make calls against it using your favorite browser or REST client (POSTman, cURL, etc).

## Need Help?

Please file an issue here or on the Watson Webserver repository and I'll be happy to help.

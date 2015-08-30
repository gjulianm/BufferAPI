# BufferAPI

This is a simple library to allow posting updates to [Buffer](http://bufferapp.com). It does not include features to manage existing updates or accounts.

BufferAPI is a PCL project, so it should be able to run on most architectures. It uses the Task library to support easy asynchronous methods.

## Authentication

The library does not provide methods to authenticate with Buffer. However, it is not difficult to create a login flow following the instructions [on the Buffer API documentation](https://buffer.com/developers/api/oauth). Basically, you should first show a webpage to the user where they log in and authorize your application. Then, when the browser is redirected to the webpage specified in the _redirect_uri_ parameter, you retrieve the auth code encoded in the _code_ parameter in the URL. With that code, you should make a request to buffer specifying your application details and this auth code. Buffer will then return a JSON object, with the access token encoded in the _access_token_ property. Remember to unescape the quote character _"_ (that is, replace _\"_ with _"_ in the retrieved access token).

## Installing

You can use NuGet to install the package to your project.  The package is available at [the NuGet Gallery](https://www.nuget.org/packages/BufferAPI/), you can install it running

    Install-Package BufferAPI
	
in the Package Manager console.

### Building errors: how to fix

It may be possible that, when including this pacakge, you start to see weird errors regarding unresolved references. Try installing the NuGet package _Microsoft.Bcl.Build_ to try and resolve them.

## Usage example

To use the Buffer API, you should instatiate the _BufferService_ class with the access token retrieved from Buffer. Then you can use its methods to push updates to Buffer:


```csharp
var token = GetAccessToken();
var service = new BufferService(token);
var profileResult = service.GetProfiles();

if(!profileResult.Succeeded)
{
	var errorMessage = String.Format("Request not suceeded. Error code {0}, inner exception {1}, response {2}", profileResult.StatusCode, profileResult.InnerException, profileResult.StringContents);
	throw new Exception(errorMessage);
}

var allProfiles = profileResult.Content;
var twitterProfiles = allProfiles.Where(x => x.Service == "twitter");

var response = service.PostUpdate("Hello from BufferAPI", twitterProfiles);

// Check response in the same fashion as profileResult above.
```

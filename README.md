# SimpleAuthorizeAttribute

I started this project because I got frustrated on how difficult it is to setup auth in .NET core for basic home project

In `Startup.cs`

```csharp
services.AddMvc(x =>
{   
    // Authorize
    x.Filters.Add<AuthorizeActionFilter>();
    
    // Role
    x.Filters.Add<UserRoleActionFilter>();
});
```

Then implement:
- `IIdentityLogic` which handles call to data-acces layer to validate username/password/role
- `ISessionUtility` (or extend `SessionUtility`) to handle getting username/password/role from `ISession`

Note that `SessionUtility` only needs keys to get infos from `ISession`

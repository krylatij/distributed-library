﻿namespace DistributedLibrary.UI.Auth;

public class InitialApplicationState
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public string? XsrfToken { get; set; }
}
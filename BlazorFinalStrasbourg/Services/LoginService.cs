using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BlazorFinalStrasbourg.DTOs;

namespace BlazorFinalStrasbourg.Services;

public class LoginService
{

    public bool IsLogged { get; set; }
    public string Token { get; set; }
    public NavigationManager _navigationManager { get; set; }
    public HttpClient _httpClient { get; set; }

    public LoginService(HttpClient httpClient, NavigationManager navigationManager)
    {
        IsLogged = false;
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async void SetHeader()
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
    }


}
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Web.Models;

namespace Web.Tests;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public ApiIntegrationTests(WebApplicationFactory<Program> applicationFactory)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:8080/"),
        };

        _applicationFactory = applicationFactory;
    }

    [Fact]
    public async Task Create_User_Mobile_ReturnsOkResult()
    {
        // Arrange
        var model = new User
        {
            PhoneNumber = "71234567890",
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "api/user/create/");
        request.Headers.Add("x-Device", "mobile");
        request.Content = content;

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Create_User_Mail_ReturnsOkResult()
    {
        // Arrange
        var model = new User
        {
            Name = "Name_Test",
            Email = "test@test.com",
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "api/user/create/");
        request.Headers.Add("x-Device", "mail");
        request.Content = content;

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Create_User_Web_ReturnsOkResult()
    {
        // Arrange
        var model = new User
        {
            Surname = "Surname_Test",
            Name = "Name_Test",
            Patronymic = "Patronymic_Test",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            PassportNumber = "PassportNumber_Test",
            PlaceOfBirth = "PlaceOfBirth_Test",
            PhoneNumber = "71234567890",
            RegistrationAddress = "RegistrationAddress_Test",
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "api/user/create/");
        request.Headers.Add("x-Device", "web");
        request.Content = content;

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Create_User_NoXDeviceHeader_ReturnsBadRequestResult()
    {
        // Arrange
        var model = new User
        {
            PhoneNumber = "71234567890",
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "api/user/create/");
        request.Content = content;

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_User_IncorrectXDeviceHeader_ReturnsBadRequestResult()
    {
        // Arrange
        var model = new User
        {
            PhoneNumber = "71234567890",
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "api/user/create/");
        request.Headers.Add("x-Device", "test");
        request.Content = content;

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Get_User_ReturnsOkResult()
    {
        // Arrange
        var model = new User
        {
            PhoneNumber = "71234567890",
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, "application/json");
        var requestCreate = new HttpRequestMessage(HttpMethod.Post, "api/user/create/");
        requestCreate.Headers.Add("x-Device", "mobile");
        requestCreate.Content = content;

        // Act
        var responseCreate = await _httpClient.SendAsync(requestCreate);
        var data = JsonConvert.DeserializeObject<int>(await responseCreate.Content.ReadAsStringAsync());
        var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"api/user/get?id={data}"));

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Find_User_ReturnsOkResult()
    {
        // Arrange
        var modelCreate = new User
        {
            PhoneNumber = "71234567890",
            Name = "Test"
        };
        var contentCreate = new StringContent(JsonConvert.SerializeObject(modelCreate), encoding: Encoding.UTF8, "application/json");
        var requestCreate = new HttpRequestMessage(HttpMethod.Post, "api/user/create/");
        requestCreate.Headers.Add("x-Device", "mobile");
        requestCreate.Content = contentCreate;
        var model = new UserSearchOptions
        {
            PhoneNumber = "71234567890",
            Name = "Test"
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Get, "api/user/find/");
        request.Content = content;

        // Act
        var responseCreate = await _httpClient.SendAsync(requestCreate);
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

using RestSharp;
using RestSharp.Authenticators;
using WebApplication1.Dto;

namespace WebApplication1.Services;

public class EmployeeService : IEmployeeService
{
    private readonly RestClient _client = new RestClient(new RestClientOptions("http://localhost:7057/integration")
    {
        Authenticator = new HttpBasicAuthenticator("username", "password")
    });
    public async Task<EmployeeShortDto> Register(EmployeeShortDto employee)
    {
        var request = new RestRequest("employee/register", Method.Post);
        request.AddJsonBody(employee);
        var response = await _client.PostAsync<EmployeeShortDto>(request);
        if (response == null)
        {
            throw new BadHttpRequestException("Main Server Response is empty");
        }
        return response;
    }
}
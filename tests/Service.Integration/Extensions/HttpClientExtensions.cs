using System.Net.Http.Json;
using System.Text.Json;
using Service.Integration.Models;
using Service.Models.Requests;
using Session = Service.Integration.Models.Session;

namespace Service.Integration.Extensions;

public static class HttpClientExtensions
{
    public static async Task<SessionControl> CreateSession(this HttpClient client)
    {
        // var req = new SessionRequest { Name = "This is some super nice testing session" };
        var req = new SessionRequest { };
        var res = await client.PostAsJsonAsync("v1/sessions", req);
        var session = await res.Content.ReadFromJsonAsync<SessionControl>(Application.Defaults.Options);

        Assert.NotNull(session);

        return session;
    }

    public static async Task<UserControl> CreateUser(this HttpClient client, Session session, string userName = "Sarah")
    {
        // var req = new UserRequest { Configuration = userName };
        var req = new UserRequest { Configuration = new JsonElement()};
        var res = await client.PostAsJsonAsync($"v1/sessions/{session.Id}/users", req);
        var user = await res.Content.ReadFromJsonAsync<UserControl>(Application.Defaults.Options);

        Assert.NotNull(user);

        return user;
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatesDatabaseClassLibrary;
using System.Net.Http;
using System.Net.Http.Json;

public class StatesPageModel : PageModel
{
    public List<State> States { get; set; } = new();

    public async Task OnGetAsync()
    {
        using var http = new HttpClient();
        http.BaseAddress = new Uri("https://localhost:5001/"); // your API's base URL
        States = await http.GetFromJsonAsync<List<State>>("api/states") ?? new();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        using var http = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };
        var response = await http.DeleteAsync($"api/states/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage(); // Refresh the list
        }

        ModelState.AddModelError(string.Empty, "Failed to delete state.");
        return Page();
    }


}

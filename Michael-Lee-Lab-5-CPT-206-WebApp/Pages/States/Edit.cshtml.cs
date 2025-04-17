using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatesDatabaseClassLibrary;
using System.Net.Http;
using System.Net.Http.Json;

public class EditStateModel : PageModel
{
    [BindProperty]
    public State State { get; set; } = new();

    public async Task OnGetAsync(int id)
    {
        using var http = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };
        var result = await http.GetFromJsonAsync<State>($"api/states/{id}");
        if (result != null)
            State = result;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        using var http = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };
        var response = await http.PutAsJsonAsync($"api/states/{State.StateID}", State);
        if (response.IsSuccessStatusCode)
            return RedirectToPage("./Index");

        ModelState.AddModelError(string.Empty, "Update failed.");
        return Page();
    }
}

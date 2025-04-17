using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatesDatabaseClassLibrary;
using System.Net.Http;
using System.Net.Http.Json;

public class CreateStateModel : PageModel
{
    [BindProperty]
    public State NewState { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        using var http = new HttpClient();
        http.BaseAddress = new Uri("https://localhost:5001/");

        var response = await http.PostAsJsonAsync("api/states", NewState);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("./Index");
        }


        ModelState.AddModelError(string.Empty, "Error creating state");
        return Page();
    }
}

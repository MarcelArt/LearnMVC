using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnMVC.Views.Hello
{
    public class WorldModel : PageModel
    {
        public string Name { get; set; }

        public void OnGet()
        {
            Name = "Hello world";
        }
    }
}

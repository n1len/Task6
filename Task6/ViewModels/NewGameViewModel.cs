using System.ComponentModel.DataAnnotations;

namespace Task6.ViewModels
{
    public class NewGameViewModel
    {
        [Required(ErrorMessage = "Game name is required")]
        public string Name { get; set; }

        public string Tags { get; set; }
    }
}

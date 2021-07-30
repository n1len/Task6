using System.Collections.Generic;

namespace Task6.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public string Tags { get; set; }
    }
}

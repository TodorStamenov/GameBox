using System;
using System.Collections.Generic;

namespace GameBox.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            this.Games = new HashSet<Game>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; private set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogSystem.Data.Contracts.Models;

namespace BlogSystem.Data.Models
{
    public class Category : BaseModel<int>
    {
        private ICollection<Post> posts;

        public Category()
        {
            this.posts = new HashSet<Post>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts
        { 
            get
            {
                return this.posts;
            }
            set
            {
                this.posts = value;
            }
        }
    }
}

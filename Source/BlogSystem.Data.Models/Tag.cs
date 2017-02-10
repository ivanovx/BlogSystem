namespace BlogSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        private ICollection<Post> posts;
        private ICollection<Page> pages;

        public Tag()
        {
            this.posts = new HashSet<Post>();
            this.pages = new HashSet<Page>();
        }

        [Key]
        public int Id { get; set; }

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

        public virtual ICollection<Page> Pages
        {
            get
            {
                return this.pages;
            }
            set
            {
                this.pages = value;
            }
        }
    }
}
using System;

namespace WebApiTest.API.Models
{
    /// <summary>
    /// An author wit Id, Name, Age and Main Category
    /// </summary>
    public class AuthorDto
    {
        /// <summary>
        /// Id of the Author
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the Author
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Age of the Author
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Main Category of the Author
        /// </summary>
        public string MainCategory { get; set; }
    }
}

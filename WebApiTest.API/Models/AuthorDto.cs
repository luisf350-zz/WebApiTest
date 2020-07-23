﻿using System;

namespace WebApiTest.API.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}
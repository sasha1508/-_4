﻿namespace GraphQLProj.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public virtual List<ProductStorage> Storages { get; set; }
    }
}

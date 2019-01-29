using System.Collections.Generic;
using CatMash.Domain;

namespace CatMash.Infra.Data.DataObject
{
    internal class JsonCat
    {
        public IEnumerable<Cat> Images { get; set; } = new List<Cat>();
    }
}

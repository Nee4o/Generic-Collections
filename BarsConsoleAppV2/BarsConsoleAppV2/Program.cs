using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarsConsoleAppV2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Entity> entities = new List<Entity>()
            {
               new Entity { Id = 1, ParentId = 0, Name = "Root entity"},
               new Entity { Id = 2, ParentId = 1, Name = "Child of 1 entity"},
               new Entity { Id = 3, ParentId = 1, Name = "Child of 1 entity"},
               new Entity { Id = 4, ParentId = 2, Name = "Child of 2 entity"},
               new Entity { Id = 5, ParentId = 4, Name = "Child of 4 entity"}
            };

            var dict = ListToDictionary(entities);
            foreach (var item in dict)
            {
                Console.Write($"Key = {item.Key}, ");
                foreach (var item2 in item.Value)
                {
                    if (item.Value.Count > 1)
                    {
                        Console.Write($"Value = List [Entity[Id = {item2.Id}]], ");
                    }
                    else
                    {
                        Console.WriteLine($"Value = List [Entity[Id = {item2.Id}]]");
                    }
                }
                if (item.Value.Count > 1) Console.WriteLine();
            }
            Console.ReadLine();
        }

        private static Dictionary<int, List<Entity>> ListToDictionary(List<Entity> _list)
        {
            var list = _list;
            _list = _list.GroupBy(x => x.ParentId)
                .Select(a => a.OrderBy(x => x.Id).First())
                .ToList();
            var dict = _list.ToDictionary(x => x.ParentId, x => new List<Entity>());
            foreach (var item in list)
            {
                dict[item.ParentId].Add(item);
            }
            return dict;
        }
    }

    public class Entity
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}

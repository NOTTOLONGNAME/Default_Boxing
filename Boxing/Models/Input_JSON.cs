using System;
using System.Collections.Generic;
using System.Text;

namespace Boxing.Models
{
    class Input_JSON
    {
        [Newtonsoft.Json.JsonProperty("cargo_space")]
        public Cargo_Space Cargo_space { get; set; }
        [Newtonsoft.Json.JsonProperty("cargo_groups")]
        public Cargo_Groups[] Cargo_groups { get; set; }
    }

    public class Cargo_Space
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }
        [Newtonsoft.Json.JsonProperty("mass")]
        public int Mass { get; set; }
        [Newtonsoft.Json.JsonProperty("size")]
        public int[] Size { get; set; }
        public void Write_ALL()
        {
            Console.WriteLine($"ID:{Id}\n MASS: {Mass}\n Size: {Size[0]}, {Size[1]}, {Size[2]}\n\n");
        }
    }

    public class Cargo_Groups
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }
        [Newtonsoft.Json.JsonProperty("mass")]
        public int Mass { get; set; }
        [Newtonsoft.Json.JsonProperty("size")]
        public int[] Size { get; set; }
        [Newtonsoft.Json.JsonProperty("sort")]
        public int Sort { get; set; }
        [Newtonsoft.Json.JsonProperty("count")]
        public int Count { get; set; }
        [Newtonsoft.Json.JsonProperty("group_id")]
        public string Group_id { get; set; }

        public void Write_ALL()
        {
            Console.WriteLine($"ID:{Id}\n MASS: {Mass}\n Size: {Size[0]}, {Size[1]}, {Size[2]}\n" +
                $"Sort: {Sort}\n Count: {Count}\n Group_id: {Group_id}\n\n");
        }
    }

}

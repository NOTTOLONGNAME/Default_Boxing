using System;
using System.Collections.Generic;
using System.Text;

namespace Boxing.Models
{

    public class Output_JSON
    {
        public Cargospace cargoSpace { get; set; }
        public Cargo[] cargos { get; set; }
        public Unpacked[] unpacked { get; set; }
    }

    public class Cargospace
    {
        public Loading_Size loading_size { get; set; }
        public float[] position { get; set; }
        public string type { get; set; }
    }

    public class Loading_Size
    {
        public float height { get; set; }
        public float length { get; set; }
        public float width { get; set; }
    }

    public class Cargo
    {
        public Calculated_Size calculated_size { get; set; }
        public string cargo_id { get; set; }
        public int id { get; set; }
        public float mass { get; set; }
        public Position position { get; set; }
        public Size size { get; set; }
        public int sort { get; set; }
        public bool stacking { get; set; }
        public bool turnover { get; set; }
        public string type { get; set; }
    }

    public class Calculated_Size
    {
        public float height { get; set; }
        public float length { get; set; }
        public float width { get; set; }
    }

    public class Position
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public class Size
    {
        public float height { get; set; }
        public float length { get; set; }
        public float width { get; set; }
    }

    public class Unpacked
    {
        public string group_id { get; set; }
        public int id { get; set; }
        public float mass { get; set; }
        public Position position { get; set; }
        public Size size { get; set; }
        public int sort { get; set; }
        public bool stacking { get; set; }
        public bool turnover { get; set; }
    }

}

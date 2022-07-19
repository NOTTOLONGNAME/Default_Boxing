using System;
using System.IO;
using System.Collections.Generic;

namespace Boxing
{
    
    class Cargo_Groups_Comparer : IComparer<Models.Cargo_Groups>
    {
        public int Compare(Models.Cargo_Groups? x, Models.Cargo_Groups? y)
        {
            if (x.Size[0] * x.Size[1] * x.Size[2] > y.Size[0] * y.Size[1] * y.Size[2])
            {
                return -1;
            }
            else
                return 1;
        }
    } 
    public class Package : Models.Cargo_Groups
    {
        public Package(string id, int count, int[] size, string groupid, int mass ) { this.Id = id;this.Size = size; this.Mass = mass;this.Group_id = groupid; }
        public Package clone()
        {
            return new Package(Id, Count ,Size, Group_id, Mass);
        }
        public Package(Models.Cargo_Groups Object) { Id = Object.Id; Size = Object.Size; Mass = Object.Mass; Group_id = Object.Group_id; Count = Object.Count; }
        //                       L W H       
        public int[] Position = {0,0,0};
    }


    public class FillingSpace : Models.Cargo_Space { 
        public FillingSpace() { }
        public FillingSpace(int [] n) {Hangar = new Space(n); }
        
        public class Space
        {
            public object clone()
            {
                return new Space();
            }
            public Space(int [] n) { Size = n; }

            public Space() { }
            public int[] Size = {0,0,0};
            public int[] pos = {0,0,0};
            Space Ontop;
            Space Onright;
            Space Down;
            

            public Package containment;
            public void fill(Package sub)
            {
                if (sub.Size[0] <= Size[0] && sub.Size[1] <= Size[1] && sub.Size[2] <= Size[2])                              //  | - l             
                {                                                                                                            //  -- - w
                    Ontop = new Space();
                    Onright = new Space();
                    Down = new Space();
                    sub.Position = (int[])pos.Clone();

                    Ontop.Size = new int[3];
                    Ontop.pos = new int[3];
                    Onright.Size = new int[3];
                    Onright.pos = new int[3];
                    Down.Size = new int[3];
                    Down.pos = new int[3];

                    // ВЫДЕЛЕНИЕ МЕСТА ПОД ПОДЗОНЫ
                    this.Ontop.Size[0] = sub.Size[0]; // у области сверху площадь устанавливаемой коробки
                    this.Ontop.Size[1] = sub.Size[1]; // и оставшаяся высота до потолка
                    this.Ontop.Size[2] = Size[2] - (sub.Size[2]); //+ pos[2]);
                    this.Ontop.pos = (int[])pos.Clone();
                    this.Ontop.pos[2] += sub.Size[2];  

                    this.Onright.Size[0] = sub.Size[0]; // у области справа длина устанавливаемой коробки и ширина остаточной области
                    this.Onright.Size[1] = Size[1] - (pos[1] + sub.Size[1]);
                    this.Onright.Size[2] = Size[2] - pos[2];// и оставшаяся высота до потолка
                    this.Onright.pos = (int[])pos.Clone();
                    this.Onright.pos[1] += sub.Size[1];

                    this.Down.Size[0] = Size[0] - (sub.Size[0] + pos[0]); // у области справа длина устанавливаемой коробки и ширина остаточной области
                    this.Down.Size[1] = sub.Size[1]; // - (pos[1] + sub.Size[1]);
                    this.Down.Size[2] = Size[2] - pos[2];// и оставшаяся высота до потолка
                    this.Down.pos = (int[])pos.Clone();
                    this.Down.pos[0] += sub.Size[0];
                }
            }
            public Package[] sides(Package sub)
            {
                Package[] res = new Package[6];
                for(int i = 0; i < 6; i++)
                {
                    res[i] = sub.clone();
                    res[i].Size = (int[])sub.Size.Clone();
                }
                res[0].Size = (int[])sub.Size.Clone();
                res[3].Size = (int[])sub.Size.Clone();
                for (int i = 1; i < 3; i++)
                {
                    res[i].Size[0] = res[i-1].Size[2];
                    res[i].Size[1] = res[i - 1].Size[0];
                    res[i].Size[2] = res[i - 1].Size[1];
                }
                res[3].Size[0] = sub.Size[1];
                res[3].Size[1] = sub.Size[0];
                for (int i = 4; i < 6; i++)
                {
                    res[i].Size[0] = res[i - 1].Size[2];
                    res[i].Size[1] = res[i - 1].Size[0];
                    res[i].Size[2] = res[i - 1].Size[1];
                }
                return res;
            }
            public bool anyplace(Package sub) {
                if (sub.Size[0] <= Size[0] && sub.Size[1] <= Size[1] && sub.Size[2] <= Size[2])
                {
                    return true;
                }
                else return false;
            }

            public void fillbox(List<Package> sub,List<Package> Putinto)
            {
                for (int i = 0; i < sub.Count; i++)
                {

                    if (anyplace(sub[i]) == true)
                    {
                        fill(sub[i]);
                        this.containment = sub[i];
                        sub[i].Id += i;
                        Putinto.Add(sub[i]);
                        sub.Remove(sub[i]);
                        Ontop.fillbox(sub, Putinto);
                        Onright.fillbox(sub, Putinto);
                        Down.fillbox(sub, Putinto);
                        break;
                    }
                
                }
                    
                }
            }
       
        //                        L  W  H       
        public int[] Position = { 0, 0, 0 };
        public Space Hangar;
    }
    class Program
    {
        void Output()
        {

        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Remove(path.Length - 10, 10);
            path = Path.Combine(path, "Data");
            foreach (string file in Directory.EnumerateFiles(path, "*.json"))
            {
                var json = File.ReadAllText(file);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Input_JSON>(json);
                Models.Cargo_Groups[] Cargo_Groups = data.Cargo_groups;
                Models.Cargo_Space Cargo_Space = data.Cargo_space;

                Array.Sort(Cargo_Groups, new Cargo_Groups_Comparer());

                Console.WriteLine("Cargo_Space:\n");
                Cargo_Space.Write_ALL();
                Console.WriteLine("Cargo_Groups\n");

                List<Package> Package = new List<Package>(130);


                for (int i = 0, j = 0; i < Cargo_Groups.Length; i++)
                {
                    for (int n = 0; n < Cargo_Groups[i].Count; n++)
                    {
                        if (j >= Package.Count)
                            Package.Add(new Package(Cargo_Groups[i]));
                        Package[j] = new Package(Cargo_Groups[i]);
                        j++;
                    }
                }
                FillingSpace Hangar = new FillingSpace(Cargo_Space.Size);
                List<Package> fin = new List<Package>();
                Hangar.Hangar.fillbox(Package, fin);

                for (int i = 0; i < fin.Count; i++)
                {

                    fin[i].Write_ALL();
                    Console.WriteLine(fin[i].Position[0]);
                    Console.WriteLine(fin[i].Position[1]);
                    Console.WriteLine(fin[i].Position[2]);


                }
                Console.WriteLine("-----------------------------------");/*
                for (int i = 0; i < Package.Count; i++)
                {

                    Package[i].Write_ALL();
                    Console.WriteLine(Package[i].Position[0]);
                    Console.WriteLine(Package[i].Position[1]);
                    Console.WriteLine(Package[i].Position[2]);
                
                }*/
            }
        }
    }
}

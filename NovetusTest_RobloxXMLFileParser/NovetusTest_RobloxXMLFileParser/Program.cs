using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovetusTest_RobloxXMLFileParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"G:\Projects\Novetus\Novetus\maps\2012\2012 - Iron Cafe.rbxl";

            Console.WriteLine("Meshes");
            RobloxXMLParser.SearchNodes(path, "SpecialMesh", "MeshId");

            Console.WriteLine("Mesh Textures");
            RobloxXMLParser.SearchNodes(path, "SpecialMesh", "TextureId");

            Console.WriteLine("Decals");
            RobloxXMLParser.SearchNodes(path, "Decal", "Texture");

            Console.WriteLine("SkyboxBk");
            RobloxXMLParser.SearchNodes(path, "Sky", "SkyboxBk");

            Console.WriteLine("SkyboxDn");
            RobloxXMLParser.SearchNodes(path, "Sky", "SkyboxDn");

            Console.WriteLine("SkyboxFt");
            RobloxXMLParser.SearchNodes(path, "Sky", "SkyboxFt");

            Console.WriteLine("SkyboxRt");
            RobloxXMLParser.SearchNodes(path, "Sky", "SkyboxRt");

            Console.WriteLine("SkyboxUp");
            RobloxXMLParser.SearchNodes(path, "Sky", "SkyboxUp");

            Console.WriteLine("HopperBins");
            RobloxXMLParser.SearchNodes(path, "HopperBin", "TextureId");

            Console.WriteLine("Tools");
            RobloxXMLParser.SearchNodes(path, "Tool", "TextureId");

            Console.WriteLine("Sounds");
            RobloxXMLParser.SearchNodes(path, "Sound", "SoundId");

            Console.WriteLine(path + " parsed.");
            Console.ReadLine();
        }
    }
}

using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string filePath = @"\\.\E:";

        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                FAT32 FATdata = new FAT32();
                FATdata.ReadBoostSector(fileStream);

                Console.WriteLine($"Bytes Per Sector: {FATdata.BytesPerSector}");
                Console.WriteLine($"Sector per Cluster: {FATdata.SectorPerCluster}");
                Console.WriteLine($"Reversed Sector: {FATdata.ReversedSector}");
                Console.WriteLine($"Number Of Fat: {FATdata.NumberOfFAT}");
                Console.WriteLine($"Size of Volume : {FATdata.VolumeSize}");
                Console.WriteLine($"Sector per FAT: {FATdata.SectorPerFAT}");
                Console.WriteLine($"Starting Cluster of RDET: {FATdata.StartingClusterOfRDET}");
                Console.WriteLine($"Type Of FAT: {FATdata.FATType}");


                FATdata.ReadFAT1(fileStream);
                List<FileManager> RootFile = new List<FileManager>();

                FATdata.ReadDET(fileStream, FATdata.StartingClusterOfRDET, ref RootFile);
                for (int i = 0; i < RootFile.Count; i++)
                {
                    RootFile[i].PrintImfomations(1);
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

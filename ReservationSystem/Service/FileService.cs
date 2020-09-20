using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using ReservationSystem.Models;
using ReservationSystem.Service.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReservationSystem.Service
{
    public class FileService : IService
    {
        private readonly string rootDir, officesFileName, roomsFileName, reservationFileName;
        public FileService(FileSettings fileSettings)
        {
            rootDir = fileSettings.RootDir;
            officesFileName = fileSettings.OfficesFileName;
            roomsFileName = fileSettings.RoomsFileName;
            reservationFileName = fileSettings.ReservationsFileName;
        }
        
        public void WriteFile<T>(string content)
        {
            string currPath = "";
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }

            currPath = GetFileName<T>(rootDir);

            if (!File.Exists(currPath))
            {
                using (var file = File.Create(currPath))
                {
                    file.Close();
                }
            }
            
            try
            {
                using (var streamWriter = File.CreateText(currPath))
                {
                    streamWriter.Write(content);
                    streamWriter.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while wirting in the file.");
                Console.WriteLine(ex.InnerException);
            }
        }
                
        public string ReadFile<T>()
        {
            if (!Directory.Exists(rootDir))
            {
                Console.WriteLine("Files root directory is not exist");
                return null;
            }
            string jsonResult, currPath;

            currPath = GetFileName<T>(rootDir);

            if (!File.Exists(currPath))
            {
                Console.WriteLine("File path is not exist");
                return null;
            }
            try
            {
                using (StreamReader streamReader = new StreamReader(currPath))
                {
                    jsonResult = streamReader.ReadToEnd();
                }
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while reading the file.");
                Console.WriteLine(ex.InnerException);
                return null;
            }
        }

       //public void DeleteItem<T>(T item)
       // {
       //     if (!Directory.Exists(rootDir))
       //     {
       //         Console.WriteLine("Files root directory is not exist");
       //         return;
       //     }
       //     string currPath = "";

       //     currPath = GetFileName<T>(rootDir);

       //     if (!File.Exists(currPath))
       //     {
       //         Console.WriteLine("File path is not exist");
       //         return;
       //     }
       //     try
       //     {
       //         List<T> result = GetAll<T>();
       //         result.RemoveAll(x => x.Equals(item));
       //         Clear(currPath);
       //         using (var streamWriter = File.AppendText(currPath))
       //         {
       //             string content = JsonConvert.SerializeObject(result);
       //             streamWriter.WriteLine(content);
       //             streamWriter.Flush();
       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         Console.WriteLine("Exception while processing the file.");
       //         Console.WriteLine(ex.InnerException);
       //         return;
       //     }
       // }

        //public void EditItem<T>(T oldItem, T newItem)
        //{
        //    try
        //    {
        //        List<T> result = GetAll<T>();
        //        DeleteItem<T>(oldItem);
        //        InsertItem<T>(newItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception while processing the file.");
        //        Console.WriteLine(ex.InnerException);
        //        return;
        //    }
        //}

        private string GetFileName<T>(string rootDir)
        {
            string path = "";

            if (typeof(T) == typeof(Office))
            {
                path = rootDir + "\\" + officesFileName;
            }
            else if (typeof(T) == typeof(Room))
            {
                path = rootDir + "\\" + roomsFileName;
            }
            else if (typeof(T) == typeof(Reservation))
            {
                path = rootDir + "\\" + reservationFileName;
            }
            else
                throw new ArgumentException("Type passed is incorrect!");

            return path;
        }

    }
}

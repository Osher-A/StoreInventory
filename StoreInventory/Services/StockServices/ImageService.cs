using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;


namespace StoreInventory.Services.StockServices
{
    public class ImageService
    {
        public byte[] GetUsersImage()
        {
            byte[] byteImage; 
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Select a picture";
            fd.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (fd.ShowDialog() == true)
                byteImage = File.ReadAllBytes(fd.FileName);
            else
                byteImage = null;

         return byteImage;
        }
    }
}

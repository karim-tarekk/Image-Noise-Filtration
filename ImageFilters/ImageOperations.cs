using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageFilters
{
    public class ImageOperations
    {
        /// <summary>
        /// Open an image, convert it to gray scale and load it into 2D array of size (Height x Width)
        /// </summary>
        /// <param name="ImagePath">Image file path</param>
        /// <returns>2D array of gray values</returns>
        public static byte[,] OpenImage(string ImagePath)
        {
            Bitmap original_bm = new Bitmap(ImagePath);
            int Height = original_bm.Height;
            int Width = original_bm.Width;

            byte[,] Buffer = new byte[Height, Width];

            unsafe
            {
                BitmapData bmd = original_bm.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, original_bm.PixelFormat);
                int x, y;
                int nWidth = 0;
                bool Format32 = false;
                bool Format24 = false;
                bool Format8 = false;

                if (original_bm.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    Format24 = true;
                    nWidth = Width * 3;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format32bppArgb || original_bm.PixelFormat == PixelFormat.Format32bppRgb || original_bm.PixelFormat == PixelFormat.Format32bppPArgb)
                {
                    Format32 = true;
                    nWidth = Width * 4;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    Format8 = true;
                    nWidth = Width;
                }
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (y = 0; y < Height; y++)
                {
                    for (x = 0; x < Width; x++)
                    {
                        if (Format8)
                        {
                            Buffer[y, x] = p[0];
                            p++;
                        }
                        else
                        {
                            Buffer[y, x] = (byte)((int)(p[0] + p[1] + p[2]) / 3);
                            if (Format24) p += 3;
                            else if (Format32) p += 4;
                        }
                    }
                    p += nOffset;
                }
                original_bm.UnlockBits(bmd);
            }

            return Buffer;
        }

        /// <summary>
        /// Get the height of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeight(byte[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidth(byte[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(1);
        }

        /// <summary>
        /// Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <param name="PicBox">PictureBox object to display the image on it</param>
        public static void DisplayImage(byte[,] ImageMatrix, PictureBox PicBox)
        {
            // Create Image:
            //==============
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            Bitmap ImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[0] = p[1] = p[2] = ImageMatrix[i, j];
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }
            PicBox.Image = ImageBMP;
        }
        public static void CountingSort(byte[] arr, int n)//O(N)
        {
            byte Max = 0, Min = 255; //O(1)
            for (int i = 0; i <n;i++)//O(N)
            {
                if (arr[i] > Max)//O(1)
                    Max = arr[i];//O(1)
                if (arr[i] < Min)//O(1)
                    Min = arr[i];//O(1)
            }
            byte[] count = new byte[Max - Min + 1];
            int k = 0;//O(1)
            for (int i = 0; i < count.Length; i++)//O(N)
            {
                count[i] = 0;//O(1)
            }
            for (int i = 0; i < arr.Length; i++)//O(N)
            {
                count[arr[i] - Min]++;//O(1)
            }
            for (int i = Min; i <= Max; i++)//O(N)
            {
                while (count[i - Min] > 0)//O(1)
                {
                    arr[k] = (byte)i;//O(1)
                    k++;//O(1)
                    count[i - Min]--;//O(1)
                }
            }
        }
        public static void heapify(byte[] arr, int n, int i)//O(logn)
        {
            int l = 2 * i + 1;//O(1)
            int r = 2 * i + 2;//O(1)
            int max = i;//O(1)
            byte Temp;
            if (l < n && arr[l] > arr[max])//O(1)
                max = l;
            if (r < n && arr[r] > arr[max])//O(1)
                max = r;

            if (max != i)//O(logn)
            {
                Temp = arr[i];
                arr[i] = arr[max];
                arr[max] = Temp;
                heapify(arr, n, max);
            }
        }
        public static void buildHeap(byte[] arr, int n)//O(N)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);
        }
        public static void HeapSort(byte[] arr, int n)//O(NlogN)
        {
            byte Temp;
            buildHeap(arr, n);//O(N)
            for (int i = n - 1; i >= 0; i--)//O(NlogN)
            {
                Temp = arr[i];
                arr[i] = arr[0];
                arr[0] = Temp;
                heapify(arr, i, 0);
            }

        }
        public static int Average(byte[] arr, int n, int t)//O(N)
        {
            int total = n - (2 * t), average = 0, sum = 0;//O(1)
            for (int i = t; i < n - t ; i++)//O(N)
            {
                sum = sum + arr[i];//O(1)
            }
            average = sum / total;//O(1)
            return average;//O(1)
        }
        public static int partition(byte[] arr, int iBegin, int jEnd)//O(N)
        {
            byte x = arr[jEnd];//O(1)
            byte Temp;//O(1)
            int pivote = iBegin;//O(1)
            for (int j = iBegin; j < jEnd; j++)//O(N)
            {
                if (arr[j] <= x)//O(1)
                {
                    Temp = arr[j];//O(1)
                    arr[j] = arr[pivote];//O(1)
                    arr[pivote++] = Temp;//O(1)
                }
            }
            Temp = arr[pivote];//O(1)
            arr[pivote] = arr[jEnd];//O(1)
            arr[jEnd] = Temp;//O(1)
            return pivote;//O(1)
        }
        public static void QuickSort(byte[] arr, int l, int h)//O(N^2)
        {
            if (l < h)
            {
                int piv = partition(arr, l, h);
                QuickSort(arr, l, piv - 1);
                QuickSort(arr, piv + 1, h);
            }

        }
        public static byte AlphaTrim(byte[,] ImageMatrix, int ii, int jj, int n, int t, int SortType)//O(N^2)
        {
            byte[] list;//O(1)
            int[] Di, Dj;//O(1)
            //Just to always ensure that window is an odd number
            if (n % 2 != 0)//O(1)
            {
                list = new byte[n * n];//O(1)
                Di = new int[n * n];//O(1)
                Dj = new int[n * n];//O(1)
            }
            else//O(1)
            {
                list = new byte[(n + 1) * (n + 1)];//O(1)
                Di = new int[(n + 1) * (n + 1)];//O(1)
                Dj = new int[(n + 1) * (n + 1)];//O(1)
            }
            /*
                 This nested loops to get values of i and j that will be added to my current Pixel Location 
                 to check this location within the Image boundaries 
             */
            int iterator = 0;//O(1)
            for (int i = -(n / 2); i <= (n / 2); i++)//O(N^2)
            {
                for (int j = -(n / 2); j <= (n / 2); j++)
                {
                    Dj[iterator] = i;//O(1)
                    Di[iterator] = j;//O(1)
                    iterator++;//O(1)
                }
            }
            int Newi, Newj, listlen = 0;//O(1)
            for (int i = 0; i < n*n; i++)//O(N^2)
            {
                Newi = ii + Di[i];//O(1)
                Newj = jj + Dj[i];//O(1)
                //check this location within the Image Boundries
                if (Newj >= 0 && Newj < GetWidth(ImageMatrix) && Newi >= 0 && Newi < GetHeight(ImageMatrix))//O(1)
                {
                    list[listlen] = ImageMatrix[Newi, Newj];//O(1)
                    listlen++;//O(1)
                }
            }
            //Lets move to Sort List Array
            // To perform the algorithm with two different sort types
            //Counting Sort
            if (SortType == 1)//O(N)
            {
                CountingSort(list, list.Length);//O(N)
            }
            //Heap Sort
            else if (SortType ==2)//O(NLogN)
            {
                HeapSort(list,list.Length);//O(NLogN)
            }
            //Lets move to Remove T elements from Start of array and the end of the array and get the average
            int average = 0;//O(1)
            average = Average(list, list.Length, t);//O(N)
            return (byte) average;//O(1)
        }
        public static byte AdaptiveMedian(byte[,] ImageMatrix, int ii, int jj, int startWindow, int WindowMax, int SortType)
        {
            byte[] list = new byte[startWindow * startWindow];//O(1)
            int[] Di = new int[startWindow * startWindow];//O(1)
            int[] Dj = new int[startWindow * startWindow];//O(1)
            /*
                 This nested loops to get values of i and j that will be added to my current Pixel Location 
                 to check this location within the Image Boundaries
             */
            int iterator = 0;//O(1)
            for (int i = -(startWindow / 2); i <= (startWindow / 2); i++)//O(N^2)
            {
                for (int j = -(startWindow / 2); j <= (startWindow / 2); j++)
                {
                    Dj[iterator] = i;//O(1)
                    Di[iterator] = j;//O(1)
                    iterator++;//O(1)
                }
            }
            int Newi, Newj, listlen = 0;//O(1)
            for (int i = 0; i < startWindow * startWindow; i++)//O(N^2)
            {
                Newi = ii + Di[i];//O(1)
                Newj = jj + Dj[i];//O(1)
                //check this location within the Image Boundries
                if (Newj >= 0 && Newj < GetWidth(ImageMatrix) && Newi >= 0 && Newi < GetHeight(ImageMatrix))//O(1)
                {
                    list[listlen] = ImageMatrix[Newi, Newj];//O(1)
                    listlen++;//O(1)
                }
            }
            //Lets move to Sort List Array
            // To perform the algorithm with two different sort types
           //Counting Sort
            if (SortType == 1)//O(N)
            {
                CountingSort(list, list.Length);//O(N)
            }
            else if (SortType == 2)//O(N^2)
            {
                QuickSort(list, 0, list.Length-1);//O(N^2)
            }
            //Lets move to Remove T elements from Start of array and the end of the array and get the average
            byte min, med, max, Zij = ImageMatrix[ii,jj];//O(1)
            min = list[0];//O(1)
            med = list[list.Length / 2];//O(1)
            max = list[list.Length - 1];//O(1)
            int A1, A2, B1, B2;//O(1)
            A1 = med - min;//O(1)
            A2 = max - med;//O(1)
            if (A1 > 0 && A2 > 0)//O(1)
            {
                B1 = Zij - min;//O(1)
                B2 = max - Zij;//O(1)
                if (B1 > 0 && B2 > 0)//O(1)
                    return Zij;//O(1)
                else
                    return med;//O(1)
            }
            else//O(N^2)
            {
                if (startWindow + 2 < WindowMax)//O(N^2)
                    return AdaptiveMedian(ImageMatrix, ii, jj, startWindow + 2, WindowMax, SortType);//O(N^2)
                else
                    return med;//O(1)
            }
        }
        public static byte[,] SelectFilter(byte[,] ImageMatrix, int n, int t, int SortType, int FilterType)
        {
            byte[,] UpdatedImage = ImageMatrix;//O(1)
            for (int i = 0; i < GetHeight(ImageMatrix); i++)//O(H*W*N^2)
            {
                for (int j = 0; j < GetWidth(ImageMatrix);j++)
                {   
                    
                    if(FilterType == 1)//AlphaTrim Filter
                    {
                        UpdatedImage[i, j] = AlphaTrim(ImageMatrix, i, j, n, t, SortType);//O(N^2)
                    }
                    if(FilterType ==2)//AdaptiveMedian Filter
                    {
                        UpdatedImage[i, j] = AdaptiveMedian(ImageMatrix, i, j, 3, n, SortType);//O(N^2)
                    }

                }
            }
            return UpdatedImage;//O(1)
        }
    }
}

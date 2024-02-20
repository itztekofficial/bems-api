//using System;
//using System.IO;

//namespace Core.Util
//{
//    public interface IImageResizer
//    {
//        ImageResizer Resize(int width, int height);

//        ImageResizer Quality(int quality);

//        void Save(string path, bool dispose);

//        MemoryStream ToStream();
//    }

//    public class ImageResizer : IImageResizer, IDisposable
//    {
//        private JpegBitmapEncoder _Encoderjpeg;
//        private Stream _ImageStreamsource;
//        private BitmapFrame _BitmapFramefirstImage;

//        public ImageResizer(string ImagePath)
//            : this(new MemoryStream(File.ReadAllBytes(ImagePath)))
//        { }

//        public ImageResizer(Stream ImageStream)
//        {
//            _ImageStreamsource = ImageStream;
//            _BitmapFramefirstImage = GetFirstBitmapFrame(_ImageStreamsource);

//            _Encoderjpeg = new JpegBitmapEncoder();
//            _Encoderjpeg.Frames.Add(_BitmapFramefirstImage);
//        }

//        public ImageResizer(byte[] ImageBytes)
//            : this(new MemoryStream(ImageBytes))
//        { }

//        public MemoryStream ToStream()
//        {
//            var memStream = new MemoryStream();
//            _Encoderjpeg.Save(memStream);
//            return memStream;
//        }

//        public ImageResizer Quality(int quality)
//        {
//            if (quality <= 75)
//            {
//                _Encoderjpeg.QualityLevel = quality;
//            }

//            return this;
//        }

//        public void Save(string path, bool dispose = true)
//        {
//            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
//            {
//                _Encoderjpeg.Save(fs);
//            }

//            if (dispose)
//            {
//                Dispose();
//            }
//        }

//        public ImageResizer Resize(int width, int height)
//        {
//            var resizedBitmapFrame = Resize(_BitmapFramefirstImage, width, height);

//            _Encoderjpeg.Frames.Clear();
//            _Encoderjpeg.Frames.Add(resizedBitmapFrame);

//            return this;
//        }

//        private bool disposedValue = false; // To detect redundant calls

//        public void Dispose()
//        {
//            Dispose(true);
//        }

//        private void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                if (disposing)
//                {
//                    _ImageStreamsource.Dispose();
//                }
//                disposedValue = true;
//            }
//        }

//        private BitmapFrame GetFirstBitmapFrame(Stream imageStream)
//        {
//            var bitmapDecoder = BitmapDecoder.Create(imageStream,
//                BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
//            return bitmapDecoder.Frames[0];
//        }

//        private BitmapFrame Resize(BitmapFrame bitmapFrame, int width, int height)
//        {
//            double scaleWidth, scaleHeight;

//            if (height == 0)
//            {
//                scaleWidth = width;
//                scaleHeight = (((double)width / bitmapFrame.PixelWidth) * bitmapFrame.PixelHeight);
//            }
//            else if (width == 0)
//            {
//                scaleHeight = height;
//                scaleWidth = (((double)height / bitmapFrame.PixelHeight) * bitmapFrame.PixelWidth);
//            }
//            else
//            {
//                scaleWidth = width;
//                scaleHeight = height;
//            }

//            var scaleTransform = new ScaleTransform(scaleWidth / bitmapFrame.PixelWidth, scaleHeight / bitmapFrame.PixelHeight, 0, 0);

//            var transformedBitmap = new TransformedBitmap(bitmapFrame, scaleTransform);

//            return BitmapFrame.Create(transformedBitmap);
//        }
//    }
//}
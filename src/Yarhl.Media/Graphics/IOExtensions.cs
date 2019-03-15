// IOExtensions.cs
//
// Copyright (c) 2019 SceneGate Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
namespace Yarhl.Media.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using Yarhl.FileFormat;
    using Yarhl.IO;

    public static class IOExtensions
    {
        public static int Map(this byte num, int maxCurrent, int maxTarget)
        {
            return Map((int)num, maxCurrent, maxTarget);
        }

        public static int Map(this int num, int maxCurrent, int maxTarget)
        {
            if (num > maxCurrent)
                throw new ArgumentOutOfRangeException(nameof(num));

            double result = (num * maxTarget) / (double)maxCurrent;
            return (int)Math.Round(result);
        }

        public static Color ReadColor<T>(this DataReader reader)
            where T : IColorEncoding, new()
        {
            T encoding = new T();
            return encoding.Decode(reader.Stream);
        }

        public static Color[] ReadColors<T>(this DataReader reader, int numColors)
            where T : IColorEncoding, new()
        {
            T encoding = new T();

            var colors = new Color[numColors];
            for (int i = 0; i < numColors; i++) {
                colors[i] = encoding.Decode(reader.Stream);
            }

            return colors;
        }

        public static void Write<T>(this DataWriter writer, Color color)
            where T : IColorEncoding, new()
        {
            T encoding = new T();
            encoding.Encode(writer.Stream, color);
        }

        public static void Write<T>(this DataWriter writer, Color[] colors)
            where T : IColorEncoding, new()
        {
            T encoding = new T();
            for (int i = 0; i < colors.Length; i++) {
                encoding.Encode(writer.Stream, colors[i]);
            }
        }
    }
}

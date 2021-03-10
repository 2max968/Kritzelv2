using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageImporter
{
    public class LightInverter
    {
        public static void InvertValue(Mat input, out Mat output)
        {
            Mat hsv = new Mat(input.Size(), MatType.CV_8UC3);
            output = new Mat(input.Size(), MatType.CV_8UC3);
            Cv2.CvtColor(input, hsv, ColorConversionCodes.RGB2HLS);
            Mat[] hsv_split = Cv2.Split(hsv);
            hsv_split[1] = 255 - hsv_split[1];
            Cv2.Merge(hsv_split, hsv);
            Cv2.CvtColor(hsv, output, ColorConversionCodes.HLS2RGB);
        }
    }
}

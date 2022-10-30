using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncEvent
{
    public class CoordinateConvert
    {
        // glass좌표 => 화면좌표로 이동하기 위한 factor
        double[] factorX = new double[3];
        double[] factorY = new double[3];

        // 화면좌좌표 => glass좌표로 이동하기 위한 factor
        double[] factorXInv = new double[3];
        double[] factorYInv = new double[3];

        public CoordinateConvert()
        {
            factorX = new double[] { 1, 0, 0 };
            factorY = new double[] { 0, 1, 0 };
            factorXInv = new double[] { 1, 0, 0 };
            factorXInv = new double[] { 0, 1, 0 };
        }

        public Point SrcToDst(Point point)
        {
            Point scnPoint = new Point();
            scnPoint.X = (int)(factorX[0] * point.X + factorX[1] * point.Y + factorX[2]);
            scnPoint.Y = (int)(factorY[0] * point.X + factorY[1] * point.Y + factorY[2]);
            return scnPoint;
        }

        public Size SrcToDst(Size size)
        {
            Size scnPoint = new Size();
            scnPoint.Width = (int)(factorX[0] * size.Width + factorX[1] * size.Height);
            scnPoint.Height = (int)(factorY[0] * size.Width + factorY[1] * size.Height);
            return scnPoint;
        }

        public Rectangle SrcToDst(Rectangle rect)
        {
            Rectangle scnRect = new Rectangle();
            scnRect.Location = SrcToDst(rect.Location);
            scnRect.Size = (Size)SrcToDst(rect.Size);
            scnRect.Width = Math.Max(2, scnRect.Width);
            scnRect.Height = Math.Max(2, scnRect.Height);
            return scnRect;
        }

        public Point DstToSrc(Point point)
        {
            Point scnPoint = new Point();
            scnPoint.X = (int)(factorXInv[0] * point.X + factorXInv[1] * point.Y + factorXInv[2]);
            scnPoint.Y = (int)(factorYInv[0] * point.X + factorYInv[1] * point.Y + factorYInv[2]);
            return scnPoint;
        }

        public Size DstToSrc(Size size)
        {
            Size scnPoint = new Size();
            scnPoint.Width = (int)(factorXInv[0] * size.Width + factorXInv[1] * size.Height);
            scnPoint.Height = (int)(factorYInv[0] * size.Width + factorYInv[1] * size.Height);
            return scnPoint;
        }

        public Rectangle DstToSrc(Rectangle rect)
        {
            Rectangle scnRect = new Rectangle();
            scnRect.Location = DstToSrc(rect.Location);
            scnRect.Size = (Size)DstToSrc(rect.Size);
            scnRect.Width = Math.Max(2, scnRect.Width);
            scnRect.Height = Math.Max(2, scnRect.Height);
            return scnRect;
        }

        int Inverse(double[] src, double[] dst)
        {
            double det = (src[0] * src[4] * src[8] + src[1] * src[5] * src[6] + src[3] * src[7] * src[2]) -
                (src[2] * src[4] * src[6] + src[1] * src[3] * src[8] + src[5] * src[7] * src[0]);
            if (det == 0)
                return -1;
            dst[0] = src[4] * src[8] - (src[5] * src[7]);
            dst[1] = -((src[1] * src[8]) - (src[2] * src[7]));
            dst[2] = (src[1] * src[5]) - (src[2] * src[4]);
            dst[3] = -((src[3] * src[8]) - (src[5] * src[6]));
            dst[4] = (src[0] * src[8]) - (src[2] * src[6]);
            dst[5] = -((src[0] * src[5]) - (src[2] * src[3]));
            dst[6] = (src[3] * src[7]) - (src[4] * src[6]);
            dst[7] = -((src[0] * src[7]) - (src[1] * src[6]));
            dst[8] = (src[0] * src[4]) - (src[1] * src[3]);

            for (int i = 0; i < 9; i++)
            {
                dst[i] = dst[i] / det;
            }

            return 0;
        }

        int Mul(double[] src, double[] src2, double[] dst)
        {
            dst[0] = src[0] * src2[0] + src[1] * src2[1] + src[2] * src2[2];
            dst[1] = src[3] * src2[0] + src[4] * src2[1] + src[5] * src2[2];
            dst[2] = src[6] * src2[0] + src[7] * src2[1] + src[8] * src2[2];

            return 0;
        }

        /// <summary>
        /// 두 좌표간은 변환식을 구한다.
        /// </summary>
        /// <param name="srcRect">source 좌표계의 좌표를 입력</param>
        /// <param name="dstRect">target 좌표계의 좌표를 입력</param>
        public void MakeFactor(Rectangle srcRect, Rectangle dstRect)
        {
            // 영상(glass) 좌표에서 화면 좌표를 구하기 위한 factor를 생성한다.
            PointF[] srcPos = new PointF[3];
            PointF[] tgtPos = new PointF[3];
            srcPos[0] = new Point(srcRect.X, srcRect.Y);
            srcPos[1] = new Point(srcRect.Right, srcRect.Y);
            srcPos[2] = new Point(srcRect.X, srcRect.Bottom);

            tgtPos[0] = new Point(dstRect.X, dstRect.Y);
            tgtPos[1] = new PointF(dstRect.Right, dstRect.Y);
            tgtPos[2] = new PointF(dstRect.X, dstRect.Bottom);

            // Glass=>화면으로의 factor를 구한다.
            MakeTransFactor(srcPos, tgtPos, factorX, factorY);
            // 화면=>Glass로의 factor를 구한다.
            MakeTransFactor(tgtPos, srcPos, factorXInv, factorYInv);
        }
        // ax+by+c = x'에서 a, b, 를 찾는 루틴
        // 3개의 x'가 필요하고 이 값이 target에 들어간다.
        // factor에는 a, b, c값이 리턴 된다.
        // Stage에서 Image로 이동하는 팩터 구하는 함수
        // srcPos와 targetPos에는 이동전 좌표계와 이동후 좌표계의 포인트가 각각 들어간다
        // 이 식은 srcPos[0]->targetPos[0], srcPos[1]->targetPos[1], srcPos[2]->targetPos[3]으로 이동할 때 식을 구하는 것이며
        // factorX[0] * srcPos.x + factorX[1] * srcPos.y + faxtorX[2] = targetPos.X
        // factorY[0] * srcPos.x + factorY[1] * srcPos.y + faxtorY[2] = targetPos.Y
        // 로 계산됨
        private void MakeTransFactor(PointF[] srcPos, PointF[] targetPos, double[] TrnsX, double[] TrnsY)
        {
            double[] src = new double[9];
            double[] srcInv = new double[9];
            double[] dst = new double[9];
            double[] b = new double[3];

            src[0] = srcPos[0].X;
            src[1] = srcPos[0].Y;
            src[2] = 1;
            src[3] = srcPos[1].X;
            src[4] = srcPos[1].Y;
            src[5] = 1;
            src[6] = srcPos[2].X;
            src[7] = srcPos[2].Y;
            src[8] = 1;

            Inverse(src, srcInv);

            b[0] = targetPos[0].X;
            b[1] = targetPos[1].X;
            b[2] = targetPos[2].X;
            Mul(srcInv, b, TrnsX);

            b[0] = targetPos[0].Y;
            b[1] = targetPos[1].Y;
            b[2] = targetPos[2].Y;
            Mul(srcInv, b, TrnsY);
        }
    }
}

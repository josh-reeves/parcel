namespace PARCEL.Helpers;

public static class Mathematician
{
    #region Constructors
    static Mathematician() { }

    #endregion

    #region Methods
    public static double GetAngle(PointF vertexA, PointF vertexB, PointF vertexC)
    {
        try
        {
            double angle = Math.Atan2(vertexC.Y - vertexA.Y, vertexC.X - vertexA.X) + Math.Atan2(vertexB.Y - vertexA.Y, vertexB.X - vertexA.X);

            if (angle < 0)
                angle += Math.PI * 2;

            return GeometryUtil.RadiansToDegrees(angle);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return 0;

        }

    }

    #endregion

}


namespace SpotFinder_Api.Models
{
    public class SpotTitleComparer : IEqualityComparer<Spot>
    {
        public bool Equals(Spot? x, Spot? y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else
                return x.Title.Equals(y.Title);
        }

        public int GetHashCode(Spot obj)
        {
            if (obj == null)
                return 0;
            else
                return obj.Title.GetHashCode();
        }
    }
}

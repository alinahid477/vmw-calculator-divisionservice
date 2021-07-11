
namespace divisionservice.Services
{
    public class DivisionService : IDivisionService
    {
        public double divide(double a, double b)
        {
            return a/b;
        }

        public string validate(double a, double b)
        {
            if(b == 0) {
                return "cannot devide by zero.";
            }
            return "";
        }
    }
}
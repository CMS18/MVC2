using RouteDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteDemo.Services
{
    static public class CourseRepository
    {
        // Mock database
        public static Course Get(int id)
        {
            var result = new Course
            {
                Id = id,
                Titel = "Demo Title",
                Description = "Bla bla ..."

            };

            return result;
        }
    }
}

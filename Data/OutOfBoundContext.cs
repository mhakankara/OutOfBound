#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutOfBound.Models;

namespace OutOfBound.Data
{
    public class OutOfBoundContext : DbContext
    {
        public OutOfBoundContext (DbContextOptions<OutOfBoundContext> options)
            : base(options)
        {
        }

        public DbSet<OutOfBound.Models.QuestionModel> QuestionModel { get; set; }

        public DbSet<OutOfBound.Models.AnswerModel> AnswerModel { get; set; }
    }
}

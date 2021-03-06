﻿using Microsoft.EntityFrameworkCore;

namespace ABPBlog.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<ABPBlogDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for ABPBlogDbContext */
            // dbContextOptions.UseSqlServer(connectionString);dbContextOptions
            dbContextOptions.UseMySQL(CompressHelper.AES_Decrypt(connectionString, "qwertyuiop", "1234567891234567"));
        }
    }
}

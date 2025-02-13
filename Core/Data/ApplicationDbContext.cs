﻿using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Core.Models;
using Core.Models.Dbo;

namespace Core.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        
        public DbSet<CardDbo> Cards { get; set; } = null!;
        public DbSet<DeckDbo> Decks { get; set; } = null!;
        public DbSet<TagDbo> Tags { get; set; } = null!;
    }
}

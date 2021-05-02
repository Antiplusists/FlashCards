﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Core.Models.Dbo;
using Core.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Repositories.Realizations
{
    public class DeckRepository : RepositoryBase<Guid, DeckDbo, DeckDbo, DeckDbo>, IDeckRepository
    {
        private readonly ICardRepository cardRepo;

        public DeckRepository(ApplicationDbContext dbContext, ICardRepository cardRepo) : base(dbContext)
        {
            this.cardRepo = cardRepo;
        }

        public override async Task<DeckDbo?> FindAsync(Guid id)
        {
            return await DbContext.Decks.FindAsync(id);
        }

        public override async Task<DeckDbo> AddAsync(DeckDbo dbo)
        {
            dbo.Tags = dbo.Tags.AsParallel().Select(tag => DbContext.Tags.Find(tag.Tag) ?? tag).ToHashSet();
            
            var result = await DbContext.Decks.AddAsync(dbo);

            await DbContext.SaveChangesAsync();
            
            return result.Entity!;
        }

        public override async Task<bool> RemoveAsync(Guid id)
        {
            var result = DbContext.Decks.Remove(new DeckDbo {Id = id});

            await DbContext.SaveChangesAsync();

            if (result is {State: EntityState.Deleted})
                return true;

            return false;
        }

        public override async Task<DeckDbo> UpdateAsync(Guid id, DeckDbo dbo)
        {
            dbo.Id = id;
            var result = DbContext.Decks.Update(dbo);

            await DbContext.SaveChangesAsync();

            if (result is {State: EntityState.Modified})
                return result.Entity!;

            throw new OperationException("Failed to update entity");
        }
        
        
        public async Task<CardDbo?> AddCard(Guid deckId, CardDbo dbo)
        {
            var deck = await FindAsync(deckId);

            if (deck is null) return null;

            CardDbo? card;
            
            if (dbo.Id == Guid.Empty)
            {
                card = await cardRepo.AddAsync(dbo);
            }
            else
            {
                card = await cardRepo.FindAsync(dbo.Id);
            }

            if (card is null) return null;
            
            deck.Cards.Add(card);
            
            await DbContext.SaveChangesAsync();

            return card;
        }

        public async Task<CardDbo?> RemoveCard(Guid deckId, Guid cardId)
        {
            var deck = await FindAsync(deckId);
            
            var card = deck?.Cards.Find(card => card.Id == cardId);
            
            if (card is null) return null;
            
            deck!.Cards.Remove(card);

            return card;
        }

        public async Task<bool> AddTags(Guid deckId, params string[] tags)
        {
            var deck = await FindAsync(deckId);

            if (deck is null) return false;
            
            foreach (var tag in tags)
            {
                var tagDbo = await DbContext.Tags.FindAsync(tag);
                deck.Tags.Add(tagDbo ?? new TagDbo(tag));
            }

            return await DbContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> RemoveTags(Guid deckId, params string[] tags)
        {
            var deck = await FindAsync(deckId);

            if (deck is null) return false;

            foreach (var tag in tags)
            {
                deck.Tags.RemoveWhere(tagDbo => tagDbo.Tag == tag);
            }

            return await DbContext.SaveChangesAsync() != 0;
        }

        public IEnumerable<DeckDbo> FindByTags(params string[] tags)
        {
            return DbContext.Tags.Where(tag => tags.Contains(tag.Tag)).SelectMany(tag => tag.Decks).Distinct().AsEnumerable();
        }
    }
}
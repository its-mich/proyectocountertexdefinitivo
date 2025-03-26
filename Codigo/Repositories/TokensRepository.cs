using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Controllers;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{


    public class TokensRepository : ITokens
        {
            private readonly CountertexDbContext context;

            public TokensRepository(CountertexDbContext context)
            {
                this.context = context;
            }

            public async Task<List<Token>> GetTokens()
            {
                var data = await context.Tokens.ToListAsync();
                return data;
            }

            public async Task<bool> PostTokens(Token tokens)
            {
                await context.Tokens.AddAsync(tokens);
                await context.SaveChangesAsync();
                return true;
            }
            public async Task<bool> PutTokens(Token tokens)
            {
                context.Tokens.Update(tokens);
                await context.SaveChangesAsync();
                return true;
            }
            public async Task<bool> DeleteTokens(Token tokens)
            {
                context.Tokens.Remove(tokens);
                await context.SaveChangesAsync();
                return true;
            }
        }


    
}

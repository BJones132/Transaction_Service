using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Scalar.AspNetCore;
using Transaction_Service.Data;
using Transaction_Service.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<TransactionDb>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("financedb"))
);

var app = builder.Build();

app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

app.UseHsts();
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbCtx = scope.ServiceProvider.GetRequiredService<TransactionDb>();
    dbCtx.Database.EnsureCreated();
    try
    {
        dbCtx.Transactions.Count();
    }
    catch (Exception)
    {
        var dbCreator = dbCtx.GetService<IRelationalDatabaseCreator>();
        dbCreator.CreateTables();
    }
}

app.MapGet("/transactions/{accountid}", getTransactions);
app.MapPost("/createTransaction", createTransaction);

app.Run();

static async Task<Ok<List<Transaction>>> getTransactions(TransactionDb db, int accountid)
{
    return TypedResults.Ok(await db.Transactions.Where(t => t.destinationAccountId == accountid).OrderByDescending(t => t.id).ToListAsync());
}

static async Task<Created<Transaction>> createTransaction(TransactionDb db, TransactionDTO transactiondto)
{
    Transaction transaction = new Transaction() {
        sourceAccountId = transactiondto.sourceAccountId,
        destinationAccountId = transactiondto.destinationAccountId,
        reference = transactiondto.reference,
        designator = transactiondto.designator,
        amount = transactiondto.amount,
        destinationBalance = transactiondto.destinationBalance
    };

    db.Transactions.Add(transaction);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/{transaction.id}", transaction);
}
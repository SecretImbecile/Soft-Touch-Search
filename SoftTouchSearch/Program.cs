// <copyright file="Program.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

using SoftTouchSearch.Data;
using SoftTouchSearch.Data.Ingest;
using SoftTouchSearch.Index;
using SoftTouchSearch.Services.Tasks;

// Verify arguments
if (args.Length != 2)
{
    throw new ArgumentException("Expected exactly 2 command-line arguments. (DB and index path)");
}

string dbPath = args[0];
string indexPath = args[1];

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDataServices(dbPath);
builder.Services.AddIndexServices(indexPath);

builder.Services.AddHostedService<IndexHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/Error");

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

#if DEBUG
// Routing for the ingest controller
app.MapIngestControllerRoute();
#endif

app.Run();
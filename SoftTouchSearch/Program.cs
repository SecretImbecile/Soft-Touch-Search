// <copyright file="Program.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.HttpOverrides;
using SoftTouchSearch.Data;
using SoftTouchSearch.Data.Ingest;
using SoftTouchSearch.Index;

// Verify arguments
if (args.Length != 1)
{
    throw new ArgumentException("Expected exactly 1 command-line argument. (Data folder path)");
}

string dataPath = args[0];
string dbPath = Path.Combine(dataPath, "softtouchsearch.db");
string indexPath = Path.Combine(dataPath, "softtouchsearch_index");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDataServices(dbPath);
builder.Services.AddIndexServices(indexPath);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseHttpsRedirection();

    // Enable the proxy server
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    });
}

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
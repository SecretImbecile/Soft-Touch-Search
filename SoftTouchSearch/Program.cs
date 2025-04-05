// <copyright file="Program.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.HttpOverrides;
using SoftTouchSearch.Data;
using SoftTouchSearch.Index;
using SoftTouchSearch.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Load application configuration
SoftTouchSearchSettings appSettings = new();
builder.Configuration
    .GetRequiredSection(nameof(SoftTouchSearchSettings))
    .Bind(appSettings);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSearchDatabase(appSettings.PathToDatabase);
builder.Services.AddIndexServices(appSettings.PathToIndex);

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

app.Run();
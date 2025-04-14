// <copyright file="Program.cs" company="Jack Kelly">
// Copyright © 2024, 2025 Jack Kelly.
//
// Soft Touch Search is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation, either version 3 of the License, or
// (at your option)any later version.
//
// Soft Touch Search is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// Soft Touch Search. If not, see [https://www.gnu.org/licenses/].
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
builder.Services.AddResponseCaching();
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

app.UseResponseCaching();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/Error");

app.UseRouting();
app.MapRazorPages();
app.MapControllers();

app.Run();
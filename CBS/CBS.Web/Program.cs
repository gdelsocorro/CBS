using CBS.Infrastructure;
using CBS.Shared.Services;
using CBS.Web.Components;
using CBS.Web.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IFormFactor, FormFactor>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddSingleton<AdminStateService>();
builder.Services.AddSingleton<VendorStateService>();
builder.Services.AddSingleton<ContributionStateService>();
builder.Services.AddSingleton<ExpenseStateService>();
builder.Services.AddSingleton<MemberStateService>();
builder.Services.AddSingleton<NotificationStateService>();

builder.Services.AddMudServices();

var app = builder.Build();

app.Services.InitializeDatabase();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(
        typeof(CBS.Shared._Imports).Assembly);

app.Run();

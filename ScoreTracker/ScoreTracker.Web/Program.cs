using ScoreTracker.Application.Contracts;
using ScoreTracker.CompositionRoot;
using ScoreTracker.Web.Mapping;
using ScoreTracker.Web.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddScoreTrackerSecurity
    (builder.Configuration).AddHttpContextAccessor().AddEndpointsApiExplorer().AddSwaggerGen().AddScoreTrackerCore().AddScoreTrackerInfrastructure()
  .AddAutoMapper(typeof(CoreToPresentationMapperProfile)).AddTransient<ICurrentUserAccessor, HttpContextCurrentUserAccessor>().AddControllers();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();
app.Run();
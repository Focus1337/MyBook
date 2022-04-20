﻿using Microsoft.AspNetCore.Identity;
using MyBook.DataAccess;
using MyBook.Entity;
using MyBook.Entity.Identity;

namespace MyBook.Middleware;

public class SubscriptionMiddleware
{
    private readonly RequestDelegate _next;

    public SubscriptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<User> userManager, ApplicationContext appContext)
    {
        var curUser = await userManager.GetUserAsync(context.User);
        var timePassed = DateTime.Now - curUser.SubDateStart;

        var sub = appContext.Subs.FirstOrDefault(x => x.Id == curUser.SubId)!;

        if (timePassed.TotalDays >= sub.Duration)
        {
            curUser.SubDateStart = default;
            curUser.SubId = 4;
            await userManager.RemoveFromRoleAsync(curUser, "UserSub");
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}

public static class SubscriptionMiddlewareExtensions
{
    public static IApplicationBuilder UseSubscription(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SubscriptionMiddleware>();
    }
}